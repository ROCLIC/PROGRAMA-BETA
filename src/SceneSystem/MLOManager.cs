using System;
using System.Collections.Generic;

namespace FiveMTool.SceneSystem
{
    /// <summary>
    /// Implementación del sistema MLO para la gestión de interiores y portales.
    /// </summary>
    public class MLOManager : IMLOSystem
    {
        private PortalEditor _portalEditor = new PortalEditor();
        private List<string> _rooms = new List<string>();
        private string _activeRoom = "limbo";

        public void LoadMLO(byte[] data)
        {
            // Lógica para cargar archivos YTYP y definir estructuras MLO
        }

        public void UpdatePortals()
        {
            // Lógica de visibilidad basada en portales MLO
        }

        public void SetRoom(string roomName)
        {
            _activeRoom = roomName;
            // Cambiar la habitación activa para el renderizado
        }

        public void AddRoom(string name)
        {
            if (!_rooms.Contains(name)) _rooms.Add(name);
        }

        public PortalEditor GetPortalEditor() => _portalEditor;
    }
}
