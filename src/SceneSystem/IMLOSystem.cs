using System;
using System.Collections.Generic;
using System.Numerics;

namespace FiveMTool.SceneSystem
{
    /// <summary>
    /// Representa una habitación dentro de un MLO.
    /// </summary>
    public class MLORoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Vector3 BbMin { get; set; }
        public Vector3 BbMax { get; set; }
        public List<Guid> AttachedEntities { get; set; } = new List<Guid>();
        public List<int> ConnectedPortals { get; set; } = new List<int>();
    }

    /// <summary>
    /// Representa un portal que conecta dos habitaciones.
    /// </summary>
    public class MLOPortal
    {
        public int Id { get; set; }
        public int FromRoom { get; set; }
        public int ToRoom { get; set; }
        public List<Vector3> Vertices { get; set; } = new List<Vector3>(); // Geometría del portal
        public Vector3 Normal { get; set; }
    }

    /// <summary>
    /// Interfaz para el sistema de creación y edición de MLO.
    /// </summary>
    public interface IMLOSystem
    {
        // Gestión de Habitaciones
        MLORoom CreateRoom(string name);
        void DeleteRoom(int roomId);
        void AddEntityToRoom(int roomId, Guid entityId);
        
        // Gestión de Portales
        MLOPortal CreatePortal(int fromRoom, int toRoom, List<Vector3> vertices);
        void AutoCalculatePortals(int roomId); // Funcionalidad avanzada
        
        // Gestión de YTYP
        void LoadFromYTYP(string xmlPath);
        void ExportToYTYP(string outputPath);
        
        // Oclusión
        void SetRoomOcclusion(int roomId, bool isOccluded);
    }
}
