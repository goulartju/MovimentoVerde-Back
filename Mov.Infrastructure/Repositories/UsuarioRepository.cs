using Microsoft.EntityFrameworkCore;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Infrastructure.Data;

namespace Mov.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios.Where(u => u.Ativo).ToListAsync();
    }

    public async Task<Usuario?> GetByIdAsync(Guid id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        usuario.Id = Guid.NewGuid();
        usuario.CriadoEm = DateTime.UtcNow;
        usuario.Ativo = true;

        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        usuario.AtualizadoEm = DateTime.UtcNow;
        _context.Entry(usuario).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task DeleteAsync(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            usuario.Ativo = false;
            usuario.AtualizadoEm = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
