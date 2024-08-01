using System.Globalization;
using System.Text;

namespace college_management.Modelos;

public class Curso
{
    private static double _contagemId = 10000000000;
    public readonly string Id;
    public string? Nome;
    private int _cargaHoraria;
    public Materia[] GradeCurricular;

    public Curso(string nome, Materia[] gradeCurricular)
    {
        Id = _contagemId.ToString(CultureInfo.InvariantCulture); 
        Nome = nome;
        GradeCurricular = gradeCurricular;

        foreach (var materia in gradeCurricular)
        {
            _cargaHoraria += materia.CargaHoraria;
        }

        _contagemId++;
    }

    public override string ToString()
    {
        StringBuilder builder = new();

        builder.AppendLine($"{Nome} - {_cargaHoraria}");
        builder.AppendLine("Matérias: ");

        foreach (var materia in GradeCurricular)
        {
            builder.AppendLine($"{materia.Nome} - {materia.CargaHoraria}");
        }

        return builder.ToString();
    }
}