using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TCC_Web.Models.DTOs.Parking;
using TCC_Web.Services;

namespace TCC_Web.Controllers
{
	public class ParkingController : Controller
	{
		private readonly ILogger<ParkingController> _logger;
		private readonly ApiService _apiService;

		public ParkingController(ILogger<ParkingController> logger, ApiService apiService)
		{
			_logger = logger;
			_apiService = apiService;
		}

		public async Task<IActionResult> Index()
		{
			var parkingList = new List<ParkingDetailedDTO>();
			string apiUrl = "https://localhost:7094/Parking/GetAll";
			try
			{
				string apiData = await _apiService.GetApiData(apiUrl);
				// Desserializar os dados da API em um objeto
				parkingList = JsonConvert.DeserializeObject<List<ParkingDetailedDTO>>(apiData);
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = "Ocorreu um erro ao obter os dados do banco de dados: " + ex.Message;
				return View(parkingList);
			}

			return View(parkingList);
		}

		//[HttpPost]
		//public async Task<JsonResult> LoadGridListAsync(DataGridParams gridDataParams)
		//{
		//	var showActionsColumn = false;
		//	var paginationParams = gridDataParams.GetPaginationParams();

		//	string apiUrl = "https://localhost:7094/Parking/GetAll";
		//	string apiData = await _apiService.GetApiData(apiUrl);
		//	// Desserializar os dados da API em um objeto
		//	var parkingList = JsonConvert.DeserializeObject<IEnumerable<Parking>>(apiData);

		//	var rows = new JArray();
		//	foreach (var parking in parkingList)
		//	{
		//		dynamic row = new JObject();
		//		row.Id = parking.Id;
		//		row.Name = parking.Name;
		//		row.Address = parking.Address;
		//		row.AddressNumber = parking.AddressNumber;
		//		row.AddressComplement = parking.AddressComplement;
		//		//row.Languages = (tag.Translations?.Any() ?? false) ? tag.Translations.OrderBy(e => e.Language)
		//		//.Select(e => e.Language.ToString())
		//		//    .Aggregate((a, x) => a + "," + x) : "";
		//		//row.ActionsButton = HTMLHelper.BootstrapTableActionsButton(BootstrapTableActionsButtonItem.NewItems
		//		//    .AddIf(canUpdate, I18n.Instance.("Edit"), Url.Action("Edit", new { Id = tag.TagId }), null, "fa fa-fw fa-edit")
		//		//    .EndIf(canRemove, I18n.Instance.__("Remove"), null, $"remove('{tag.TagId}', '{tag.TagName}')", "fa fa-fw fa-trash", "text-danger")
		//		//).ToHtmlString();
		//		showActionsColumn = showActionsColumn || ((string)row.ActionsButton).Length > 10;

		//		rows.Add(row);
		//	}

		//	return Json(new DataGridResult(paginationParams.MaxCount, rows, showActionsColumn));
		//}

		public async Task<IActionResult> View(Guid id)
		{
			string apiUrl = "https://localhost:7094/Parking/GetById/" + id.ToString();
			string apiData = await _apiService.GetApiData(apiUrl);

			// Desserializar os dados da API em um objeto
			ParkingDetailedDTO model = JsonConvert.DeserializeObject<ParkingDetailedDTO>(apiData);

			return View(model);
		}

		public IActionResult Add()
		{
			var model = new ParkingDetailedDTO();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(ParkingDetailedDTO model)
		{
			string postBody = JsonConvert.SerializeObject(model);

			string apiUrl = "https://localhost:7094/Parking/Add";
			string message = await _apiService.PostApiData(apiUrl, postBody);

			// Desserializar os dados da API em um objeto
			if (message == "Estacionamento cadastrado com sucesso!")
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		public async Task<IActionResult> Update(Guid id)
		{
			string apiUrl = "https://localhost:7094/Parking/GetById/" + id.ToString();
			string apiData = await _apiService.GetApiData(apiUrl);

			// Desserializar os dados da API em um objeto
			var model = JsonConvert.DeserializeObject<ParkingDetailedDTO>(apiData);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Update(ParkingDetailedDTO model)
		{
			string postBody = JsonConvert.SerializeObject(model);

			string apiUrl = "https://localhost:7094/Parking/Update";
			string message = await _apiService.PutApiData(apiUrl, postBody);

			// Desserializar os dados da API em um objeto
			if (message == "Estacionamento alterado com sucesso!")
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		public async Task<IActionResult> Remove(Guid id)
		{
			string apiUrl = "https://localhost:7094/Parking/Remove/" + id.ToString();
			string message = await _apiService.DeleteApi(apiUrl);

			return RedirectToAction("Index");
		}
	}
}