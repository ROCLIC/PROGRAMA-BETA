using System;

namespace FiveMTool.RenderEngine
{
    /// <summary>
    /// Interfaz para el motor de renderizado 3D.
    /// Responsable de la visualización de mallas, colisiones y navegación.
    /// </summary>
    public interface IRenderEngine
    {
        void Initialize(IntPtr windowHandle, int width, int height);
        void Resize(int width, int height);
        void Update(float deltaTime);
        void Render();
        
        // Controles de cámara
        void SetCameraPosition(float x, float y, float z);
        void SetCameraRotation(float pitch, float yaw);
        
        // Gestión de mallas
        void LoadModel(byte[] modelData, string format); // YDR, YBN, etc.
        void ClearScene();
    }
}
