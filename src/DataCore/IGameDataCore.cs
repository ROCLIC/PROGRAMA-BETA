using System;
using System.Collections.Generic;

namespace FiveMTool.DataCore
{
    /// <summary>
    /// Interfaz para el núcleo de datos del juego.
    /// Responsable de la gestión de archivos RPF y búsqueda de assets.
    /// </summary>
    public interface IGameDataCore
    {
        string GamePath { get; }
        bool IsInitialized { get; }

        /// <summary>
        /// Inicializa el núcleo apuntando a la carpeta de GTA V.
        /// </summary>
        void Initialize(string path);

        /// <summary>
        /// Busca un asset por su nombre (ej: "prop_tree_01.ydr").
        /// </summary>
        byte[] GetFileData(string fileName);

        /// <summary>
        /// Obtiene una lista de todos los archivos de un tipo específico.
        /// </summary>
        IEnumerable<string> GetFilesByType(string extension);
    }
}
