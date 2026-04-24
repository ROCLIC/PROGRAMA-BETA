using System;
using System.Collections.Generic;
using System.Numerics;

namespace FiveMTool.SceneSystem
{
    /// <summary>
    /// Representa un portal MLO que conecta dos habitaciones (Rooms).
    /// </summary>
    public class MLOPortal
    {
        public string Name { get; set; } = "Nuevo Portal";
        public Vector3[] Vertices { get; set; } = new Vector3[4]; // Portales rectangulares por defecto
        public string FromRoom { get; set; }
        public string ToRoom { get; set; }
        public bool IsSelected { get; set; } = false;
    }

    /// <summary>
    /// Herramienta visual para crear y editar portales MLO directamente en el visor 3D.
    /// </summary>
    public class PortalEditor
    {
        private List<MLOPortal> _portals = new List<MLOPortal>();
        private MLOPortal _activePortal;

        /// <summary>
        /// Crea un nuevo portal en la posición actual de la cámara.
        /// </summary>
        public void CreatePortal(Vector3 position, string from, string to)
        {
            var portal = new MLOPortal
            {
                FromRoom = from,
                ToRoom = to,
                Vertices = new Vector3[]
                {
                    position + new Vector3(-1, 0, -1),
                    position + new Vector3(1, 0, -1),
                    position + new Vector3(1, 0, 1),
                    position + new Vector3(-1, 0, 1)
                }
            };
            _portals.Add(portal);
            _activePortal = portal;
        }

        /// <summary>
        /// Ajusta los vértices del portal activo usando el sistema de Snapping.
        /// </summary>
        public void SnapActivePortal(SnappingSystem snapping)
        {
            if (_activePortal == null) return;
            for (int i = 0; i < _activePortal.Vertices.Length; i++)
            {
                _activePortal.Vertices[i] = snapping.SnapPosition(_activePortal.Vertices[i]);
            }
        }

        public List<MLOPortal> GetPortals() => _portals;
    }
}
