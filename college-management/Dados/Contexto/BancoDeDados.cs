using college_management.Dados.Modelos;
using Microsoft.EntityFrameworkCore;


namespace college_management.Dados.Contexto;


public class BancoDeDados : DbContext
{
	public BancoDeDados(DbContextOptions<BancoDeDados> options) : base(options)
	{
	}

	public DbSet<Curso>           Cursos          { get; set; }
	public DbSet<Materia>         Materias        { get; set; }
	public DbSet<Avaliacao>       Avaliacoes      { get; set; }
	public DbSet<CorpoDocente>    CorpoDocente    { get; set; }
	public DbSet<GradeCurricular> GradeCurricular { get; set; }
	public DbSet<Matricula>       Matriculas      { get; set; }
	public DbSet<Turma>           Turmas          { get; set; }
	public DbSet<TurmaAluno>      TurmaAluno      { get; set; }
	public DbSet<Aluno>           Alunos          { get; set; }
	public DbSet<Docente>         Docentes        { get; set; }
	public DbSet<Gestor>          Gestores        { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		// Cada modelo terá a sua própria tabela: Table per Concrete Type
		builder.Entity<Modelo>().UseTpcMappingStrategy();

		builder.Entity<Modelo>()
		       .HasOne<Gestor>()
		       .WithMany(gestor => gestor.Modelos)
		       .HasForeignKey(modelo => modelo.GestorId);

		builder.Entity<CorpoDocente>()
		       .HasOne<Gestor>()
		       .WithMany(gestor => gestor.CorposDocentes)
		       .HasForeignKey(modelo => modelo.GestorId);

		builder.Entity<Matricula>()
		       .HasOne<Gestor>()
		       .WithMany(gestor => gestor.Matriculas)
		       .HasForeignKey(modelo => modelo.GestorId);

		builder.Entity<Avaliacao>()
		       .HasOne<Gestor>()
		       .WithMany(gestor => gestor.Avaliacoes)
		       .HasForeignKey(modelo => modelo.GestorId);

		builder.Entity<GradeCurricular>()
		       .HasOne<Gestor>()
		       .WithMany(gestor => gestor.Grades)
		       .HasForeignKey(modelo => modelo.GestorId);

		#region Turma

		builder.Entity<Docente>()
		       .HasMany<Turma>()
		       .WithOne(turma => turma.Docente)
		       .HasForeignKey(turma => turma.DocenteId);

		builder.Entity<Turma>()
		       .HasOne<Materia>()
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

		builder.Entity<Turma>()
		       .HasMany(t => t.Alunos)
		       .WithMany(a => a.Turmas)
		       .UsingEntity<TurmaAluno>(
			       j => j.HasOne(ta => ta.Aluno).WithMany(a => a.TurmaAlunos),
			       j => j.HasOne(ta => ta.Turma).WithMany(t => t.TurmaAlunos));

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

		builder.Entity<Materia>()
		       .HasMany(m => m.Alunos)
		       .WithMany(a => a.Materias)
		       .UsingEntity<Avaliacao>(
			       j => j.HasOne(m => m.Aluno).WithMany(a => a.Avaliacoes),
			       j => j.HasOne(m => m.Materia).WithMany(m => m.Avaliacoes));

		builder.Entity<CorpoDocente>()
		       .HasKey(cd => new
			               { cd.DocenteId, cd.MateriaId }); // Composite key

		builder.Entity<CorpoDocente>()
		       .HasOne(cd => cd.Docente)
		       .WithMany(d => d.CorpoDocente)
		       .HasForeignKey(cd => cd.DocenteId);

		builder.Entity<CorpoDocente>()
		       .HasOne(cd => cd.Materia)
		       .WithMany(m => m.CorpoDocente)
		       .HasForeignKey(cd => cd.MateriaId);

		// Explicitly define the many-to-many relationship
		builder.Entity<Docente>()
		       .HasMany(d => d.Materias)
		       .WithMany(m => m.Docentes)
		       .UsingEntity<CorpoDocente>(
			       j => j.HasOne(cd => cd.Materia)
			             .WithMany(m => m.CorpoDocente),
			       j => j.HasOne(cd => cd.Docente)
			             .WithMany(d => d.CorpoDocente));

		// Configure GradeCurricular as the join table
		builder.Entity<GradeCurricular>()
		       .HasKey(gc => new { gc.CursoId, gc.MateriaId }); // Composite key

		builder.Entity<GradeCurricular>()
		       .HasOne(gc => gc.Curso)
		       .WithMany(c => c.GradesCurriculares)
		       .HasForeignKey(gc => gc.CursoId);

		builder.Entity<GradeCurricular>()
		       .HasOne(gc => gc.Materia)
		       .WithMany(m => m.GradesCurriculares)
		       .HasForeignKey(gc => gc.MateriaId);

		// Explicitly define the many-to-many relationship
		builder.Entity<Materia>()
		       .HasMany(m => m.Cursos)
		       .WithMany(c => c.Materias)
		       .UsingEntity<GradeCurricular>(
			       j => j.HasOne(gc => gc.Curso)
			             .WithMany(c => c.GradesCurriculares),
			       j => j.HasOne(gc => gc.Materia)
			             .WithMany(m => m.GradesCurriculares));

		#endregion

		#region Curso

		builder.Entity<Matricula>()
		       .HasKey(m => new { m.CursoId, m.AlunoId }); // Composite key

		builder.Entity<Matricula>()
		       .HasOne(m => m.Curso)
		       .WithMany(c => c.Matriculas)
		       .HasForeignKey(m => m.CursoId);

		builder.Entity<Matricula>()
		       .HasOne(m => m.Aluno)
		       .WithMany(a => a.Matriculas)
		       .HasForeignKey(m => m.AlunoId);

		// Ensure the many-to-many relationship is explicitly defined
		builder.Entity<Curso>()
		       .HasMany(c => c.Alunos)
		       .WithMany(a => a.Cursos)
		       .UsingEntity<Matricula>(
			       j => j.HasOne(m => m.Aluno).WithMany(a => a.Matriculas),
			       j => j.HasOne(m => m.Curso).WithMany(c => c.Matriculas));

		#endregion

		base.OnModelCreating(builder);
	}
}
