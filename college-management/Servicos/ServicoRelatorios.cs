using System.Reflection;
using System.Text;
using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Servicos.Interfaces;
using college_management.Utilitarios;
using Microsoft.VisualBasic.FileIO;


namespace college_management.Servicos;


public sealed class ServicoRelatorios<T> : IServicoRelatorios<T>
where T : Modelo
{
	private readonly string  _arquivoRelatorios;
	private readonly Usuario _usuario;
	private readonly List<T> _modelos;

	public ServicoRelatorios(Usuario usuario,
	                         List<T> modelos)
	{
		_arquivoRelatorios
			= Path.Combine(SpecialDirectories.MyDocuments,
			               "OsDerivados",
			               "CollegeManagement",
			               "Relatorios",
			               $"{typeof(T).Name}.csv");

		_usuario = usuario;
		_modelos = modelos;
	}

	public string GerarRelatorio(T modelo, Cargo? cargoUsuario)
	{
		return cargoUsuario
			       .TemPermissao(PermissoesAcesso.AcessoEscrita)
			       ? GerarEntradasRelatorio()
			       : modelo.ToString();
	}

	public string GerarEntradasRelatorio()
	{
        if (_modelos.Count == 0)
            return "Nenhum registro encontrado.";

        else
        {
            var relatorio = new StringBuilder();
            var propriedades = typeof(T).GetProperties();


            //  Adiciona o cabecalho à string CSV
            foreach (var propriedade in propriedades)
            {
                if (propriedades.Last() == propriedade)
                    relatorio.Append($"{propriedade.Name}\n");

                else
                    relatorio.Append($"{propriedade.Name},");
            }

            // Adiciona os valores à string CSV
            foreach (var modelo in _modelos)
            {
                if (modelo == null)
                    relatorio.Append("Registro nulo\n");

                else // Adiciona os valores do registro à string CSV
                {
                    foreach (var propriedade in propriedades)
                    {
                        if (propriedades.Last() == propriedade)
                            relatorio.Append($"{propriedade.GetValue(modelo)}\n");
                        
                        else
                            relatorio.Append($"{propriedade.GetValue(modelo)},");
                    }
                }
            }

            return relatorio.ToString();
        }
    }

	public async Task ExportarRelatorio(string relatorio)
	{
		// TODO: Implementar um algoritmo para exportar relatórios no formato CSV

		throw new NotImplementedException();
	}
}
