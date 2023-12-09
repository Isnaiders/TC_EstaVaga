using System.Net;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TCC_API.Interfaces.Repositories;
using TCC_API.Models;
using TCC_API.Models.DTOs.User;
using TCC_API.Models.Entities.Base;
using TCC_API.Models.Entities.User;

namespace TCC_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : Controller
	{
		public readonly IMapper _mapper;
		public readonly IUserRepository _userRepository;
		public readonly IUserSessionRepository _userSessionRepository;

		public UserController(IMapper mapper, IUserRepository userRepository, IUserSessionRepository userSessionRepository)
		{
			_mapper = mapper;
			_userRepository = userRepository;
			_userSessionRepository = userSessionRepository;
		}

		[HttpGet("GetAll")]
		public ActionResult<IEnumerable<UserDetailedDTO>> GetAllUser()
		{
			var userDBs = _userRepository.GetAll();
			var userDTOs = _mapper.Map<IEnumerable<UserDetailedDTO>>(userDBs);
			return Ok(userDTOs);
		}

		[HttpGet("GetById/{userId}")]
		public ActionResult<User> GetUserById(Guid userId)
		{
			var userDB = _userRepository.GetById(userId);

			if (userDB == null)
			{
				return NotFound("Usuário não encontrado.");
			}

			var userDTO = _mapper.Map<User>(userDB);
			return Ok(userDTO);
		}

		[HttpPost("Add")]
		public ActionResult Add(UserDetailedDTO userDTO)
		{
			var userDB = _mapper.Map<User>(userDTO);

			var isValidName = !string.IsNullOrWhiteSpace(userDTO.Name);
			var isValidPassword = !string.IsNullOrWhiteSpace(userDTO.Password);
			var isValidEmail = IsValidEmail(userDTO.Email);
			var canSaveEmail = !_userRepository.EmailExists(userDB.Email);
			var isValidBirthDate = IsValidBirthDate(userDTO.BirthDate);

			var results = new List<ResultModel>();
			if (!isValidName)
				results.Add(new ResultModel("Nome é obrigatoório.", HttpStatusCode.NotAcceptable, "Nome"));
			if (!isValidPassword)
				results.Add(new ResultModel("Senha é obrigatória.", HttpStatusCode.NotAcceptable, "Senha"));
			if (!isValidEmail)
				results.Add(new ResultModel("Formato do email informado é inválido.", HttpStatusCode.NotAcceptable, "Email"));
			if (!isValidBirthDate)
				results.Add(new ResultModel("A data de nascimento não corresponde com as politicas de utilização da empresa.", HttpStatusCode.NotAcceptable, "Data de Nascimento"));
			if (!canSaveEmail)
				results.Add(new ResultModel("O email informado já existe em nosso sistema.", HttpStatusCode.NotAcceptable, "Email"));

			if (results.Any())
				return BadRequest(results);

			_userRepository.Add(userDB);

			if (_userRepository.SaveAll())
			{
				results.Add(new ResultModel("Usuário cadastrado com sucesso!", HttpStatusCode.Accepted));
				return Ok(results);
			}

			results.Add(new ResultModel("Ocorreu um erro ao salvar o usuário.", HttpStatusCode.NotModified));
			return BadRequest(results);
		}

		#region:: UserValidations
		static bool IsValidBirthDate(DateTime birthDate)
		{
			int idade = DateTime.Now.Year - birthDate.Year;

			// Verifique se a pessoa tem pelo menos 18 anos
			if (idade >= 18 && birthDate < DateTime.UtcNow)
			{
				return true;
			}

			return false;
		}
		static bool IsValidEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;
			// Define a expressão regular para validar endereços de e-mail
			string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

			// Ignore o caso (case-insensitive)
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

			// Verifique se o e-mail corresponde ao padrão
			return regex.IsMatch(email);
		}
		#endregion

		[HttpPut("Update")]
		public ActionResult Update(UserDetailedDTO userDTO)
		{
			var userDB = _userRepository.GetById(userDTO.Id);

			if (userDB == null)
			{
				return BadRequest("Usuário não encontrado.");
			}

			userDB.Name = userDTO.Name;
			userDB.Type = userDTO.Type;
			userDB.Email = userDTO.Email;
			userDB.BirthDate = userDTO.BirthDate;
			userDB.WhenUpdated = DateTime.UtcNow;
			userDB.SystemStatus = userDTO.SystemStatus;

			_userRepository.Update(userDB);

			if (_userRepository.SaveAll())
			{
				return Ok("Usuário alterado com sucesso!");
			}

			return BadRequest("Ocorreu um erro ao alterar o usuário.");
		}

		[HttpDelete("Remove/{userId}")]
		public ActionResult Remove(Guid userId)
		{
			var validationResult = _userRepository.Remove(userId);

			if (!string.IsNullOrEmpty(validationResult.ErrorMessage))
			{
				return NotFound(validationResult.ErrorMessage);
			}

			if (_userRepository.SaveAll())
			{
				return Ok("Usuário foi removido com sucesso!");
			}

			return BadRequest("Ocorreu um erro ao remover o usuário.");
		}

		//Session -*------------------------------------------------------
		[HttpPost("Login")]
		public async Task<ActionResult> Login(LoginModel model)
		{
			var userId = _userRepository.GetUserIdByLogin(model.UserIdentifier, model.Password);

			if (userId.HasValue && userId != default)
			{
				var sessionDB = _userSessionRepository.GetByUserId(userId.Value);

				if (sessionDB != null)
				{
					if (sessionDB.End > DateTime.UtcNow)
						return Ok("Login realizado com sucesso");

					_userSessionRepository.Remove(sessionDB.Id);
				}

				var session = new UserSession(userId.Value, DateTime.UtcNow, DateTime.UtcNow.AddHours(1));
				_userSessionRepository.Add(session);

				if (await _userRepository.SaveAllAsync())
				{
					return Ok("Login realizado com sucesso");
				}

				return BadRequest("Ocorreu um erro ao realizar o login");
			}

			return BadRequest("Usuário ou senha incorretos");
		}

		[HttpDelete("Logout/{userId}")]
		public async Task<ActionResult> Logout(Guid userId)
		{
			if (userId != Guid.Empty)
			{
				var validationResult = _userSessionRepository.RemoveAllByUserId(userId);

				if (!string.IsNullOrEmpty(validationResult.ErrorMessage))
				{
					return NotFound(validationResult.ErrorMessage);
				}

				if (_userSessionRepository.SaveAll())
				{
					return Ok("Logout feito com sucesso!");
				}

				return BadRequest("Ocorreu um erro ao realizar o logout do usuário.");
			}

			return BadRequest("Usuário informado inválido!");
		}

		//[HttpGet("GetSessionById/{userId}")]
		//public async Task<ActionResult<UserSession>> GetUserSessionById(Guid userId)
		//{
		//    var userSessionDb = await _userSessionRepository.GetById(userId);

		//    if (userSessionDb == null)
		//    {
		//        return NotFound("Usuário não encontrado.");
		//    }

		//    var userSessionDTO = _mapper.Map<UserSessionDetailedDTO>(userSessionDb);
		//    return Ok(userSessionDTO);
		//}

		[HttpPost("AddUserSession")]
		public ActionResult AddUserSession(UserSessionDetailedDTO userSessionDTO)
		{
			var userSessionDB = _mapper.Map<UserSession>(userSessionDTO);
			_userSessionRepository.Add(userSessionDB);

			if (_userSessionRepository.SaveAll())
			{
				return Ok("Usuário cadastrado com sucesso!");
			}

			return BadRequest("Ocorreu um erro ao salvar o usuário.");
		}

		//[HttpDelete("RemoveSession/{userId}")]
		//public async Task<ActionResult> RemoveUser(Guid userId)
		//{
		//	var validationResult = _authenticationRepository.Remove(userId);

		//	if (!string.IsNullOrEmpty(validationResult.ErrorMessage))
		//	{
		//		return NotFound(validationResult.ErrorMessage);
		//	}

		//	if (await _authenticationRepository.SaveAllAsync())
		//	{
		//		return Ok("Usuário foi removido com sucesso!");
		//	}

		//	return BadRequest("Ocorreu um erro ao remover o usuário.");
		//}

		[HttpGet("GetIdByUserIdentity/{userIdentity}")]
		public ActionResult<Guid> GetIdByUserIdentifier(string userIdentity)
		{
			var userId = _userRepository.GetIdByUserIdentifier(userIdentity);

			if (userId == null)
			{
				return NotFound("Usuário não encontrado.");
			}

			return Ok(userId);
		}
	}
}
