using Mov.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mov.Domain.Dtos.Usuario
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public int Permissao { get; set; } 
        public bool Ativo { get; set; }
    }
}
