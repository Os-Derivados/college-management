using System;
using System.IO;

namespace college_management.Utilitarios
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

        public void Log(string message, Severidade severidade = Severidade.Info)
        {
            string logMensagem = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss} [{severidade}] - {message}";

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
