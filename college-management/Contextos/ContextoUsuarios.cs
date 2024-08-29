using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public class ContextoUsuarios : Contexto<Usuario>,
                                IContextoUsuarios
{
    public async Task EditarMatricula(Repositorio<Usuario> repositorio,
                                      Usuario usuario)
    {
        var usuarioTemPermissao =
            usuario.Cargo.VerificarPermissao(
                PermissoesAcesso.PermissaoAcessoEscrita);

        if (!usuarioTemPermissao)
        {
            Console.WriteLine("Você não tem permissão para realizar esta ação");

            return;
        }
        
        throw new NotImplementedException();
    }

    public void VerMatricula(Repositorio<Usuario> repositorio,
                             Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void VerBoletim(Repositorio<Usuario> repositorio,
                           Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void VerFinanceiro(Repositorio<Usuario> repositorio,
                              Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
