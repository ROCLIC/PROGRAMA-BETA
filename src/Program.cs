using System;
using System.Windows.Forms;
using FiveMTool.SceneSystem;
using FiveMTool.UI;
using FiveMTool.DataCore;

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
            var sceneManager = new SceneManager();
            var dataCore = new GameDataCore();

            // Crear el formulario principal
            var mainForm = new MainForm(sceneManager);

            // Ejemplo de carga inicial
            sceneManager.AddObject(new SceneObject { Name = "Modelo de Prueba (YDR)", Position = new System.Numerics.Vector3(0, 0, 0) });

            Application.Run(mainForm);
        }
    }
}
