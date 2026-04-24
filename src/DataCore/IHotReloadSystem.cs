using System;
using System.Threading.Tasks;

namespace FiveMTool.DataCore
{
    /// <summary>
    /// Interfaz para el sistema de actualización en caliente (Hot-Reload).
    /// Permite enviar cambios al servidor FiveM sin reiniciar.
    /// </summary>
    public interface IHotReloadSystem
    {
        bool IsConnected { get; }
        string ServerIp { get; set; }
        int ServerPort { get; set; }

        /// <summary>
        /// Conecta la herramienta con el recurso del servidor FiveM.
        /// </summary>
        Task<bool> ConnectToServer(string ip, int port, string secretToken);

        /// <summary>
        /// Envía una actualización de entidad (YMAP/MLO) al servidor.
        /// </summary>
        Task<bool> PushEntityUpdate(object entityData);

        /// <summary>
        /// Recarga un recurso completo en el servidor.
        /// </summary>
        Task<bool> ReloadResource(string resourceName);

        // Eventos de estado
        event Action<string> OnSyncStatusChanged;
    }
}
