using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace college_management.Dados.Modelos;

[Table("Matriculas")]
public class Matricula : IRastreavel

{
	public Matricula(uint periodo, Modalidade modalidade)
	{
		Periodo = periodo;
		Modalidade = modalidade;
	}

	public Matricula() { }

	public uint? CursoId { get; set; }
	public virtual Curso? Curso { get; set; }
	public uint? AlunoId { get; set; }
	public virtual Aluno? Aluno { get; set; }

	[NotMapped]
	public string Codigo => $"{CursoId}{AlunoId}";

	[Required]
	public uint Periodo { get; set; }

	[Required]
	[DefaultValue(Modalidade.Presencial)]
	public Modalidade Modalidade { get; set; }

	public string? CriadoPor { get; set; }
	public string? EditadoPor { get; set; }
	public DateTime? CriadoEm { get; set; }
	public DateTime? EditadoEm { get; set; }

	public static Matricula CriarMatricula(Dictionary<string, string> cadastro)
	{
		var periodoValido = uint.TryParse(cadastro["Periodo"],
										  out var periodoCurso);

		if (!periodoValido) return new Matricula();

		var modalidadeValida = Enum.TryParse<Modalidade>(
			cadastro["Modalidade"],
			out var modalidadeCurso);

		if (!modalidadeValida) return new Matricula();

		Matricula novaMatricula = new(periodoCurso, modalidadeCurso);

		return novaMatricula;
	}
}

public enum Modalidade
{
	Presencial,
	Ead,
	Hibrido
}
