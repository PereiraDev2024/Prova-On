namespace ApiProvaOnline.Model
{
    public class Professor
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

        public DateTime DataHoraRegistro { get; set; }

        // Navigation properties
        public ICollection<Prova> Provas { get; set; } = new List<Prova>();
    }
}
