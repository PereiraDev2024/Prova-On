namespace ApiProvaOnline.Model
{
    public class AlunoRespostaObjetiva
    {
        public int Id { get; set; }
        public int IdAluno { get; set; }
        public int IdPerguntaObjetiva { get; set; }
        public char Resposta { get; set; }
        public double PontuacaoFinal { get; set; }
        public DateTime DataHorRegistro { get; set; }

        // Navigation properties
        public Aluno? Aluno { get; set; }
        public PerguntaObjetiva? PerguntaObjetiva { get; set; }
    }
}
