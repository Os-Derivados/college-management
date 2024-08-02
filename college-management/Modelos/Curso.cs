using System.Globalization;
using System.Text;

namespace college_management.Modelos;

public class Curso
{
    public Curso(string nome, Materia[] gradeCurricular)
    {
        Nome = nome;
        Id = _contagemId.ToString(CultureInfo.InvariantCulture); 
        GradeCurricular = gradeCurricular;
        
        _contagemId++;
    }
    
    private static double _contagemId = 10000000000;
    
    public string? Nome { get; set; }
    public readonly string Id;
    public Materia[] GradeCurricular { get; set; }

    public override string ToString()
    {
        StringBuilder builder = new();

        builder.AppendLine($"{Nome} - {CalcularCargaHoraria()}");
        builder.AppendLine("Matérias: ");

        foreach (var materia in GradeCurricular)
        {
            builder.AppendLine($"{materia.Nome} - {materia.CargaHoraria}");
        }

        return builder.ToString();
    }

    public double CalcularCargaHoraria()
    {
        return GradeCurricular.Sum(materia => materia.CargaHoraria);
    }
}