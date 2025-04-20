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
		// Cada modelo terá a sua própria tabela
		builder.Entity<Rastreavel>().UseTpcMappingStrategy();
		
		builder.Entity<Rastreavel>()
		       .HasOne<Gestor>()
		       .WithMany(gestor => gestor.Modelos)
		       .HasForeignKey(modelo => modelo.GestorId);

		base.OnModelCreating(builder);
	}
}
