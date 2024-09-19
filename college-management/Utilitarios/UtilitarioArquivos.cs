namespace college_management.Utilitarios;

public static class UtilitarioArquivos
{
    public static readonly string DiretorioBase =
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData),
            "OsDerivados",
            "CollegeManagement");

    public static readonly string DiretorioDados =
        Path.Combine(DiretorioBase, "Dados");
    
    public static readonly string DiretorioLayouts =
        Path.Combine(DiretorioBase, "Layouts");
    
    public static void Incializar()
    {
        string[] diretorios =
            [DiretorioBase, DiretorioDados, DiretorioLayouts];

        foreach (var diretorio in diretorios)
        {
            if (Directory.Exists(diretorio)) continue;
            
            Directory.CreateDirectory(diretorio);
        }
    }
}
