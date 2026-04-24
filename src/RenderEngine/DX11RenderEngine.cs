using System;
using System.Numerics;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace FiveMTool.RenderEngine
{
    /// <summary>
    /// Implementación concreta del motor de renderizado usando DirectX 11 a través de SharpDX.
    /// </summary>
    public class DX11RenderEngine : IRenderEngine, IDisposable
    {
        private Device _device;
        private DeviceContext _context;
        private SwapChain _swapChain;
        private RenderTargetView _renderTargetView;
        private DepthStencilView _depthStencilView;
        private Texture2D _depthBuffer;
        
        private Camera _camera;
        private Viewport _viewport;
        private ShaderManager _shaderManager;
        
        private bool _isInitialized = false;

        public DX11RenderEngine(Camera camera)
        {
            _camera = camera;
        }

        /// <summary>
        /// Inicializa DirectX 11 vinculado al handle de la ventana proporcionado.
        /// </summary>
        public void Initialize(IntPtr windowHandle, int width, int height)
        {
            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(width, height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = windowHandle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, desc, out _device, out _swapChain);
            _context = _device.ImmediateContext;

            // Inicializar gestor de shaders
            string shaderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Shaders");
            _shaderManager = new ShaderManager(_device, shaderPath);

            Resize(width, height);
            _isInitialized = true;
        }

        /// <summary>
        /// Ajusta el tamaño de los buffers de renderizado.
        /// </summary>
        public void Resize(int width, int height)
        {
            if (_device == null) return;

            _renderTargetView?.Dispose();
            _depthStencilView?.Dispose();
            _depthBuffer?.Dispose();

            _swapChain.ResizeBuffers(1, width, height, Format.Unknown, SwapChainFlags.None);

            using (var backBuffer = _swapChain.GetBackBuffer<Texture2D>(0))
            {
                _renderTargetView = new RenderTargetView(_device, backBuffer);
            }

            var depthDesc = new Texture2DDescription()
            {
                Format = Format.D24_UNorm_S8_UInt,
                ArraySize = 1,
                MipLevels = 1,
                Width = Math.Max(1, width),
                Height = Math.Max(1, height),
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };

            _depthBuffer = new Texture2D(_device, depthDesc);
            _depthStencilView = new DepthStencilView(_device, _depthBuffer);

            _viewport = new Viewport(0, 0, width, height, 0.0f, 1.0f);
            _context.Rasterizer.SetViewport(_viewport);
            
            _camera.AspectRatio = (float)width / (float)height;
        }

        /// <summary>
        /// Actualiza la lógica del motor, incluyendo la cámara.
        /// </summary>
        public void Update(float deltaTime)
        {
            if (!_isInitialized) return;
            _camera.UpdateMatrices();
        }

        /// <summary>
        /// Realiza el renderizado de la escena.
        /// </summary>
        public void Render()
        {
            if (!_isInitialized) return;

            _context.OutputMerger.SetRenderTargets(_depthStencilView, _renderTargetView);
            _context.ClearRenderTargetView(_renderTargetView, new Color4(0.176f, 0.176f, 0.188f, 1.0f)); // RGB(45,45,48)
            _context.ClearDepthStencilView(_depthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);

            // Aquí se añadiría el renderizado de la cuadrícula y mallas
            RenderGrid();

            _swapChain.Present(1, PresentFlags.None);
        }

        private void RenderGrid()
        {
            // Implementación básica de una cuadrícula de referencia
            // Por ahora es un placeholder para la estructura de renderizado
        }

        public void SetCameraPosition(float x, float y, float z)
        {
            _camera.Position = new System.Numerics.Vector3(x, y, z);
        }

        public void SetCameraRotation(float pitch, float yaw)
        {
            _camera.Rotation = new System.Numerics.Vector3(pitch, yaw, _camera.Rotation.Z);
        }

        public void LoadModel(byte[] modelData, string format)
        {
            // Implementación futura para cargar YDR/YBN
        }

        public void ClearScene()
        {
            // Limpiar recursos de la escena
        }

        public void Dispose()
        {
            _shaderManager?.Dispose();
            _renderTargetView?.Dispose();
            _depthStencilView?.Dispose();
            _depthBuffer?.Dispose();
            _swapChain?.Dispose();
            _context?.Dispose();
            _device?.Dispose();
        }
    }
}
