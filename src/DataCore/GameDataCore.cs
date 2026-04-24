using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FiveMTool.DataCore
{
    public class GameDataCore : IGameDataCore
    {
        public string GamePath { get; private set; }
        public bool IsInitialized { get; private set; }

        private Dictionary<string, string> _assetIndex; // Nombre -> Ruta RPF o Ruta Física

        public GameDataCore()
        {
            _assetIndex = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            IsInitialized = false;
        }

        public void Initialize(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"La ruta especificada no existe: {path}");

            if (!File.Exists(Path.Combine(path, "GTA5.exe")))
                throw new Exception("No se encontró GTA5.exe en la carpeta especificada. Asegúrate de seleccionar la raíz de GTA V.");

            GamePath = path;
            
            // Simulación de escaneo inicial para la Fase 1
            // En una implementación completa, aquí llamaríamos al RpfManager de CodeWalker
            ScanGameFolder();

            IsInitialized = true;
        }

        private void ScanGameFolder()
        {
            // Escaneo recursivo de archivos RPF para indexación
            // Nota: En la fase real, usaríamos la lógica de CodeWalker para entrar en los RPF
            var rpfFiles = Directory.GetFiles(GamePath, "*.rpf", SearchOption.AllDirectories);
            
            foreach (var rpf in rpfFiles)
            {
                string fileName = Path.GetFileName(rpf);
                if (!_assetIndex.ContainsKey(fileName))
                {
                    _assetIndex.Add(fileName, rpf);
                }
            }
        }

        public byte[] GetFileData(string fileName)
        {
            if (!IsInitialized) throw new InvalidOperationException("El núcleo no ha sido inicializado.");

            if (_assetIndex.TryGetValue(fileName, out string path))
            {
                // Si es un archivo físico directo
                if (File.Exists(path))
                    return File.ReadAllBytes(path);
                
                // Si estuviera dentro de un RPF, aquí usaríamos el extractor de CodeWalker
            }

            return null;
        }

        public IEnumerable<string> GetFilesByType(string extension)
        {
            if (!IsInitialized) return Enumerable.Empty<string>();

            return _assetIndex.Keys.Where(k => k.EndsWith(extension, StringComparison.OrdinalIgnoreCase));
        }
    }
}
