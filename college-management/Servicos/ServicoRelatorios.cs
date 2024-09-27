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
		// TODO: Implementar um algoritmo para converter registros JSON para o formato CSV
		throw new NotImplementedException();
	}

	public async Task ExportarRelatorio(string relatorio)
	{
		// TODO: Implementar um algoritmo para exportar relatórios no formato CSV
		
		throw new NotImplementedException();
	}
}
