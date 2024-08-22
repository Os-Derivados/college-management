using System.Text;
using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Servicos.Interfaces;
using Microsoft.VisualBasic.FileIO;

namespace college_management.Servicos;

public sealed class ServicoRelatorios<T> : IServicoRelatorios<T>
    where T : Modelo
{
    private readonly string _arquivoRelatorios;
    private readonly Usuario _usuario;
    private readonly Repositorio<T> _repositorio;

    public ServicoRelatorios(Usuario usuario,
                             Repositorio<T> repositorio)
    {
        _arquivoRelatorios = Path.Combine(
            SpecialDirectories.MyDocuments,
            "OsDerivados",
            "CollegeManagement",
            "Relatorios",
            $"{typeof(T)}.csv");

        _usuario = usuario;
        _repositorio = repositorio;
    }

    public string GerarRelatorio(T modelo)
    {
        return _usuario.Cargo.Nome switch
        {
            CargosDeAcesso.CargoAlunos => modelo.ToString(),
            CargosDeAcesso.CargoGestores
                or CargosDeAcesso.CargoAdministradores =>
                GerarEntradasRelatorio(),
            _ => throw new InvalidOperationException(
                     "Não há modelo de relatório disponível para este cargo")
        };
    }

    private string GerarEntradasRelatorio()
    {
        var modelos = _repositorio.ObterTodos();

        if (modelos.Count is 0)
            return modelos[0].ToString();

        StringBuilder entradasRelatorio = new();

        entradasRelatorio.AppendLine(
            modelos[0].ObterNomesPropriedades());

        foreach (var modelo in modelos)
            entradasRelatorio.AppendLine(modelo.ToString());

        return entradasRelatorio.ToString();
    }

    public async Task ExportarRelatorio(string relatorio)
    {
        throw new NotImplementedException();
    }
}
