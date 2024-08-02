using college_management.Constantes;
using college_management.Modelos;

var teste = new Usuario("thiago.santos2003", "Thiago Rodrigues", new Cargo(CargosDeAcesso.CargoAlunos), "asdf");

Console.WriteLine(teste.ToString());