using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeWalker.GameFiles;

namespace FiveMTool.DataCore
{
    /// <summary>
    /// Implementación del núcleo de datos que utiliza CodeWalker.Core para gestionar archivos RPF.
    /// </summary>
    public class GameDataCore : IGameDataCore
    {
        public string GamePath { get; private set; }
        public bool IsInitialized { get; private set; }

        private RpfManager _rpfManager;

        public GameDataCore()
        {
            _rpfManager = new RpfManager();
            IsInitialized = false;
        }

        /// <summary>
        /// Inicializa el RpfManager de CodeWalker con la ruta de GTA V.
        /// </summary>
        public void Initialize(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"La ruta especificada no existe: {path}");

            if (!File.Exists(Path.Combine(path, "GTA5.exe")))
                throw new Exception("No se encontró GTA5.exe en la carpeta especificada. Asegúrate de seleccionar la raíz de GTA V.");

            GamePath = path;
            
            // Inicializar CodeWalker RpfManager
            // Esto escaneará todos los RPFs y construirá el índice de archivos
            _rpfManager.Init(path, false, 
                status => Console.WriteLine($"Cargando: {status}"), 
                error => Console.WriteLine($"Error: {error}"));

            IsInitialized = true;
        }

        /// <summary>
        /// Obtiene los datos binarios de un archivo buscando en todos los RPFs indexados.
        /// </summary>
        public byte[] GetFileData(string fileName)
        {
            if (!IsInitialized) throw new InvalidOperationException("El núcleo no ha sido inicializado.");

            // Buscar la entrada en el diccionario de CodeWalker
            if (_rpfManager.EntryDict.TryGetValue(fileName.ToLower(), out var entry))
            {
                return _rpfManager.GetFileData(entry);
            }

            return null;
        }

        /// <summary>
        /// Obtiene una lista de nombres de archivos que coinciden con la extensión.
        /// </summary>
        public IEnumerable<string> GetFilesByType(string extension)
        {
            if (!IsInitialized) return Enumerable.Empty<string>();

            string ext = extension.StartsWith(".") ? extension.ToLower() : "." + extension.ToLower();
            
            return _rpfManager.EntryDict.Keys
                .Where(k => k.EndsWith(ext))
                .ToList();
        }

        /// <summary>
        /// Carga un archivo YDR (Drawable) usando la lógica de CodeWalker.
        /// </summary>
        public YdrFile LoadYdr(string fileName)
        {
            if (_rpfManager.EntryDict.TryGetValue(fileName.ToLower(), out var entry) && entry is RpfFileEntry fileEntry)
            {
                var ydr = new YdrFile();
                byte[] data = _rpfManager.GetFileData(fileEntry);
                ydr.Load(data, fileEntry);
                return ydr;
            }
            return null;
        }
    }
}
