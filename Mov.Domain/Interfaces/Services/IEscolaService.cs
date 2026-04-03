using Mov.Domain.Dtos.Escola;
using Mov.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mov.Domain.Interfaces.Services
{
    public interface IEscolaService
    {
        Task<IEnumerable<Escola>> GetAllAsync();
        Task<Escola?> GetByIdAsync(Guid id);
        Task<Escola> CreateAsync(CreateEscolaDto dto);
        Task<Escola> UpdateAsync(UpdateEscolaDto dto);
        Task DeleteAsync(Guid id);
    }
}
