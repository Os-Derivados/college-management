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

        public void Log(string message, LogLevel level = LogLevel.Info)
        {
            string logMensagem = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss} [{level}] - {message}";
            string logLocalArquivo = Path.Combine(_logDiretorio, "logfile.txt");

            File.AppendAllText(logLocalArquivo, logMensagem + Environment.NewLine);
        }

        public enum LogLevel
        {
            Info,
            Aviso,
            Erro,
        }
    }
}
