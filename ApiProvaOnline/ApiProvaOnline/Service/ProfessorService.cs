using ApiProvaOnline.Application.Interfaces;
using ApiProvaOnline.Model;

namespace ApiProvaOnline.Service
{
    public class ProfessorService
    {
        private readonly IProfessorRepository _repository;

        public ProfessorService(IProfessorRepository repository)
        {
            _repository = repository;
        }

        public async Task CadastrarProfessorAsync(Professor professor)
        {
            professor.DataHoraRegistro = DateTime.Now;
            await _repository.AddAsync(professor);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> RemoverProfessorAsync(int id)
        {
            var professor = await _repository.GetByIdAsync(id);
            if (professor == null) return false;

            _repository.Remove(professor);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<Professor?> BuscarProfessorAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Professor>> ListarProfessoresAsync()
        {
            var professores = await _repository.GetAllAsync();
            return professores.OrderBy(p => p.Nome);
        }

        public async Task<bool> AtualizarProfessorAsync(Professor professor)
        {
            var existente = await _repository.GetByIdAsync(professor.Id);
            if (existente == null) return false;

            existente.Nome = professor.Nome;
            existente.Email = professor.Email;
            existente.Senha = professor.Senha;
            existente.DataHoraRegistro = professor.DataHoraRegistro;

            _repository.Update(existente);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<Professor?> LoginProfessorAsync(string email, string senha)
        {
            return await _repository.LoginAsync(email, senha);
        }
    }
}
