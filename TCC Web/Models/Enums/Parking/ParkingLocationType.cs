using System.ComponentModel.DataAnnotations;

namespace TCC_Web.Models.Enums.Parking
{
    public enum ParkingLocationType : int
    {
        [Display(Name = "Não Definido")]
        Undefined = 0,
        [Display(Name = "Rua")]
        Street = 1,
        [Display(Name = "Shopping")]
        Shopping = 2,
        [Display(Name = "Aeroporto")]
        Airport = 3,
        [Display(Name = "Evento")]
        Event = 4,
        [Display(Name = "Educação")]
        Educação = 5,
        [Display(Name = "Supermercado")]
        Supermarket = 6,
        [Display(Name = "Hospital")]
        Hospital = 7
    }
}