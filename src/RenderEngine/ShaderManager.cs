using System;
using System.Collections.Generic;
using System.IO;
using SharpDX.Direct3D11;

namespace FiveMTool.RenderEngine
{
    /// <summary>
    /// Gestiona la carga y el almacenamiento de shaders compilados de CodeWalker.
    /// </summary>
    public class ShaderManager : IDisposable
    {
        private Device _device;
        private Dictionary<string, VertexShader> _vertexShaders = new Dictionary<string, VertexShader>();
        private Dictionary<string, PixelShader> _pixelShaders = new Dictionary<string, PixelShader>();
        private string _shaderPath;

        public ShaderManager(Device device, string shaderPath)
        {
            _device = device;
            _shaderPath = shaderPath;
        }

        /// <summary>
        /// Carga un Vertex Shader desde un archivo .cso.
        /// </summary>
        public VertexShader GetVertexShader(string name)
        {
            if (_vertexShaders.TryGetValue(name, out var shader)) return shader;

            string path = Path.Combine(_shaderPath, $"{name}.cso");
            if (!File.Exists(path)) return null;

            byte[] bytecode = File.ReadAllBytes(path);
            var vs = new VertexShader(_device, bytecode);
            _vertexShaders[name] = vs;
            return vs;
        }

        /// <summary>
        /// Carga un Pixel Shader desde un archivo .cso.
        /// </summary>
        public PixelShader GetPixelShader(string name)
        {
            if (_pixelShaders.TryGetValue(name, out var shader)) return shader;

            string path = Path.Combine(_shaderPath, $"{name}.cso");
            if (!File.Exists(path)) return null;

            byte[] bytecode = File.ReadAllBytes(path);
            var ps = new PixelShader(_device, bytecode);
            _pixelShaders[name] = ps;
            return ps;
        }

        public void Dispose()
        {
            foreach (var vs in _vertexShaders.Values) vs.Dispose();
            foreach (var ps in _pixelShaders.Values) ps.Dispose();
            _vertexShaders.Clear();
            _pixelShaders.Clear();
        }
    }
}
