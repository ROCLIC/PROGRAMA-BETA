using System;
using System.Numerics; // Usamos System.Numerics para mayor modernidad y portabilidad

namespace FiveMTool.RenderEngine
{
    public class Camera
    {
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 Rotation { get; set; } = Vector3.Zero; // Pitch, Yaw, Roll
        
        public float FieldOfView { get; set; } = 1.0f;
        public float AspectRatio { get; set; } = 16f / 9f;
        public float NearPlane { get; set; } = 0.1f;
        public float FarPlane { get; set; } = 10000.0f;

        public Matrix4x4 ViewMatrix { get; private set; }
        public Matrix4x4 ProjectionMatrix { get; private set; }

        public void UpdateMatrices()
        {
            // Calcular matriz de vista basada en posición y rotación
            var rotationMatrix = Matrix4x4.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z);
            var forward = Vector3.Transform(new Vector3(0, 0, -1), rotationMatrix);
            var up = Vector3.Transform(new Vector3(0, 1, 0), rotationMatrix);
            
            ViewMatrix = Matrix4x4.CreateLookAt(Position, Position + forward, up);
            
            // Calcular matriz de proyección
            ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearPlane, FarPlane);
        }

        public void Move(Vector3 direction, float amount)
        {
            var rotationMatrix = Matrix4x4.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z);
            var transformedDir = Vector3.Transform(direction, rotationMatrix);
            Position += transformedDir * amount;
        }
    }
}
