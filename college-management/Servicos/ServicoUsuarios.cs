using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Servicos.Interfaces;


namespace college_management.Servicos;


public class ServicoUsuarios : ServicoModelos<Usuario>
{
	public ServicoUsuarios(IRepositorioUsuarios repositorioUsuarios)
		: base(repositorioUsuarios)
	{
	}
}
