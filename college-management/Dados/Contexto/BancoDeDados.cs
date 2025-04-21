using college_management.Dados.Modelos;
using Microsoft.EntityFrameworkCore;


namespace college_management.Dados.Contexto;


public class BancoDeDados : DbContext
{
	public BancoDeDados(DbContextOptions<BancoDeDados> options) : base(options)
	{
	}

	public DbSet<Aluno>           Alunos          { get; set; }
	public DbSet<Avaliacao>       Avaliacoes      { get; set; }
	public DbSet<CorpoDocente>    CorpoDocente    { get; set; }
	public DbSet<Curso>           Cursos          { get; set; }
	public DbSet<Docente>         Docentes        { get; set; }
	public DbSet<Gestor>          Gestores        { get; set; }
	public DbSet<GradeCurricular> GradeCurricular { get; set; }
	public DbSet<Materia>         Materias        { get; set; }
	public DbSet<Matricula>       Matriculas      { get; set; }
	public DbSet<Turma>           Turmas          { get; set; }
	public DbSet<Usuario>         Usuarios        { get; set; }


	protected override void OnModelCreating(ModelBuilder builder)
	{
		// Cada modelo terá a sua própria tabela: Table per Concrete Type
		builder.Entity<Rastreavel>().UseTpcMappingStrategy();

		builder.Entity<Rastreavel>()
		       .HasOne<Gestor>()
		       .WithMany(gestor => gestor.Modelos)
		       .HasForeignKey(modelo => modelo.GestorId);

		#region Turma

		builder.Entity<Docente>()
		       .HasMany<Turma>()
		       .WithOne(turma => turma.Docente)
		       .HasForeignKey(turma => turma.DocenteId);

		builder.Entity<Turma>()
		       .HasMany<Aluno>()
		       .WithMany(aluno => aluno.Turmas)
		       .UsingEntity<TurmaAluno>();

		builder.Entity<Turma>()
		       .HasOne<Materia>()
		       .WithMany(materia => materia.Turmas)
		       .HasForeignKey(turma => turma.MateriaId);

		#endregion

		#region Materia

		builder.Entity<Materia>()
		       .HasMany<Aluno>()
		       .WithMany(aluno => aluno.Materias)
		       .UsingEntity<Avaliacao>();

		builder.Entity<Materia>()
		       .HasMany<Docente>()
		       .WithMany(docente => docente.Materias)
		       .UsingEntity<CorpoDocente>();

		builder.Entity<Materia>()
		       .HasMany<Curso>()
		       .WithMany(curso => curso.Materias)
		       .UsingEntity<GradeCurricular>();

		#endregion

		#region Curso

		builder.Entity<Curso>()
		       .HasMany<Aluno>()
		       .WithMany(aluno => aluno.Cursos)
		       .UsingEntity<Matricula>();

		#endregion

		base.OnModelCreating(builder);
	}
}
