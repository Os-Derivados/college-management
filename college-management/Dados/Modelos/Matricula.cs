namespace college_management.Dados.Modelos;

public sealed class Matricula : Modelo
{
    public Matricula(long numero,
                     int periodo,
                     Curso curso,
                     Modalidade modalidade)
    {
        Numero = numero;
        Periodo = periodo;
        Curso = curso;
        Modalidade = modalidade;
        Id = _contagemId.ToString();

        foreach (var materia in Curso.GradeCurricular)
        {
            Notas.Add(new Nota(materia.Nome, materia.Id));
        }

        _contagemId++;
    }

    private static long _contagemId = 10000000000;

    public long Numero { get; set; }
    public int Periodo { get; set; }
    public Curso Curso { get; set; }
    public Modalidade Modalidade { get; set; }
    public List<Nota> Notas { get; set; } = [];
}

public enum Modalidade
{
    Presencial,
    Ead,
    Hibrido
}

