using System.Globalization;

namespace college_management.Dados.Modelos;

public sealed class Materia : Modelo
{
    public Materia(string nome, Turno turno, int cargaHoraria)
    {
        Nome = nome;
        Id = _contagemId.ToString(CultureInfo.InvariantCulture);
        Turno = turno;
        CargaHoraria = cargaHoraria;

        _contagemId++;
    }

    private static double _contagemId = 10000000000;

    public string? Nome { get; set; }
    public Turno Turno { get; set; }
    public int CargaHoraria { get; set; }
}

public enum Turno
{
    Matutino,
    Vespertino,
    Noturno,
    Integral
}