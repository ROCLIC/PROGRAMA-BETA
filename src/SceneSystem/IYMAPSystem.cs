using System;
using System.Collections.Generic;
using System.Numerics;

namespace FiveMTool.SceneSystem
{
    /// <summary>
    /// Representa una entidad colocada en un archivo YMAP.
    /// </summary>
    public class YMAPEntity
    {
        public string ArchetypeName { get; set; }
        public uint Hash { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public float LodDistance { get; set; }
        public uint ParentIndex { get; set; }
    }

    /// <summary>
    /// Interfaz para el sistema de creación de YMAP y escaneo de mundo.
    /// </summary>
    public interface IYMAPSystem
    {
        // Gestión de YMAP
        void CreateNewYMAP(string name);
        void AddEntity(YMAPEntity entity);
        void RemoveEntity(YMAPEntity entity);
        void ExportToYMAP(string outputPath);

        // Escáner de Mundo Avanzado
        /// <summary>
        /// Escanea un área definida por un radio y detecta objetos existentes.
        /// </summary>
        IEnumerable<YMAPEntity> ScanWorldArea(Vector3 center, float radius);
        
        /// <summary>
        /// Identifica el asset bajo el cursor en tiempo real.
        /// </summary>
        YMAPEntity GetEntityUnderCursor(Vector2 mousePos);

        // Gestión de LODs
        void SetLODLevel(YMAPEntity entity, int level);
    }
}
