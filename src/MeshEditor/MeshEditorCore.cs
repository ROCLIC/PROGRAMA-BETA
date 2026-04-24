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

        public EditMode CurrentMode { get; set; } = EditMode.Object;

        public void SelectVertex(int index)
        {
            if (!_selectedVertices.Contains(index))
                _selectedVertices.Add(index);
        }

        public void SelectElement(object element, bool additive = false)
        {
            // Implementación de IMeshEditor
        }

        public void TranslateSelection(Vector3 delta)
        {
            // Lógica para mover los vértices seleccionados en la malla activa
        }

        public void RotateSelection(Vector3 axis, float angle)
        {
            // Implementación de IMeshEditor
        }

        public void ScaleSelection(Vector3 factor)
        {
            // Implementación de IMeshEditor
        }

        public void ExtrudeSelection()
        {
            // Lógica para extruir caras o aristas seleccionadas
        }

        public void SubdivideSelection()
        {
            // Implementación de IMeshEditor
        }

        public void DeleteSelection()
        {
            // Implementación de IMeshEditor
        }

        public void FlipNormals()
        {
            // Implementación de IMeshEditor
        }

        public void ClearSelection()
        {
            _selectedVertices.Clear();
        }
    }
}
