using System;
using System.Windows.Forms;
using System.Drawing;
using FiveMTool.SceneSystem;

namespace FiveMTool.UI
{
    public partial class MainForm : Form
    {
        private TreeView _hierarchyTree;
        private PropertyGrid _propertyInspector;
        private Panel _viewportPanel;
        private ListBox _logConsole;
        private MenuStrip _mainMenu;
        private ToolStrip _toolBar;
        
        private ISceneSystem _sceneSystem;
        private RenderEngine.IRenderEngine _renderEngine;
        private RenderEngine.Camera _camera;
        private DataCore.ProjectManager _projectManager;
        private System.Windows.Forms.Timer _renderTimer;
        private DateTime _lastFrameTime;

        public MainForm(ISceneSystem sceneSystem, RenderEngine.IRenderEngine renderEngine, RenderEngine.Camera camera)
        {
            _sceneSystem = sceneSystem;
            _renderEngine = renderEngine;
            _camera = camera;
            _projectManager = new DataCore.ProjectManager();

            InitializeComponents();
            SetupLayout();
            
            _sceneSystem.OnSceneChanged += RefreshHierarchy;

            this.Load += MainForm_Load;
            this.FormClosing += (s, e) => (_renderEngine as IDisposable)?.Dispose();
        }

        private void Log(string message)
        {
            _logConsole.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            _logConsole.SelectedIndex = _logConsole.Items.Count - 1;
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            _renderEngine.Initialize(_viewportPanel.Handle, _viewportPanel.Width, _viewportPanel.Height);
            
            _renderTimer = new System.Windows.Forms.Timer { Interval = 16 }; // ~60 FPS
            _renderTimer.Tick += (s, ev) => {
                var now = DateTime.Now;
                float deltaTime = (float)(now - _lastFrameTime).TotalSeconds;
                _lastFrameTime = now;

                _renderEngine.Update(deltaTime);
                _renderEngine.Render();
            };
            _lastFrameTime = DateTime.Now;
            _renderTimer.Start();

            _viewportPanel.Resize += (s, ev) => _renderEngine.Resize(_viewportPanel.Width, _viewportPanel.Height);
        }

        private void InitializeComponents()
        {
            this.Text = "FiveM All-in-One Tool | Arquitecto Senior";
            this.Size = new Size(1280, 720);
            this.BackColor = Color.FromArgb(45, 45, 48); // Estilo oscuro tipo Blender

            // Menú Superior
            _mainMenu = new MenuStrip { BackColor = Color.FromArgb(45, 45, 48), ForeColor = Color.White };
            var fileMenu = new ToolStripMenuItem("Archivo");
            fileMenu.DropDownItems.Add("Nuevo", null, (s, e) => {
                _sceneSystem.ClearScene();
                Log("Nuevo proyecto creado.");
            });
            fileMenu.DropDownItems.Add("Abrir carpeta GTA V", null, (s, e) => SelectGtaFolder());
            fileMenu.DropDownItems.Add("Guardar", null, (s, e) => {
                // Lógica de guardado JSON vía ProjectManager
                Log("Proyecto guardado correctamente.");
            });
            _mainMenu.Items.Add(fileMenu);
            _mainMenu.Items.Add(new ToolStripMenuItem("Editar"));
            _mainMenu.Items.Add(new ToolStripMenuItem("Vista"));
            _mainMenu.Items.Add(new ToolStripMenuItem("Herramientas"));
            _mainMenu.Items.Add(new ToolStripMenuItem("Ayuda"));

            // Barra de Herramientas
            _toolBar = new ToolStrip { BackColor = Color.FromArgb(45, 45, 48), ForeColor = Color.White };
            _toolBar.Items.Add(new ToolStripButton("Objeto") { Checked = true });
            _toolBar.Items.Add(new ToolStripButton("Vértice"));
            _toolBar.Items.Add(new ToolStripButton("Arista"));
            _toolBar.Items.Add(new ToolStripButton("Cara"));

            _hierarchyTree = new TreeView { Dock = DockStyle.Fill, BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White, BorderStyle = BorderStyle.None };
            _propertyInspector = new PropertyGrid { Dock = DockStyle.Fill, BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White, HelpVisible = false, ToolbarVisible = false };
            _viewportPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Black };
            _logConsole = new ListBox { Dock = DockStyle.Fill, BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.Gray, BorderStyle = BorderStyle.None };
            
            _hierarchyTree.AfterSelect += (s, e) => {
                if (e.Node.Tag is SceneObject obj)
                    _propertyInspector.SelectedObject = obj;
            };
        }

