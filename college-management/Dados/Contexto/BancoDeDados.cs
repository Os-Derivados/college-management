using college_management.Dados.Modelos;
using Microsoft.EntityFrameworkCore;


namespace college_management.Dados.Contexto;


public class BancoDeDados : DbContext
{
	public BancoDeDados(DbContextOptions<BancoDeDados> options) : base(options)
	{
	}

	public DbSet<Gestor> Gestores { get; set; }

	public DbSet<Docente> Docentes { get; set; }

	public DbSet<Aluno> Alunos { get; set; }

	public DbSet<Curso> Cursos { get; set; }

	public DbSet<Materia> Materias { get; set; }

	public DbSet<Matricula> Matriculas { get; set; }

	public DbSet<CorpoDocente> CorpoDocente { get; set; }

	public DbSet<Turma> Turmas { get; set; }

	public DbSet<GradeCurricular> GradeCurricular { get; set; }

	public DbSet<Avaliacao> Avaliacoes { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<Modelo>().HasKey(m => m.Id);
		builder.Entity<Modelo>()
		       .Property(m => m.Nome)
		       .HasMaxLength(128)
		       .IsRequired();

		// GESTOR <-> MODELO: 1:N
		builder.Entity<Gestor>()
		       .HasMany<Modelo>()
		       .WithOne(m => m.Gestor)
		       .HasForeignKey(m => m.GestorId);

		builder.Entity<Gestor>()
		       .Property(g => g.Cargo)
		       .HasDefaultValue(Cargo.Operador);

		builder.Entity<Usuario>()
		       .Property(u => u.Login)
		       .HasMaxLength(64)
		       .IsRequired();

		builder.Entity<Usuario>().Property(u => u.Credenciais).IsRequired();

		builder.Entity<Materia>().Property(m => m.CargaHoraria).IsRequired();

		// DOCENTE <-> TURMA: 1:N
		builder.Entity<Docente>()
		       .HasMany<Turma>()
		       .WithOne(t => t.Docente)
		       .HasForeignKey(t => t.DocenteId);

		// DOCENTE <-> MATERIA: N:N
		builder.Entity<Docente>()
		       .HasMany<Materia>()
		       .WithMany(m => m.Docentes)
		       .UsingEntity<CorpoDocente>();

		builder.Entity<Turma>().Property(t => t.Turno).IsRequired();

		// ALUNO <-> MATERIA: N:N
		builder.Entity<Aluno>()
		       .HasMany<Materia>()
		       .WithMany(m => m.Alunos)
		       .UsingEntity<Turma>();

		// ALUNO <-> CURSO: N:N
		builder.Entity<Aluno>()
		       .HasMany<Curso>()
		       .WithMany(c => c.Alunos)
		       .UsingEntity<Matricula>();

		// ALUNO <-> MATERIA: N:N
		builder.Entity<Aluno>()
		       .HasMany<Materia>()
		       .WithMany(m => m.Alunos)
		       .UsingEntity<Avaliacao>();

		builder.Entity<Avaliacao>()
		       .Property(a => a.Status)
		       .HasDefaultValue(StatusAvaliacao.EmAndamento);

		// CURSO <-> MATERIA: N:N
		builder.Entity<Curso>()
		       .HasMany<Materia>()
		       .WithMany(m => m.Cursos)
		       .UsingEntity<GradeCurricular>();
	}
}
