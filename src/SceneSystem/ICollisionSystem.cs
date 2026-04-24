using System;
using System.Collections.Generic;
using System.Numerics;

namespace FiveMTool.SceneSystem
{
    public enum CollisionType
    {
        Box,
        Sphere,
        Capsule,
        Mesh
    }

    /// <summary>
    /// Representa una primitiva de colisión física (YBN).
    /// </summary>
    public class CollisionGeometry
    {
        public CollisionType Type { get; set; }
        public Vector3 Center { get; set; }
        public Vector3 Size { get; set; } // Para Box/Sphere
        public List<Vector3> Vertices { get; set; } // Para Mesh
        public string PhysicsMaterial { get; set; } // ej: "CONCRETE", "WOOD", "METAL"
    }

    /// <summary>
    /// Interfaz para el sistema de colisiones.
    /// </summary>
    public interface ICollisionSystem
    {
        void AddCollision(SceneObject obj, CollisionGeometry geometry);
        void RemoveCollision(SceneObject obj);
        
        // Visualización Debug
        void SetDebugVisibility(bool visible);
        
        // Edición
        void UpdateCollisionGeometry(SceneObject obj, CollisionGeometry newGeometry);
    }
}
