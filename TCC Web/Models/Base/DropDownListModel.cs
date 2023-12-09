using Microsoft.AspNetCore.Mvc.Rendering;

namespace TCC_Web.Models.Base
{
    public class DropDownListModel
    {
        public static IEnumerable<SelectListItem> DataEnumerate<T>(
            T enumType,
            string selected = "",
            bool showDefaultOption = true,
            string defaultOptionText = "",
            bool selectIfSingleOption = false) where T : struct, IConvertible
        {
            var values = new Dictionary<T, string>();

            foreach (var value in System.Enum.GetValues(typeof(T)))
                if (value.ToString().ToUpper() != "UNKNOWN" && value.ToString().ToUpper() != "DESCONHECIDO")
                    values.Add((T)value, value.ToString());

            return DataEnumerate<T>(
                values,
                selected,
                showDefaultOption,
                defaultOptionText,
                selectIfSingleOption);
        }

        public static IEnumerable<SelectListItem> DataEnumerate<T>(
            IReadOnlyDictionary<T, string> dataDictionary
            , string selected = ""
            , bool showDefaultOption = true
            , string defaultOptionText = ""
            , bool selectIfSingleOption = false)
        {
            var result = new List<SelectListItem>();

            if (showDefaultOption)
                result.Add(new SelectListItem
                {
                    Value = "",
                    Text = string.IsNullOrWhiteSpace(defaultOptionText) ? "Selecione" : defaultOptionText,
                    Selected = (selected == "")
                });

            if (dataDictionary?.Any() ?? false)
                foreach (var data in dataDictionary)
                    result.Add(new SelectListItem
                    {
                        Value = data.Key.ToString(),
                        Text = data.Value?.ToString(),
                        Selected = (selected == data.Key.ToString())
                    });

            if ((dataDictionary?.Count ?? 0) == 1 && selectIfSingleOption)
                result.LastOrDefault().Selected = true;

            return result;
        }
    }
}
