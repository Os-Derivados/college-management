using System.Globalization;

namespace college_management.Modelos;

public enum Turno
{
    Matutino, Vespertino, Noturno, Integral
}

public class Materia
{
    private static double _contagemId = 10000000000;
    public readonly string Id;
    public string? Nome;
    public readonly int CargaHoraria;
    public Turno Turno;

    public Materia(string nome, int cargaHoraria, Turno turno)
    {
        Id = _contagemId.ToString(CultureInfo.InvariantCulture);
        Nome = nome;
        CargaHoraria = cargaHoraria;
        Turno = turno;

        _contagemId++;
    }
}