using System.Text;
using college_management.Constantes;
using college_management.Modelos;

namespace college_management.Servicos;

public static class ServicoDePermissoes
{
    public static void ListarContextos(Usuario usuario)
    {
        StringBuilder contextos = new();
        contextos.AppendLine("0. Sair");

        switch (usuario.Cargo.Nome)
        {
            case CargosDeAcesso.CargoAlunos:
                contextos.AppendLine("1. Cursos");

                break;
            
            case CargosDeAcesso.CargoGestores or CargosDeAcesso.CargoAdministradores:
                contextos.AppendLine("1. Cursos");
                contextos.AppendLine("2. Cadastros");

                break;
            
            default:
                throw new InvalidOperationException("O usuário não possui um cargo válido");
        }
        
        Console.WriteLine(contextos);
    }
}