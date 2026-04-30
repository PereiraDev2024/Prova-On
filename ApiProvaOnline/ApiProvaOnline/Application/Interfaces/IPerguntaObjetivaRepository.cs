using ApiProvaOnline.Model;

namespace ApiProvaOnline.Application.Interfaces
{
    public interface IPerguntaObjetivaRepository : IRepository<PerguntaObjetiva>
    {
        Task<IEnumerable<PerguntaObjetiva>> GetByProvaIdAsync(int provaId);
    }
}
