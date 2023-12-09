using System.Text;

namespace TCC_Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetApiData(string apiUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return data;
            }
            else
            {
                // Tratar o erro de acordo com suas necessidades
                throw new HttpRequestException($"Erro na requisição: {response.StatusCode}");
            }
        }

        public async Task<string> PostApiData(string apiUrl, string jsonData)
        {
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return data;
            }
            else
            {
                // Tratar o erro de acordo com suas necessidades
                throw new HttpRequestException($"Erro na requisição: {response.StatusCode}");
            }
        }

		public async Task<string> PutApiData(string apiUrl, string jsonData)
		{
			HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _httpClient.PutAsync(apiUrl, content);

			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				return data;
			}
			else
			{
				// Tratar o erro de acordo com suas necessidades
				throw new HttpRequestException($"Erro na requisição: {response.StatusCode}");
			}
		}

		public async Task<string> DeleteApi(string apiUrl)
		{
			HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				return data;
			}
			else
			{
				// Tratar o erro de acordo com suas necessidades
				throw new HttpRequestException($"Erro na requisição: {response.StatusCode}");
			}
		}
	}
}