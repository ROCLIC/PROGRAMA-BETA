using System;
using System.Collections.Generic;
using System.Numerics;

namespace FiveMTool.SceneSystem
{
    /// <summary>
    /// Define un objeto dentro de la escena 3D.
    /// </summary>
    public class SceneObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 Rotation { get; set; } = Vector3.Zero;
        public Vector3 Scale { get; set; } = Vector3.One;
        public bool IsVisible { get; set; } = true;
        public List<SceneObject> Children { get; set; } = new List<SceneObject>();
        public object Metadata { get; set; } // Para vincular con datos de CodeWalker (YDR, YBN)
    }

    /// <summary>
    /// Interfaz para el sistema de gestión de escena.
    /// </summary>
    public interface ISceneSystem
    {
        SceneObject Root { get; }
        void AddObject(SceneObject obj, SceneObject parent = null);
        void RemoveObject(Guid id);
        SceneObject FindObject(Guid id);
        void ClearScene();
        
        // Eventos para la UI
        event Action OnSceneChanged;
    }
}
