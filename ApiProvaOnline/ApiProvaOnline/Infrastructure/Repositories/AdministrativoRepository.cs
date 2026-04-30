using ApiProvaOnline.Application.Interfaces;
using ApiProvaOnline.Infrastructure.Data;
using ApiProvaOnline.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiProvaOnline.Infrastructure.Repositories
{
    public class AdministrativoRepository : GenericRepository<Administrativo>, IAdministrativoRepository
    {
        public AdministrativoRepository(ProvaOnDbContext context) : base(context) { }

        public async Task<Administrativo?> GetByEmailAsync(string email)
        {
            return await _context.Administrativos
                .FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
