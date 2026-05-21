using System.ComponentModel.DataAnnotations;

namespace Mov.Domain.Enums
{
    public enum TurnoEnum
    {
        [Display(Name = "Manhã")]
        MANHA = 1,

        [Display(Name = "Tarde")]
        TARDE = 2,

        [Display(Name = "Noite")]
        NOITE = 3,
    }
}
