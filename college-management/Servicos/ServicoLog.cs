using System;
using System.IO;

namespace college_management.Utilitarios
{
    public class ServicoLog
    {
        private readonly string _logDiretorio;

        public ServicoLog(string logDiretorio = "logs")
        {
            _logDiretorio = logDiretorio;

            if (!Directory.Exists(_logDiretorio))
            {
                Directory.CreateDirectory(_logDiretorio);
            }
        }

        public void Log(string message, Severidade severidade = Severidade.Info)
        {
            string logMensagem = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss} [{severidade}] - {message}";
            string logLocalArquivo = Path.Combine(_logDiretorio, "logfile.txt");

            File.AppendAllText(logLocalArquivo, logMensagem + Environment.NewLine);
        }

        public enum Severidade
        {
            Info,
            Aviso,
            Erro,
        }
    }
}
