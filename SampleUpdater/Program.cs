using System;
using log4net;
using log4net.Config;
using System.IO;

namespace Sample
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {

            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            Log.Info("=== Iniciando prueba de actualización ===");
            var tempRoot = Path.Combine(Path.GetTempPath(), "SampleUpdaterTest");
            var sourcePath = Path.Combine(tempRoot, "origen");
            var installPath = Path.Combine(tempRoot, "destino");

            if (Directory.Exists(tempRoot)) Directory.Delete(tempRoot, true);

            Directory.CreateDirectory(sourcePath);
            Directory.CreateDirectory(installPath);

            var existingFile = Path.Combine(installPath, "data.txt");
            File.WriteAllText(existingFile, "Contenido original");

            File.WriteAllText(Path.Combine(sourcePath, "nuevo.add"), "Archivo nuevo agregado");
            File.WriteAllText(Path.Combine(sourcePath, "data.upd"), "Archivo actualizado");
            File.WriteAllText(Path.Combine(sourcePath, "data.del"), ""); // para que se elimine

            Log.Info($"Archivos preparados en {sourcePath} y {installPath}");
            MonitorUpdaterManagerSample.UpdateMonitor(sourcePath, installPath, "1.0.0");
            Log.Info("=== Prueba terminada ===");
        }
    }
}
