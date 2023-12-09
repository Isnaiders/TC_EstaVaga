using System.Net;
using System.Text;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using RazorClassLibrary.Models.Entities.Base;

namespace RazorClassLibrary.Services
{
	public class ApiService
	{
		public Guid UserId;
		public string UserIdentifier;

		private readonly HttpClient _httpClient;

		public ApiService()
		{
			_httpClient = new HttpClient()
			{
				BaseAddress = new Uri("https://localhost:7094/"),
			};
			_httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
			_httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, Environment.UserName);
			_httpClient.DefaultRequestHeaders.Add(HeaderNames.AcceptLanguage, "pt-br");
		}

		public async Task<string> GetApiData(string route)
		{
			var apiUrl = _httpClient.BaseAddress + route;
			HttpResponseMessage response = new HttpResponseMessage();
			try
			{
				response = await _httpClient.GetAsync(apiUrl);
			}
			catch (Exception)
			{
				return "Não foi possível fazer a comunicação com o servidor.";
			}

			if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.BadRequest)
			{
				return await response.Content.ReadAsStringAsync();
			}
			else
			{
				return $"Erro na requisição: {response.StatusCode}";
			}
		}

		public async Task<List<ResultModel>> PostApiData(string route, string jsonData)
		{
			return await PostOrPutApiData(route, jsonData, true);
		}

		public async Task<List<ResultModel>> PutApiData(string route, string jsonData)
		{
			return await PostOrPutApiData(route, jsonData, false);
		}

		public async Task<List<ResultModel>> PostOrPutApiData(string route, string jsonData, bool IsPost)
		{
			var apiUrl = _httpClient.BaseAddress + route;
			HttpResponseMessage response = new HttpResponseMessage();
			HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			List<ResultModel> results = new List<ResultModel>();

			try
			{
				if (IsPost)
					response = await _httpClient.PostAsync(apiUrl, content);
				else
					response = await _httpClient.PutAsync(apiUrl, content);
			}
			catch (Exception)
			{
				results.Add(new ResultModel("Não foi possível fazer a comunicação com o servidor.", HttpStatusCode.BadRequest));
				return results;
			}

			//TODO: Arrumar o retorno da API
			if (response.IsSuccessStatusCode)
			{
				results.Add(new ResultModel(await response.Content.ReadAsStringAsync(), HttpStatusCode.Accepted));
				return results;
			}
			else if (response.StatusCode == HttpStatusCode.BadRequest)
			{
				string errorContent = await response.Content.ReadAsStringAsync();
				try
				{
					results = JsonConvert.DeserializeObject<List<ResultModel>>(errorContent);
				}
				catch (Exception)
				{
					results.Add(new ResultModel("Tente novamente.", HttpStatusCode.InternalServerError));
				}
				return results;
			}
			else
			{
				results.Add(new ResultModel(await response.Content.ReadAsStringAsync(), response.StatusCode));
				return results;
			}
		}

		public async Task<string> DeleteApi(string route)
		{
			var apiUrl = _httpClient.BaseAddress + route;
			HttpResponseMessage response = new HttpResponseMessage();

			try
			{
				response = await _httpClient.DeleteAsync(apiUrl);
			}
			catch (Exception)
			{
				return "Não foi possível fazer a comunicação com o servidor.";
			}

			if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.BadRequest)
			{
				return await response.Content.ReadAsStringAsync();
			}
			else
			{
				return $"Erro na requisição: {response.StatusCode}";
			}
		}
	}
}