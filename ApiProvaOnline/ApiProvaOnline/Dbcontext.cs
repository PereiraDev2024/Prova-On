using ApiProvaOnline.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiProvaOnline.Infrastructure.Data
{
    public class ProvaOnDbContext : DbContext
    {
        public ProvaOnDbContext(DbContextOptions<ProvaOnDbContext> options) : base(options) { }

        public DbSet<Prova> Provas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Administrativo> Administrativos { get; set; }
        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<PerguntaObjetiva> PerguntasObjetivas { get; set; }
        public DbSet<AlunoResposta> AlunoRespostas { get; set; }
        public DbSet<AlunoRespostaObjetiva> AlunoRespostasObjetivas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.ToTable("Professor");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Senha).HasMaxLength(150).IsRequired();
            });

            modelBuilder.Entity<Administrativo>(entity =>
            {
                entity.ToTable("Administrativo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Senha).HasMaxLength(150).IsRequired();
                entity.Property(e => e.DataHoraRegistro).HasColumnName("DataHoraRegistro");
            });

            modelBuilder.Entity<Prova>(entity =>
            {
                entity.ToTable("Prova");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Codigo).HasMaxLength(45).IsRequired();
                entity.HasOne(e => e.Professor)
                      .WithMany(p => p.Provas)
                      .HasForeignKey(e => e.IdProfessor)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.ToTable("Aluno");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
                entity.HasOne(e => e.Prova)
                      .WithMany(p => p.Alunos)
                      .HasForeignKey(e => e.IdProva)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Pergunta>(entity =>
            {
                entity.ToTable("Pergunta");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).HasMaxLength(500).IsRequired();
                entity.Property(e => e.Descricao).HasMaxLength(500).IsRequired();
                entity.HasOne(e => e.Prova)
                      .WithMany(p => p.Perguntas)
                      .HasForeignKey(e => e.IdProva)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PerguntaObjetiva>(entity =>
            {
                entity.ToTable("PerguntaObjetiva");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).HasMaxLength(500).IsRequired();
                entity.Property(e => e.Descricao).HasMaxLength(500).IsRequired();
                entity.Property(e => e.OpcaoA).HasMaxLength(500).IsRequired();
                entity.Property(e => e.OpcaoB).HasMaxLength(500).IsRequired();
                entity.Property(e => e.OpcaoC).HasMaxLength(500).IsRequired();
                entity.Property(e => e.OpcaoD).HasMaxLength(500).IsRequired();
                entity.Property(e => e.OpcaoCorreta).HasColumnType("char(1)").IsRequired();
                entity.HasOne(e => e.Prova)
                      .WithMany(p => p.PerguntasObjetivas)
                      .HasForeignKey(e => e.IdProva)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AlunoResposta>(entity =>
            {
                entity.ToTable("AlunoResposta");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Resposta).HasMaxLength(500).IsRequired();
                entity.HasOne(e => e.Aluno)
                      .WithMany(a => a.Respostas)
                      .HasForeignKey(e => e.IdAluno)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Pergunta)
                      .WithMany(p => p.Respostas)
                      .HasForeignKey(e => e.IdPergunta)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AlunoRespostaObjetiva>(entity =>
            {
                entity.ToTable("AlunoRespostaObjetiva");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Resposta).HasColumnType("char(1)").IsRequired();
                entity.Property(e => e.DataHorRegistro).HasColumnName("DataHoraRegistro");
                entity.HasOne(e => e.Aluno)
                      .WithMany(a => a.RespostasObjetivas)
                      .HasForeignKey(e => e.IdAluno)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.PerguntaObjetiva)
                      .WithMany(p => p.RespostasObjetivas)
                      .HasForeignKey(e => e.IdPerguntaObjetiva)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
