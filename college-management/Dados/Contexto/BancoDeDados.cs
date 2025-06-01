using college_management.Dados.Modelos;
using Microsoft.EntityFrameworkCore;


namespace college_management.Dados.Contexto;


public class BancoDeDados : DbContext
{
	public BancoDeDados(DbContextOptions<BancoDeDados> options) : base(options)
	{
	}

	public DbSet<Curso> Cursos { get; set; }
	public DbSet<Materia> Materias { get; set; }
	public DbSet<Avaliacao> Avaliacoes { get; set; }
	public DbSet<CorpoDocente> CorpoDocente { get; set; }
	public DbSet<GradeCurricular> GradeCurricular { get; set; }
	public DbSet<Matricula> Matriculas { get; set; }
	public DbSet<Turma> Turmas { get; set; }
	public DbSet<TurmaAluno> TurmaAluno { get; set; }
	public DbSet<Usuario> Usuarios { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		#region EntityTables

		builder.Entity<Usuario>()
			   .HasDiscriminator<string>("Tipo")
			   .HasValue<Aluno>("Aluno")
			   .HasValue<Docente>("Docente")
			   .HasValue<Gestor>("Gestor");

		builder.Entity<Curso>().ToTable("Cursos");
		builder.Entity<Materia>().ToTable("Materias");

		#endregion

		#region JoinTables

		builder.Entity<CorpoDocente>().ToTable("CorposDocentes");
		builder.Entity<GradeCurricular>().ToTable("GradesCurriculares");
		builder.Entity<Matricula>().ToTable("Matriculas");
		builder.Entity<Turma>().ToTable("Turmas");
		builder.Entity<TurmaAluno>().ToTable("TurmaAlunos");
		builder.Entity<Avaliacao>().ToTable("Avaliacoes");

		#endregion

		#region Turma

		builder.Entity<Docente>()
			   .HasMany(d => d.Turmas)
			   .WithOne(turma => turma.Docente)
			   .HasForeignKey(turma => turma.DocenteId);

		builder.Entity<Turma>()
			   .HasOne(t => t.Materia)
			   .WithMany(materia => materia.Turmas)
			   .HasForeignKey(turma => turma.MateriaId);

		builder.Entity<TurmaAluno>()
			   .HasKey(ta => new { ta.TurmaId, ta.AlunoId });

		builder.Entity<TurmaAluno>()
			   .HasOne(ta => ta.Turma)
			   .WithMany(t => t.TurmaAlunos)
			   .HasForeignKey(ta => ta.TurmaId);

		builder.Entity<TurmaAluno>()
			   .HasOne(ta => ta.Aluno)
			   .WithMany(t => t.TurmaAlunos)
			   .HasForeignKey(ta => ta.AlunoId);

		#endregion

		#region Materia

		builder.Entity<Avaliacao>().HasKey(a => new { a.MateriaId, a.AlunoId });

		builder.Entity<Avaliacao>()
			   .HasOne(a => a.Materia)
			   .WithMany(c => c.Avaliacoes)
			   .HasForeignKey(m => m.MateriaId);

		builder.Entity<Avaliacao>()
			   .HasOne(a => a.Aluno)
			   .WithMany(a => a.Avaliacoes)
			   .HasForeignKey(m => m.AlunoId);

		builder.Entity<CorpoDocente>()
			   .HasKey(cd => new { cd.DocenteId, cd.MateriaId });

		builder.Entity<CorpoDocente>()
			   .HasOne(cd => cd.Docente)
			   .WithMany(d => d.CorpoDocente)
			   .HasForeignKey(cd => cd.DocenteId);

		builder.Entity<CorpoDocente>()
			   .HasOne(cd => cd.Materia)
			   .WithMany(m => m.CorpoDocente)
			   .HasForeignKey(cd => cd.MateriaId);

		builder.Entity<GradeCurricular>()
			   .HasKey(gc => new { gc.CursoId, gc.MateriaId });

		builder.Entity<GradeCurricular>()
			   .HasOne(gc => gc.Curso)
			   .WithMany(c => c.GradesCurriculares)
			   .HasForeignKey(gc => gc.CursoId);

		builder.Entity<GradeCurricular>()
			   .HasOne(gc => gc.Materia)
			   .WithMany(m => m.GradesCurriculares)
			   .HasForeignKey(gc => gc.MateriaId);
		#endregion

		#region Curso

		builder.Entity<Matricula>()
			   .HasKey(m => new { m.CursoId, m.AlunoId });

		builder.Entity<Matricula>()
			   .HasOne(m => m.Curso)
			   .WithMany(c => c.Matriculas)
			   .HasForeignKey(m => m.CursoId);

		builder.Entity<Matricula>()
			   .HasOne(m => m.Aluno)
			   .WithMany(a => a.Matriculas)
			   .HasForeignKey(m => m.AlunoId);

		#endregion

		base.OnModelCreating(builder);
	}

	public int Salvar(string userLogin)
	{
		var entries = ChangeTracker.Entries()
								   .Where(e => e.State is EntityState.Added
											  or EntityState.Modified);

		foreach (var entry in entries)
		{
			if (entry.State is EntityState.Added)
			{
				entry.Property("CriadoPor").CurrentValue = userLogin;
				entry.Property("CriadoEm").CurrentValue = DateTime.Now;
			}
			else if (entry.State is EntityState.Modified)
			{
				entry.Property("EditadoPor").CurrentValue = userLogin;
				entry.Property("EditadoEm").CurrentValue = DateTime.Now;
			}
		}

		return base.SaveChanges();
	}

	public override int SaveChanges()
	{
		return Salvar("System");
	}
}
