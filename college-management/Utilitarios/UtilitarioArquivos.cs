namespace college_management.Utilitarios;


public static class UtilitarioArquivos
{
	public static readonly string DiretorioBase
		= Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder
			                                     .ApplicationData),
			"OsDerivados",
			"CollegeManagement");

	public static readonly string DiretorioDados
		= Path.Combine(DiretorioBase, "Dados");

	public static void Inicializar()
	{
		string[] diretorios = [DiretorioBase, DiretorioDados];

		foreach (var diretorio in diretorios)
		{
			if (Directory.Exists(diretorio)) continue;

			Directory.CreateDirectory(diretorio);
		}
	}
}
