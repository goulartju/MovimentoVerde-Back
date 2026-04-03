using Mov.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mov.Domain.Interfaces.Repositories
{
    public interface IEscolaRepository
    {
        Task<IEnumerable<Escola>> GetAllAsync();
        Task<Escola?> GetByIdAsync(Guid id);
        Task<Escola> CreateAsync(Escola escola);
        Task<Escola> UpdateAsync(Escola escola);
        Task DeleteAsync(Guid id);
    }
}
