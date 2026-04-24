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
        private ISceneSystem _sceneSystem;

        public MainForm(ISceneSystem sceneSystem)
        {
            _sceneSystem = sceneSystem;
            InitializeComponents();
            SetupLayout();
            
            _sceneSystem.OnSceneChanged += RefreshHierarchy;
        }

        private void InitializeComponents()
        {
            this.Text = "FiveM All-in-One Tool | Arquitecto Senior";
            this.Size = new Size(1280, 720);
            this.BackColor = Color.FromArgb(45, 45, 48); // Estilo oscuro tipo Blender

            _hierarchyTree = new TreeView { Dock = DockStyle.Fill, BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White };
            _propertyInspector = new PropertyGrid { Dock = DockStyle.Fill, BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White };
            _viewportPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Black };
            
            _hierarchyTree.AfterSelect += (s, e) => {
                if (e.Node.Tag is SceneObject obj)
                    _propertyInspector.SelectedObject = obj;
            };
        }

        private void SetupLayout()
        {
            // Panel Izquierdo (Jerarquía)
            var leftPanel = new Panel { Dock = DockStyle.Left, Width = 250 };
            leftPanel.Controls.Add(_hierarchyTree);
            var hierarchyLabel = new Label { Text = "Jerarquía", Dock = DockStyle.Top, ForeColor = Color.Gray, Height = 25 };
            leftPanel.Controls.Add(hierarchyLabel);

            // Panel Derecho (Inspector)
            var rightPanel = new Panel { Dock = DockStyle.Right, Width = 300 };
            rightPanel.Controls.Add(_propertyInspector);
            var inspectorLabel = new Label { Text = "Propiedades", Dock = DockStyle.Top, ForeColor = Color.Gray, Height = 25 };
            rightPanel.Controls.Add(inspectorLabel);

            // Centro (Visor)
            this.Controls.Add(_viewportPanel);
            this.Controls.Add(leftPanel);
            this.Controls.Add(rightPanel);
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
    }
}
