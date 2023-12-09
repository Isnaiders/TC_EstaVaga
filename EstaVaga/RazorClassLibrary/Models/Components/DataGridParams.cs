using Newtonsoft.Json;

namespace RazorClassLibrary.Models.Components
{
    /// <summary>
    /// Parâmetros do DataGrid para consulta Ajax
    /// </summary>
    [Serializable]
    public class DataGridParams
    {
        public DataGridParams()
        {
            Offset = int.MinValue;
            Limit = int.MinValue;

            Sort = string.Empty;
            Order = string.Empty;
            Search = string.Empty;
        }

        /// <summary>
        /// Quantidade máxima de registros por página
        /// </summary>
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Refêrencia da página atual
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "sort")]
        public string Sort { get; set; }

        [JsonProperty(PropertyName = "order")]
        public string Order { get; set; }

        /// <summary>
        /// String do campo de busca
        /// </summary>
        [JsonProperty(PropertyName = "search")]
        public string Search { get; set; }

        /// <summary>
        /// Retorna um objeto de parâmetros de paginação preenchido
        /// </summary>
        public PaginationParams GetPaginationParams()
        {
            var result = new PaginationParams(Offset, Limit, Search);

            if (!string.IsNullOrWhiteSpace(Sort))
                result.OrderBy.Add(Sort + (Order?.ToUpper()?.Trim() == "DESC" ? " DESC" : ""));

            if (Search != null)
                Search = Search.Trim();

            return result;
        }
    }
}
