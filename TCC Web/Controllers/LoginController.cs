using Microsoft.AspNetCore.Mvc;
using TCC_Web.Models.DTOs.Login;
using Newtonsoft.Json;
using TCC_Web.Services;
using TCC_Web.Models.DTOs.User;

namespace TCC_Web.Controllers
{
	public class LoginController : Controller
	{
		private readonly ApiService _apiService;

		public LoginController(ApiService apiService)
		{
			_apiService = apiService;
		}

		public IActionResult Index()
		{
			var model = new LoginDTO();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginDTO model)
		{
			string apiUrl = "https://localhost:7094/User/Login";
			try
			{
				if(!ModelState.IsValid)
					return View("Index", model);

				string postBody = JsonConvert.SerializeObject(model);
				var result = Convert.ToBoolean(await _apiService.PostApiData(apiUrl, postBody));

				if (result)
					return RedirectToAction("Index", "Home");

				TempData["ErrorMessage"] = "Usuário ou senha inválidos.";

				return View("Index", model);
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = "Ocorreu um erro ao obter os dados do banco de dados: " + ex.Message;
				return View("Index", model);
			}
		}

		public IActionResult SingUp()
		{
			var model = new UserSessionDTO();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> SingUp(UserSessionDTO model)
		{
			if (string.IsNullOrEmpty(model.User.Email) || string.IsNullOrEmpty(model.User.Password))
				return View(model);
			
			if (model.User.BirthDate > DateTime.UtcNow.AddYears(-18).AddSeconds(-10))
			{
				TempData["ErrorMessage"] = "O usuário deve ser maior de idade (18 anos)";
				return View(model);
			}
			string apiUrl = "https://localhost:7094/Authentication/Add";
			try
			{
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
