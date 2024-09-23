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

	private string GerarEntradasRelatorio()
	{
		if (_modelos.Count is 0)
			return _modelos[0].ToString();

		StringBuilder entradasRelatorio = new();
		
		entradasRelatorio
			.AppendLine(UtilitarioTipos
				            .ObterNomesPropriedades(typeof(T)
					                                    .GetProperties()));

		foreach (var modelo in _modelos)
			entradasRelatorio.AppendLine(modelo.ToString());

		return entradasRelatorio.ToString();
	}
	
	public async Task ExportarRelatorio(string relatorio)
	{
		throw new NotImplementedException();
	}
}
