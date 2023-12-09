using System.ComponentModel.DataAnnotations;

namespace TCC_Web.Models.Enums.Parking
{
    public enum OpeningDay : int
    {
        [Display(Name = "Desconhecido")]
        Unknown = 0,
        [Display(Name = "Domingo")]
        Sunday = 1,
        [Display(Name = "Segunda-Feira")]
        Monday = 2,
        [Display(Name = "Terça-Feira")]
        Tuesday = 3,
        [Display(Name = "Quarta-Feira")]
        Wednesday = 4,
        [Display(Name = "Quinta-Feira")]
        Thursday = 5,
        [Display(Name = "Sexta-Feira")]
        Friday = 6,
        [Display(Name = "Sábado")]
        Saturday = 7
    }
}