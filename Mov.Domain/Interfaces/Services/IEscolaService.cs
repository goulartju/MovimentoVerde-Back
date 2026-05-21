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
        Task<EscolaResponse?> GetByIdAsync(Guid id);
        Task<EscolaResponse> CreateAsync(CreateEscolaDto dto);
        Task<EscolaResponse> UpdateAsync(UpdateEscolaDto dto);
        Task DeleteAsync(Guid id);
    }
}
