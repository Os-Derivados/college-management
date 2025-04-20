using college_management.Dados.Modelos;
using Microsoft.EntityFrameworkCore;


namespace college_management.Dados.Contexto;


public class BancoDeDados : DbContext
{
	public BancoDeDados(DbContextOptions<BancoDeDados> options) :
			base(options) { }

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
		// Garantir hierarquia para a tabela de Usuarios
		builder.Entity<Usuario>()
		       .HasDiscriminator<string>("TipoUsuario")
		       .HasValue<Docente>("Docente")
		       .HasValue<Gestor>("Gestor")
		       .HasValue<Aluno>("Aluno");

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

		builder.Entity<Usuario>().Property(u => u.Senha).IsRequired();
		builder.Entity<Usuario>().Property(u => u.Sal).IsRequired();

		builder.Entity<Materia>().Property(m => m.CargaHoraria).IsRequired();

		// DOCENTE <-> TURMA: 1:N
		builder.Entity<Docente>()
		       .HasMany<Turma>()
		       .WithOne(t => t.Docente)
		       .HasForeignKey(t => t.DocenteId);

		builder.Entity<Turma>().Property(t => t.Turno).IsRequired();

		builder.Entity<Avaliacao>()
		       .Property(a => a.Status)
		       .HasDefaultValue(StatusAvaliacao.EmAndamento);

		// ALUNO <-> CURSO: N:N
		builder.Entity<Aluno>()
		       .HasMany(a => a.Cursos)
		       .WithMany(c => c.Alunos)
		       .UsingEntity<Matricula>(
			       l => l.HasOne<Curso>()
			             .WithMany()
			             .HasForeignKey(m => m.CursoId),
			       r => r.HasOne<Aluno>()
			             .WithMany()
			             .HasForeignKey(m => m.AlunoId)
		       );

		// ALUNO <-> MATERIA: N:N com avaliações
		builder.Entity<Aluno>()
		       .HasMany(a => a.Materias)
		       .WithMany(m => m.Alunos)
		       .UsingEntity<Avaliacao>(
			       l => l.HasOne<Materia>()
			             .WithMany()
			             .HasForeignKey(a => a.MateriaId),
			       r => r.HasOne<Aluno>()
			             .WithMany()
			             .HasForeignKey(a => a.AlunoId)
		       );

		// ALUNO <-> MATERIA: N:N com turmas
		builder.Entity<Aluno>()
		       .HasMany<Materia>()
		       .WithMany(m => m.Alunos)
		       .UsingEntity<Turma>(
			       l => l.HasOne<Materia>()
			             .WithMany()
			             .HasForeignKey(t => t.MateriaId),
			       r => r.HasOne<Aluno>()
			             .WithMany()
			             .HasForeignKey(t => t.AlunoId)
		       );

		// DOCENTE <-> MATERIA: N:N
		builder.Entity<Docente>()
		       .HasMany(d => d.Materias)
		       .WithMany(m => m.Docentes)
		       .UsingEntity<CorpoDocente>(
			       l => l.HasOne<Materia>()
			             .WithMany()
			             .HasForeignKey(cd => cd.MateriaId),
			       r => r.HasOne<Docente>()
			             .WithMany()
			             .HasForeignKey(cd => cd.DocenteId)
		       );

		// CURSO <-> MATERIA: N:N
		builder.Entity<Curso>()
		       .HasMany(c => c.Materias)
		       .WithMany(m => m.Cursos)
		       .UsingEntity<GradeCurricular>(
			       l => l.HasOne<Materia>()
			             .WithMany()
			             .HasForeignKey(gc => gc.MateriaId),
			       r => r.HasOne<Curso>()
			             .WithMany()
			             .HasForeignKey(gc => gc.CursoId)
		       );

		base.OnModelCreating(builder);
	}
}
