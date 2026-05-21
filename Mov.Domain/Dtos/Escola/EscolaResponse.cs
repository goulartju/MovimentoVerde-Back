using System;
using System.Collections.Generic;
using System.Text;

namespace Mov.Domain.Dtos.Escola;

public class EscolaResponse
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Municipio { get; set; }
    public required string Contato { get; set; }
    public required string Diretor { get; set; }
    public Boolean Ativo { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }
}

