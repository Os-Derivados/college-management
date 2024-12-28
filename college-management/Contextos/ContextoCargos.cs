using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Views;


namespace college_management.Contextos;


public class ContextoCargos : Contexto<Cargo>
{
	public ContextoCargos(BaseDeDados baseDeDados,
	                      Usuario     usuarioContexto) :
		base(baseDeDados,
		     usuarioContexto) { }

	public override async Task Cadastrar() 
    {
        var temPermissao =
        CargoContexto.TemPermissao(PermissoesAcesso.AcessoEscrita) || 
        CargoContexto.TemPermissao(PermissoesAcesso.AcessoAdministradores);

        Cargo novoCargo = null;

        if (temPermissao)
        {
            novoCargo = TelaDeCadastro();

            if (novoCargo is null)
            {
                TelaErro("");
            }

            var resultado = await BaseDeDados.Cargos.Adicionar(novoCargo);

            if (resultado) Console.WriteLine("Cargo salvo com sucesso!");

            Console.ReadKey();
        }
    }

	public override async Task Editar() 
    {
        var temPermissao =
        CargoContexto.TemPermissao(PermissoesAcesso.AcessoEscrita) ||
        CargoContexto.TemPermissao(PermissoesAcesso.AcessoAdministradores);

        Cargo cargo = null;
        string nomeCargo = "";


        InputView inputView = new InputView("--Editor de cargos--");

        if(temPermissao)
        {
            nomeCargo = TelaSelecaoParaEditar(inputView);

            cargo = BaseDeDados.Cargos.ObterPorNome(nomeCargo);

            cargo = TelaDeEdicao(cargo, inputView);

            if (cargo is null)
            {
                TelaErro("Nome inválido");
                return;
            }

            if(await BaseDeDados.Cargos.Atualizar(cargo))
                inputView.LerEntrada("Sair", "Cargo Editado com sucesso");

        }
    }

	public override async Task Excluir() 
    {
        var temPermissao =
        CargoContexto.TemPermissao(PermissoesAcesso.AcessoEscrita) ||
        CargoContexto.TemPermissao(PermissoesAcesso.AcessoAdministradores);

        InputView inputView = new InputView("Exclusao de Cargo");
        string cargoNome = "";
        string cargoId = "";

        if(temPermissao)
        {
            cargoNome = TelaExclusao(inputView);
            cargoId = BaseDeDados.Cargos?.ObterPorNome(cargoNome)?.Id!; 

            if (cargoId is null)
            {
                TelaErro("Esse cargo não existe");

                return;
            }

            
            if (await BaseDeDados.Cargos.Remover(cargoId))
                inputView.LerEntrada("Sair", "Exclusão realizada com sucesso");

            
        }
    }

	public override void Visualizar()  
	{
        var temPermissao = CargoContexto.TemPermissao(PermissoesAcesso.AcessoAdministradores) ||
                                           CargoContexto.TemPermissao(PermissoesAcesso.AcessoEscrita);

        RelatorioView<Cargo> relatorioView;

        if (temPermissao)
        {
            relatorioView = new RelatorioView<Cargo>("Visualizar Usuários",
                                                       BaseDeDados.Cargos.ObterTodos());
        }

        else
            relatorioView = new RelatorioView<Cargo>("Visualizar Usuário", new List<Cargo>() 
            { 
                BaseDeDados.Cargos.ObterPorId(UsuarioContexto.CargoId) 
            });


        relatorioView.ConstruirLayout();
        relatorioView.Exibir();

        InputView inputRelatorio = new(relatorioView.Titulo);
        inputRelatorio.LerEntrada("Sair", relatorioView.Layout.ToString());
    }


	public override void VerDetalhes() 
    {
        var permissaoAdmin = CargoContexto.TemPermissao(PermissoesAcesso.AcessoAdministradores);


        if (permissaoAdmin)
        {
            OpcoesDeVisualizacao();

            Console.ReadKey();
        }
    }




    #region Metodos privados uteis

    Cargo TelaDeCadastro()
    {
        InputView inputUsuario = new("Cadastrar cargo");
        inputUsuario.ConstruirLayout();

        inputUsuario.LerEntrada("nome", "Insira o nome do novo cargo: ");

        if (ValidaEntrada(inputUsuario, "nome"))
        {

            List<string> nivelDePermissao = SelecaoDePermissao();
            string nomeCargo = inputUsuario.EntradasUsuario["nome"];

            if (nomeCargo is not null &&
                nivelDePermissao is not null)
            {
                return new Cargo(nomeCargo, nivelDePermissao);
            }

        }
        
        return null;
    }



