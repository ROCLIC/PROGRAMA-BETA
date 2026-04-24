using System;
using System.Collections.Generic;
using System.Numerics;

namespace FiveMTool.MeshEditor
{
    public enum EditMode
    {
        Object,
        Vertex,
        Edge,
        Face
    }

    /// <summary>
    /// Interfaz para el editor de mallas inspirado en Blender.
    /// </summary>
    public interface IMeshEditor
    {
        EditMode CurrentMode { get; set; }
        
        // Selección
        void SelectElement(object element, bool additive = false);
        void ClearSelection();
        
        // Operaciones de Transformación
        void TranslateSelection(Vector3 delta);
        void RotateSelection(Vector3 axis, float angle);
        void ScaleSelection(Vector3 factor);
        
        // Operaciones de Modelado (Fase 3/4)
        void ExtrudeSelection();
        void SubdivideSelection();
        void DeleteSelection();
        void FlipNormals();
    }

    /// <summary>
    /// Representación de una malla editable.
    /// </summary>
    public class EditableMesh
    {
        public List<Vector3> Vertices { get; set; } = new List<Vector3>();
        public List<int> Indices { get; set; } = new List<int>();
        public List<Vector2> UVs { get; set; } = new List<Vector2>();
        public List<Vector3> Normals { get; set; } = new List<Vector3>();
        
        // Estructura para selección rápida
        public HashSet<int> SelectedVertices { get; set; } = new HashSet<int>();
    }
}
