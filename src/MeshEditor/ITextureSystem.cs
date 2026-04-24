using System;
using System.Collections.Generic;
using System.Numerics;

namespace FiveMTool.MeshEditor
{
    /// <summary>
    /// Representa un material compatible con los shaders de GTA V.
    /// </summary>
    public class GTAMaterial
    {
        public string Name { get; set; }
        public string ShaderName { get; set; } // ej: "default", "decal", "glass"
        public Dictionary<string, string> Textures { get; set; } = new Dictionary<string, string>(); // SamplerName -> TexturePath
        public Dictionary<string, Vector4> Parameters { get; set; } = new Dictionary<string, Vector4>();
    }

    /// <summary>
    /// Interfaz para el sistema de texturas y mapeado UV.
    /// </summary>
    public interface ITextureSystem
    {
        void ApplyMaterial(EditableMesh mesh, GTAMaterial material);
        
        // Herramientas UV tipo Blender
        void SetUVScale(EditableMesh mesh, Vector2 scale);
        void SetUVOffset(EditableMesh mesh, Vector2 offset);
        void RotateUVs(EditableMesh mesh, float angle);
        
        // Gestión de Biblioteca
        void ImportTexture(string path);
        IEnumerable<string> GetAvailableTextures();
    }
}
