using System;
using System.Numerics;

namespace FiveMTool.SceneSystem
{
    /// <summary>
    /// Sistema de rejilla magnética (Snapping) para alineación precisa de objetos y portales MLO.
    /// </summary>
    public class SnappingSystem
    {
        public float GridSize { get; set; } = 0.1f; // Por defecto 10cm para MLOs
        public bool IsEnabled { get; set; } = true;
        public float RotationSnap { get; set; } = 5.0f; // Grados

        /// <summary>
        /// Ajusta una posición a la rejilla más cercana.
        /// </summary>
        public Vector3 SnapPosition(Vector3 position)
        {
            if (!IsEnabled) return position;

            return new Vector3(
                MathF.Round(position.X / GridSize) * GridSize,
                MathF.Round(position.Y / GridSize) * GridSize,
                MathF.Round(position.Z / GridSize) * GridSize
            );
        }

        /// <summary>
        /// Ajusta una rotación a los incrementos definidos.
        /// </summary>
        public Vector3 SnapRotation(Vector3 rotation)
        {
            if (!IsEnabled) return rotation;

            return new Vector3(
                MathF.Round(rotation.X / RotationSnap) * RotationSnap,
                MathF.Round(rotation.Y / RotationSnap) * RotationSnap,
                MathF.Round(rotation.Z / RotationSnap) * RotationSnap
            );
        }
    }
}
