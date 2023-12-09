using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TCC_Web.Models.DTOs.Login;
using TCC_Web.Models.DTOs.User;
using TCC_Web.Services;

namespace TCC_Web.Controllers
{
	public class UserController : Controller
	{
		private readonly ILogger<UserController> _logger;
		private readonly ApiService _apiService;

		public UserController(ILogger<UserController> logger, ApiService apiService)
		{
			_logger = logger;
			_apiService = apiService;
		}

		public async Task<IActionResult> Index()
		{
			var userList = new List<UserDetailedDTO>();
			string apiUrl = "https://localhost:7094/User/GetAll";
			try
			{
				string apiData = await _apiService.GetApiData(apiUrl);
				// Desserializar os dados da API em um objeto
				userList = JsonConvert.DeserializeObject<List<UserDetailedDTO>>(apiData);
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = "Ocorreu um erro ao obter os dados do banco de dados: " + ex.Message;
				return View(userList);
			}

			return View(userList);
		}

		public IActionResult Add()
		{
			var model = new UserSessionDTO();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(UserSessionDTO model)
		{
			if (model.User.BirthDate > DateTime.UtcNow.AddYears(-18).AddSeconds(-10))
			{
				TempData["ErrorMessage"] = "O usuário deve ser maior de idade (18 anos)";
				return View(model);
			}

			string postBody = JsonConvert.SerializeObject(model);

			string apiUrl = "https://localhost:7094/User/Add";
			string message = await _apiService.PostApiData(apiUrl, postBody);

			if (message == "Usuário cadastrado com sucesso!")
			{
				return RedirectToAction("Index", "Home");
			}

			return View(model);
		}

		public async Task<IActionResult> Update(Guid id)
		{
			string apiUrl = "https://localhost:7094/User/GetById/" + id.ToString();
			string apiData = await _apiService.GetApiData(apiUrl);

			// Desserializar os dados da API em um objeto
			var model = JsonConvert.DeserializeObject<UserSessionDTO>(apiData);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UserSessionDTO model)
		{
			if (model.User.BirthDate > DateTime.UtcNow.AddYears(-18).AddSeconds(-10))
			{
				TempData["ErrorMessage"] = "O usuário deve ser maior de idade (18 anos)";
				return View(model);
			}

			string postBody = JsonConvert.SerializeObject(model);

			string apiUrl = "https://localhost:7094/User/Update";
			string message = await _apiService.PutApiData(apiUrl, postBody);

			// Desserializar os dados da API em um objeto
			if (message == "Usuário alterado com sucesso!")
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		public async Task<IActionResult> Remove(Guid id)
		{
			string apiUrl = "https://localhost:7094/User/Remove/" + id.ToString();
			string message = await _apiService.DeleteApi(apiUrl);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> View(Guid id)
		{
			string apiUrl = "https://localhost:7094/User/GetById/" + id.ToString();
			string apiData = await _apiService.GetApiData(apiUrl);

			// Desserializar os dados da API em um objeto
			var model = JsonConvert.DeserializeObject<UserSessionDTO>(apiData);

			return View(model);
		}

        //aaaaaaaaaaaaaaaaaaaaaaaa Login
        public IActionResult Login()
        {
            var model = new LoginDTO();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
			if (!ModelState.IsValid)
				return View(model);

			string apiUrl = "https://localhost:7094/User/Login";
			string postBody = JsonConvert.SerializeObject(model);
			string message = await _apiService.PostApiData(apiUrl, postBody);

			if (message == "Login realizado com sucesso")
			{
				return RedirectToAction("Index", "Home");
			}

			TempData["ErrorMessage"] = message;

			return View(model);	
        }

        public IActionResult SingUp()
        {
            var model = new UserDetailedDTO();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SingUp(UserDetailedDTO model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return View(model);

            if (model.BirthDate > DateTime.UtcNow.AddYears(-18).AddSeconds(-10))
            {
                TempData["ErrorMessage"] = "O usuário deve ser maior de idade (18 anos)";
                return View(model);
            }
            string apiUrl = "https://localhost:7094/User/Add";
            try
            {
				model.Type = Models.Enums.User.UserType.Client;
                string postBody = JsonConvert.SerializeObject(model);
                var message = await _apiService.PostApiData(apiUrl, postBody);

                if (message == "Usuário cadastrado com sucesso!")
                {
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = message;

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro ao obter os dados do banco de dados: " + ex.Message;
                return View(model);
            }
        }
    }
}
