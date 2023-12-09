using System.Net;

namespace TCC_API.Models.Entities.Base
{
	public class ResultModel
	{
		public ResultModel(string validationMessage, HttpStatusCode statusCode, string field = "Mensagem")
		{
			ValidationMessage = validationMessage;
			StatusCode = statusCode;
			Field = field;
		}

		public string ValidationMessage { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public string Field { get; set; }

		public bool IsSuccessStatusCode
		{
			get { return ((int)StatusCode >= 200) && ((int)StatusCode <= 299); }
		}
	}
}
