using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace FiveMTool.SceneSystem
{
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
        public void CreatePortal(Vector3 position, int from, int to)
        {
            var portal = new MLOPortal
            {
                FromRoom = from,
                ToRoom = to,
                Vertices = new List<Vector3>
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
            for (int i = 0; i < _activePortal.Vertices.Count; i++)
            {
                _activePortal.Vertices[i] = snapping.SnapPosition(_activePortal.Vertices[i]);
            }
        }

        public List<MLOPortal> GetPortals() => _portals;
    }
}
