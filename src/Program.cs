using System;
using System.Windows.Forms;
using FiveMTool.SceneSystem;
using FiveMTool.UI;
using FiveMTool.DataCore;
using FiveMTool.RenderEngine;

namespace FiveMTool
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicialización de módulos
            var dataCore = new GameDataCore();
            var sceneManager = new SceneManager(dataCore);
            var camera = new Camera();
            var renderEngine = new DX11RenderEngine(camera);

            // Crear el formulario principal
            var mainForm = new MainForm(sceneManager, renderEngine, camera);

            // Ejemplo de carga inicial
            sceneManager.AddObject(new SceneObject { Name = "Modelo de Prueba (YDR)", Position = new System.Numerics.Vector3(0, 0, 0) });

            Application.Run(mainForm);
        }
    }
}
