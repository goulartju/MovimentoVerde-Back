using System;
using System.Collections.Generic;

namespace Mov.Domain.Entities
{
    public class Escola
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Municipio { get; set; }
        public required string Endereco { get; set; }
        public required string Diretor { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }

        // Propriedades de navegação
        public ICollection<Calendario> Calendarios { get; set; } = new List<Calendario>();
        public ICollection<Turma> Turmas { get; set; } = new List<Turma>();
    }
}
