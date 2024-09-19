using college_management.Contextos.Interfaces;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public class ContextoCargos : Contexto<Cargo>
{
    public override async Task Cadastrar(Repositorio<Cargo> repositorio,
                                         Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override async Task Editar(Repositorio<Cargo> repositorio,
                                      Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override async Task Excluir(Repositorio<Cargo> repositorio,
                                       Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override void Visualizar(Repositorio<Cargo> repositorio,
                                    Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
