using System;
using System.Collections.Generic;
using System.Text;

namespace Mov.Domain.Dtos.Escola
{
    public class UpdateEscolaDto
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Municipio { get; set; }
        public required string Endereco { get; set; }
        public required string Diretor { get; set; }
        public Boolean Ativo { get; set; }
    }
}
