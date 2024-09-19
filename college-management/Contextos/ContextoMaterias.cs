using college_management.Contextos.Interfaces;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public class ContextoMaterias : Contexto<Materia>
{
    public override async Task Cadastrar(
        Repositorio<Materia> repositorio,
        Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override async Task Editar(Repositorio<Materia> repositorio,
                                      Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override async Task Excluir(Repositorio<Materia> repositorio,
                                       Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override void Visualizar(Repositorio<Materia> repositorio,
                                    Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
