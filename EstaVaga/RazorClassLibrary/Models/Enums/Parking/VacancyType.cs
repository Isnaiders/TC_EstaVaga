using System.ComponentModel.DataAnnotations;

namespace RazorClassLibrary.Models.Enums.Parking
{
    public enum VacancyType : int
    {
        [Display(Name = "Não Definido")]
        Undefined = 0,
        [Display(Name = "Deficiente")]
        Deficient = 1
    }
}