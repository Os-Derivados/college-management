using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
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

    	private readonly string _caminhoArquivo
		= Path.Combine(UtilitarioArquivos.DiretorioDados,
		               $"{typeof(T).Name}s.json");  //preciso saber o caminho dos arquivos json para converte-los

	public string GerarRelatorio(T modelo, Cargo? cargoUsuario)
	{
		return cargoUsuario
			       .TemPermissao(PermissoesAcesso.AcessoEscrita)
			       ? GerarEntradasRelatorio()
			       : modelo.ToString();
	}

	public string  GerarEntradasRelatorio()
	{
        if (_modelos.Count == 0)
            return "Nenhum registro encontrado.";

        else
        {

            var serializer = new DataContractJsonSerializer(typeof(List<T>));

              using var streamArquivo
			= File.OpenRead(_caminhoArquivo);

            //_modelos = JsonSerializer.DeserializeAsync<List<T>>(streamArquivo);
		    //var listaRelatorios =  JsonSerializer.DeserializeAsync<List<T>>(streamArquivo);

            var listaRelatorios = (List<T>)serializer.ReadObject(streamArquivo);

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
            foreach (var modelo in listaRelatorios)
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
                       File.WriteAllText(_arquivoRelatorios, relatorio.ToString());
            return relatorio.ToString();
        }
    }

	public async Task ExportarRelatorio(string relatorio)
	{
		// TODO: Implementar um algoritmo para exportar relatórios no formato CSV
        

	}
}
