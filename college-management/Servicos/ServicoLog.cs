namespace college_management.Servicos
{
    public class ServicoLog
    {
        private readonly string _logDiretorio;
        private readonly string _logArquivo;

        public ServicoLog(string logDiretorio = "logs")
        {
            _logDiretorio = logDiretorio;

            if (!Directory.Exists(_logDiretorio))
            {
                Directory.CreateDirectory(_logDiretorio);
            }
            
            _logArquivo = Path.Combine(_logDiretorio, "logfile.txt");
        }

        public void Log(string mensagem, Severidade severidade = Severidade.Info)
        {
            string logMensagem = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss} [{severidade}] - {mensagem}";

            File.AppendAllText(_logArquivo, logMensagem + Environment.NewLine);
        }

        public enum Severidade
        {
            Info,
            Aviso,
            Erro,
        }
    }
}
