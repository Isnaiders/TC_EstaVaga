namespace TCC_Web.Models.Components
{
    /// <summary>
    /// Parâmetros de paginação de uma consulta
    /// </summary>
    public class PaginationParams
    {
        #region :: Construtores
        public PaginationParams()
        {
            IgnoreRemoveds = true;
            IsPagined = true;
            OrderBy = new HashSet<string>();
            OutlistedData = new Dictionary<string, object>();
            FooterData = new List<KeyValuePair<string, string>>();
        }

        public PaginationParams(int skip, int take, string search)
            : this()
        {
            Skip = skip;
            Take = take;
            Search = search;
        }
        #endregion

        #region :: Constantes Estáticas

        /// <summary>
        /// Retorna um PaginationParamms configurado para não paginar dados
        /// </summary>
        public static PaginationParams NotPaginned { get { return new PaginationParams { IsPagined = false }; } }

        #endregion

        /// <summary>
        /// Quantidade de registros total da Query no Banco de Dados
        /// </summary>
        public int MaxCount { get; set; }

        /// <summary>
        /// Indicador do registro inicial a ser obtido
        /// </summary>
        public int Skip { get; private set; }

        /// <summary>
        /// Quantidade de registros a serem obtidos da consulta a partir do registro inicial Skip
        /// </summary>
        public int Take { get; private set; }

        /// <summary>
        /// Filtro por string para buscas personalizadas
        /// </summary>
        public string Search { get; private set; }

        /// <summary>
        /// Ordenação pelos campos
        /// </summary>
        public HashSet<string> OrderBy { get; set; }

        /// <summary>
        /// Não traz nenhum registro que esteja com SystemStatus = Removed ou Unknow
        /// </summary>
        public bool IgnoreRemoveds { get; set; }

        /// <summary>
        /// Se true, não realiza LazzyLoad e traz apenas dados da entidade raiz
        /// </summary>
        public bool Detached { get; set; }

        //TODO: Alterar nome da propriedade para IsPaginned [31/07/2019 - Bruno.Crema]
        /// <summary>
        /// Se true a consulta será paginada com base nos parâmetros Skip e Take
        /// </summary>
        public bool IsPagined { get; set; }

        /// <summary>
        /// Retorna true se a propriedade Search for preenchida
        /// </summary>
        public bool HasSearchText { get { return !string.IsNullOrWhiteSpace(Search); } }

        /// <summary>
        /// Dados adicionais relacionados à paginação
        /// Estes dados não serão exibidos dentro da tabela, mas podem ser usados para qualquer manipulação/exibição em tela.
        /// </summary>
        public IDictionary<string, object> OutlistedData { get; set; }

        /// <summary>
        /// Dados a serem exibidos no footer da tabela, preferencialmente atribuídos na camada de Repository
        /// Key = nome da coluna
        /// Value = valor da célula
        /// </summary>
        public List<KeyValuePair<string, string>> FooterData { get; set; }


        public void SetOrderByProperty(string sort, bool orderByDesc = false)
        {
            if (!string.IsNullOrWhiteSpace(sort))
                OrderBy.Add(sort + (orderByDesc ? " DESC" : ""));
        }
    }
}
