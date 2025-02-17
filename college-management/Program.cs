﻿using college_management.Dados;
using college_management.Middlewares;
using college_management.Utilitarios;
using college_management.Views;

Console.Clear();

UtilitarioArquivos.Inicializar();

BaseDeDados baseDeDados = new();

if (!bool.TryParse(args[1], out var seed))
	View.Aviso(
		"Aviso : Argumento \"seed\" não informado ou incorreto, utilizando o valor padrão : false");

if (seed)
{
	await UtilitarioSeed.IniciarBaseDeDados(baseDeDados);
}
else if (!UtilitarioSeed.ValidarDadosIniciais(baseDeDados))
{
	View.Aviso(
		"Base de Dados não inicializada com valores padrão. Execute o programa novamente com o argumento seed definido como true");

	return;
}

if (!bool.TryParse(args[0], out var modoDesenvolvimento))
	View.Aviso(
		"Aviso : Argumento modoDesenvolvimento não informado ou incorreto, utilizando o valor padrão : false");

var usuarioLogado =
	MiddlewareAutenticacao.Autenticar(modoDesenvolvimento,
	                                  baseDeDados.Usuarios);

MiddlewareContexto.Inicializar(baseDeDados, usuarioLogado);

Console.Clear();
Console.WriteLine("Saindo...");

public enum EstadoDoApp
{
	Sair,
	Login,
	Contexto,
	Recurso
}