	void OpcoesDeVisualizacao()
	{
        MenuView menuPesquisa = new("Cargos",
                                    "Selecione um dos campos:",
                                    ["Nome do Cargo", "Id"]);

        Cargo cargo = null!;
        DetalhesView detalhesView = null!;
        


        menuPesquisa.ConstruirLayout();
        menuPesquisa.LerEntrada();

        KeyValuePair<string, string>? campoPesquisa = menuPesquisa.OpcaoEscolhida switch
        {
            1 => new KeyValuePair<string, string>("Nome do Cargo",
                                                  "Insira o Nome do Cargo: "),
            2 => new KeyValuePair<string, string>("Id",
                                                  "Insira o Id do Cargo: "),

            _ => null
        };

        InputView inputPesquisa = new("Ver Detalhes: Pesquisar Cargo");

        if (campoPesquisa is null)
        {
            inputPesquisa.LerEntrada("Campo",
                                     "Campo inválido. Tente novamente.");

            return;
        }

        inputPesquisa.LerEntrada(campoPesquisa?.Key,
                                 campoPesquisa?.Value);

        

        if (menuPesquisa.OpcaoEscolhida is 1)
        {
            var nomeDoCargo = inputPesquisa.ObterEntrada("Nome do Cargo");
            cargo = BaseDeDados.Cargos.ObterPorNome(nomeDoCargo);
        }

        else if (menuPesquisa.OpcaoEscolhida is 2)
        {
            var id = inputPesquisa.ObterEntrada("Id");
            cargo = BaseDeDados.Cargos.ObterPorId(id);
        }

        if (cargo == null)
        {
            inputPesquisa.LerEntrada("Cargo",
                                     "Cargo não encontrado.");
            return;
        }

        ExibirDetalhesCargo(cargo, detalhesView);
    }


    List<string> SelecaoDePermissao()
    {
        List<string> permissoes = new List<string>();

        var propriedades = typeof(PermissoesAcesso).GetFields();
        string[] nomePropriedades = new string[propriedades.Length];
        int index = 0;

        for (int i = 0; i < propriedades.Length; i++)
        {
            nomePropriedades[i] = propriedades[i].Name;
            index++;
        }
        index = 0;
        

        while(index < nomePropriedades.Length)
        {
            MenuView menuView = new 
                MenuView("Permissões", "Selecione o nivel de permissão do cargo,", 
                nomePropriedades);

            menuView.ConstruirLayout();
            menuView.Exibir();
            menuView.LerEntrada();

            int opcao = menuView.OpcaoEscolhida-1;

            if (opcao >= nomePropriedades.Length || opcao < 0) break;

            if (!permissoes.Contains
                (nomePropriedades
                [opcao]))

                permissoes.Add(nomePropriedades[opcao]);



            index = opcao;
        }

        Console.Clear();
        return permissoes;
        
    }


    string TelaExclusao(InputView inputView)
    {

        inputView.LerEntrada("name", "Insira o nome do cargo a ser excluido do banco de dados: ");

        var nomeCargo = inputView.ObterEntrada("name");

        return nomeCargo;
    }

    string TelaSelecaoParaEditar(InputView inputView)
    {

        inputView.LerEntrada("name", "Insira o nome do cargo a ser editado: ");

        var cargoId = inputView.ObterEntrada("name");

        return cargoId;
    }


    Cargo TelaDeEdicao(Cargo cargoAtual, InputView inputView)
    {

        inputView.LerEntrada("nome",
            $"Nome Atual: {cargoAtual.Nome}\n\nEscolha um novo nome: ");

        if (ValidaEntrada(inputView, "nome"))
        {

            cargoAtual.Permissoes = SelecaoDePermissao();

            cargoAtual.Nome = inputView.ObterEntrada("nome");

            return cargoAtual;
        }

        return null;
    }

    bool ValidaEntrada(InputView inputView, string chave)
    {

        string item = inputView.ObterEntrada(chave);


        if (string.IsNullOrEmpty(item)) 
        {
            return false; 
        }

        else return true;
    }

    void TelaErro(string menssagem)
    {
        Console.Clear();

        Console.WriteLine(menssagem);
        Console.WriteLine("\t\t\n\n <--- Pressione alguma tecla para Sair --->");
        Console.ReadKey();
    }

    void ExibirDetalhesCargo(Cargo cargo, DetalhesView detalhesView)
    {
        var dicionario = new Dictionary<string, string>();
        string permissoes = ListaParaString(cargo.Permissoes);
        string usuarioId = ListaParaString(cargo.UsuariosIds);

        dicionario.Add("Id", cargo.Id);
        dicionario.Add("Nome", cargo.Nome);
        dicionario.Add("Permissões", permissoes);
        dicionario.Add("Ids de Usuários", usuarioId);

        detalhesView = new DetalhesView("Cargo", dicionario);

        detalhesView.ConstruirLayout();
        detalhesView.Exibir();
    }

    string ListaParaString(List<string> strings)
    {
        string output = "\n[\n";
        foreach (string str in strings) 
        {
            output += $"\t{str}\n";
        }

        output += "]";

        return output;
    }

    #endregion
}