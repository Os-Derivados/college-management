using college_management.Dados.Modelos;
using Microsoft.EntityFrameworkCore;


namespace college_management.Dados.BancoDeDados;


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
}
