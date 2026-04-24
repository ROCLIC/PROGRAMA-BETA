using System;
using System.Collections.Generic;
using System.Numerics;

namespace FiveMTool.MeshEditor
{
    /// <summary>
    /// Implementación del núcleo del editor de mallas para operaciones tipo Blender.
    /// </summary>
    public class MeshEditorCore : IMeshEditor
    {
        private List<int> _selectedVertices = new List<int>();

        public void SelectVertex(int index)
        {
            if (!_selectedVertices.Contains(index))
                _selectedVertices.Add(index);
        }

        public void TranslateSelection(Vector3 delta)
        {
            // Lógica para mover los vértices seleccionados en la malla activa
        }

        public void ExtrudeSelection()
        {
            // Lógica para extruir caras o aristas seleccionadas
        }

        public void ClearSelection()
        {
            _selectedVertices.Clear();
        }
    }
}
