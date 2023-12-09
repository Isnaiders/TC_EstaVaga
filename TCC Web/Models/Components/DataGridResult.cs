using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace TCC_Web.Models.Components
{
    /// <summary>
    /// Objeto de retorno para consultas paginadas do componente visual BootstrapTable
    /// </summary>
    [Serializable]
    public class DataGridResult
    {
        #region :: Construtor
        public DataGridResult() { }
        public DataGridResult(int total, JArray rows, bool showActionsColumn = true, bool showAddButton = true)
            : this()
        {
            Total = total;

            // Realiza a desserialização
            Rows = JsonSerializer.Deserialize<object>(rows.ToString());

            ShowActionsColumn = showActionsColumn;

            ShowAddButton = showAddButton;
            OutlistedData = new Dictionary<string, object>();
            FooterData = new List<KeyValuePair<string, string>>();
        }
        public DataGridResult(PaginationParams paginationParams, JArray rows, bool showActionsColumn = true, bool showAddButton = true)
            : this()
        {
            Total = paginationParams.MaxCount;

            // Realiza a desserialização
            Rows = JsonSerializer.Deserialize<object>(rows.ToString());

            ShowActionsColumn = showActionsColumn;

            ShowAddButton = showAddButton;
            OutlistedData = paginationParams.OutlistedData;
            FooterData = paginationParams.FooterData;
        }
        #endregion

        /// <summary>
        /// Quantidade todal de registros retornados na consulta
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// JArray contendo as linhas retornadas da consulta paginada
        /// </summary>
        public object Rows { get; private set; }

        /// <summary>
        /// Se deve ou não exibir a coluna de ações
        /// </summary>
        public bool ShowActionsColumn { get; private set; }

        /// <summary>
        /// Se deve ou não exibir o botão de adicionar (definido acima da tabela)
        /// </summary>
        public bool ShowAddButton { get; private set; }

        /// <summary>
        /// Dados extra, não são adicionados dentro do grid.
        /// </summary>
        /// <remarks>
        /// A finalidade desta propriedade é mandar dados adicionais para a tela,
        /// será útil quando existir a necessidade de atualizar alguma informação da tela
        /// (via JS) assim que a tabela for atualizada.
        /// </remarks>
        public IDictionary<string, object> OutlistedData { get; private set; }

        /// <summary>
        /// Informações (texto) a serem adicionadas no footer da tabela.
        /// Key: chave da coluna ("data-field")
        /// Value: o texto que deve ser inserido no footer de acordo com a chave
        /// </summary>        
        /// <remarks>O tipo desta propriedade NÃO é um dicionário justamente para facilitar a manipulação em JS.</remarks>
        public List<KeyValuePair<string, string>> FooterData { get; private set; }
    }
}
