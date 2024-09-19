using college_management.Dados.Modelos;

namespace college_management.Dados.Repositorios;

public class RepositorioMaterias : Repositorio<Materia>
{
    public override bool Existe(Materia modelo)
    {
        var nomeExistente =
            BaseDeDados.FirstOrDefault(m => m.Nome == modelo.Nome);
        var idExistente = ObterPorId(modelo.Id);

        return ((nomeExistente is not null)
                || (idExistente is not null));
    }
}
