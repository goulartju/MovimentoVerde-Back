using System.ComponentModel.DataAnnotations;

namespace Mov.Domain.Enums;

public enum PermissaoEnum
{
    [Display(Name = "Visualizador")]
    Visualizador = 0,
    [Display(Name = "Editor")]
    Editor = 1,
    [Display(Name = "Administrador")]
    Administrador = 2
}
