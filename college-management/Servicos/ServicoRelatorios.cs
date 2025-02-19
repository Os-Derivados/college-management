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

                           
                _arquivoRelatorios =Path.Combine(UtilitarioArquivos.DiretorioBase,
                                $"{typeof(T).Name}.csv");


		_usuario = usuario;
		_modelos = modelos;
	}




	public string GerarRelatorio(Cargo cargoUsuario)
	{
        if (cargoUsuario == null) throw new NullReferenceException(
                                                        $"Error: {typeof(Cargo).Name} é null. " +
                                                        $"(ServicoRelatorios<{typeof(T).Name}>.GerarRelatorio)"
                                                    );


        // Gera relatorio caso o Cargo tenha permissão.
        if ( cargoUsuario.TemPermissao(PermissoesAcesso.AcessoEscrita) || 
             cargoUsuario.TemPermissao(PermissoesAcesso.AcessoAdministradores) ) 
        {
            return GerarEntradasRelatorio();
        }


        throw new ArgumentException($"Error: Usuario não tem permissão para gerar relatorio. " +
                                                  $"OBS: Um usuario sem premissão não deveria ver a opção de " +
                                                  $"gerar relatorios. " +
                                                  $"(ServicoRelatorios<{typeof(T).Name}>.GerarRelatorio)"
                                             );
    }

	public string  GerarEntradasRelatorio()
	{
        if (_modelos.Count == 0)
            return "Nenhum registro encontrado.";

        else
        {
            
            var relatorio = new StringBuilder();
            var propriedades = typeof(T).GetProperties();


            //  Adiciona o cabeçalho à string CSV relatorio
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

                else // Adiciona os valores do registro à string CSV relatorio
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
        await File.WriteAllTextAsync(_arquivoRelatorios, relatorio);
	}
}
