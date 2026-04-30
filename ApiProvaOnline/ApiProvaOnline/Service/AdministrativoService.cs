using ApiProvaOnline.Application.Interfaces;
using ApiProvaOnline.Model;

namespace ApiProvaOnline.Service
{
    public class AdministrativoService
    {
        private readonly IAdministrativoRepository _repository;

        public AdministrativoService(IAdministrativoRepository repository)
        {
            _repository = repository;
        }

        public async Task CadastrarAdministrativoAsync(Administrativo administrativo)
        {
            administrativo.DataHoraRegistro = DateTime.Now;
            await _repository.AddAsync(administrativo);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> RemoverAdministrativoAsync(int id)
        {
            var administrativo = await _repository.GetByIdAsync(id);
            if (administrativo == null) return false;

            _repository.Remove(administrativo);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<Administrativo?> BuscarAdministrativoAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Administrativo>> ListarAdministrativoAsync()
        {
            var administrativos = await _repository.GetAllAsync();
            return administrativos.OrderBy(a => a.Nome);
        }

        public async Task<bool> AtualizarAdministrativoAsync(Administrativo administrativo)
        {
            var existente = await _repository.GetByIdAsync(administrativo.Id);
            if (existente == null) return false;

            existente.Nome = administrativo.Nome;
            existente.Email = administrativo.Email;
            existente.Senha = administrativo.Senha;
            existente.DataHoraRegistro = administrativo.DataHoraRegistro;

            _repository.Update(existente);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