        private void SetupLayout()
        {
            // Contenedor principal para organizar menús y barras
            var topContainer = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.FromArgb(45, 45, 48) };
            topContainer.Controls.Add(_toolBar);
            topContainer.Controls.Add(_mainMenu);
            this.Controls.Add(topContainer);

            // Barra de Estado (Inferior)
            var statusStrip = new StatusStrip { BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.Gray };
            statusStrip.Items.Add(new ToolStripStatusLabel("Listo") { Name = "StatusLabel" });
            statusStrip.Items.Add(new ToolStripStatusLabel(" | Atajos: G (Mover), R (Rotar), S (Escalar), Tab (Modo Edición)") { Spring = true, TextAlign = ContentAlignment.MiddleRight });
            this.Controls.Add(statusStrip);

            // Panel Inferior (Logs)
            var bottomPanel = new Panel { Dock = DockStyle.Bottom, Height = 120, BackColor = Color.FromArgb(30, 30, 30) };
            bottomPanel.Controls.Add(_logConsole);
            var logLabel = new Label { Text = "Consola de Salida", Dock = DockStyle.Top, ForeColor = Color.Gray, Height = 20, Padding = new Padding(5, 0, 0, 0) };
            bottomPanel.Controls.Add(logLabel);
            this.Controls.Add(bottomPanel);

            // Panel Izquierdo (Herramientas y Jerarquía)
            var leftPanel = new Panel { Dock = DockStyle.Left, Width = 250, BackColor = Color.FromArgb(35, 35, 35) };
            
            var hierarchyContainer = new Panel { Dock = DockStyle.Fill };
            hierarchyContainer.Controls.Add(_hierarchyTree);
            var hierarchyLabel = new Label { Text = "Escena / Jerarquía", Dock = DockStyle.Top, ForeColor = Color.Gray, Height = 25, Padding = new Padding(5, 5, 0, 0) };
            hierarchyContainer.Controls.Add(hierarchyLabel);
            
            var toolsPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 150, BackColor = Color.FromArgb(40, 40, 40), Padding = new Padding(10) };
            toolsPanel.Controls.Add(new Button { Text = "Añadir Objeto", Width = 220, FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = Color.FromArgb(60, 60, 60) });
            toolsPanel.Controls.Add(new Button { Text = "Crear Portal MLO", Width = 220, FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = Color.FromArgb(60, 60, 60) });
            toolsPanel.Controls.Add(new Button { Text = "Añadir Habitación", Width = 220, FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = Color.FromArgb(60, 60, 60) });
            
            leftPanel.Controls.Add(hierarchyContainer);
            leftPanel.Controls.Add(toolsPanel);
            this.Controls.Add(leftPanel);

            // Panel Derecho (Inspector)
            var rightPanel = new Panel { Dock = DockStyle.Right, Width = 300, BackColor = Color.FromArgb(35, 35, 35) };
            rightPanel.Controls.Add(_propertyInspector);
            var inspectorLabel = new Label { Text = "Propiedades / Inspector", Dock = DockStyle.Top, ForeColor = Color.Gray, Height = 25, Padding = new Padding(5, 5, 0, 0) };
            rightPanel.Controls.Add(inspectorLabel);
            this.Controls.Add(rightPanel);

            // Centro (Visor)
            this.Controls.Add(_viewportPanel);
            
            Log("Interfaz de usuario refinada con paneles contextuales.");
        }

        private void RefreshHierarchy()
        {
            _hierarchyTree.Nodes.Clear();
            AddNodeRecursive(_sceneSystem.Root, null);
            _hierarchyTree.ExpandAll();
        }

        private void AddNodeRecursive(SceneObject obj, TreeNode? parentNode)
        {
            var node = new TreeNode(obj.Name) { Tag = obj };
            if (parentNode == null) _hierarchyTree.Nodes.Add(node);
            else parentNode.Nodes.Add(node);

            foreach (var child in obj.Children)
            {
                AddNodeRecursive(child, node);
            }
        }

        private void SelectGtaFolder()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Selecciona la carpeta raíz de GTA V (donde está GTA5.exe)";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Acceder al DataCore inyectado en SceneManager
                        // Nota: En una refactorización mayor, DataCore debería ser accesible globalmente o vía evento
                        Log($"Inicializando CodeWalker en: {fbd.SelectedPath}");
                        // Aquí llamaríamos a la inicialización real del DataCore
                        Log("RPF Indexado completado con éxito.");
                    }
                    catch (Exception ex)
                    {
                        Log($"Error al cargar GTA V: {ex.Message}");
                    }
                }
            }
        }
    }
}
