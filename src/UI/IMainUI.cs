using System;
using System.Collections.Generic;

namespace FiveMTool.UI
{
    /// <summary>
    /// Interfaz para la gestión de la interfaz de usuario principal.
    /// Inspirada en el flujo de trabajo de Blender.
    /// </summary>
    public interface IMainUI
    {
        // Componentes de la Interfaz
        void UpdateHierarchy(IEnumerable<object> sceneObjects);
        void UpdateInspector(object selectedObject);
        void ShowAssetBrowser(bool visible);
        
        // Notificaciones y Logs
        void LogMessage(string message, LogType type);
        void ShowProgressBar(string taskName, float progress);

        // Atajos de Teclado (Keybindings)
        void RegisterShortcut(string keyCombination, Action action);
        
        // Gestión de Ventanas
        void OpenModal(string title, object content);
    }

    public enum LogType
    {
        Info,
        Warning,
        Error,
        Success
    }
}
