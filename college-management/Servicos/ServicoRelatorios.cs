using System.Collections;
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
        DateTime TimeNow = DateTime.UtcNow;
                           
                _arquivoRelatorios =Path.Combine(UtilitarioArquivos.DiretorioBase,
                                $@"Relatorios\{typeof(T).Name}_{TimeNow.ToString("dd-MM-yy_H-mm-ss")}.csv");


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
            Console.WriteLine("Gerando relatorio...");
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
                    continue;

                else // Adiciona os valores do registro à string CSV relatorio
                {
                    foreach (var propriedade in propriedades)
                    {
                        var _ = propriedade.GetValue(modelo);

                        if (_ is Array a)
                        {
                            relatorio.Append(FormatarEnumerables(a));
                        }

                        else if (_ is IList l)
                        {
                            relatorio.Append(FormatarEnumerables(l));
                        }

                        else
                            relatorio.Append(_);


                        if (propriedades.Last() == propriedade)
                            relatorio.Append("\n");
                        
                        else
                            relatorio.Append(",");
                    }
                }
            }

            return relatorio.ToString();



            static string FormatarEnumerables(IEnumerable enumerable)
            {
                StringBuilder Str = new();
                foreach (var v in enumerable)
                {
                    if (v is Materia m)
                        Str.Append(m.Nome);
                    else
                        Str.Append(v);
                    
                    Str.Append(",");
                }
                Str.Remove(Str.Length - 1, 1);

                return Str.ToString();
            }
        }
    }

	public async Task ExportarRelatorio(string relatorio)
    {
        await File.WriteAllTextAsync(_arquivoRelatorios, relatorio);
	}
}
