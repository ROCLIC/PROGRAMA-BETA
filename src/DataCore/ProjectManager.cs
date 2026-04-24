using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using FiveMTool.SceneSystem;

namespace FiveMTool.DataCore
{
    /// <summary>
    /// Gestiona la creación, carga y guardado de proyectos de FiveMTool.
    /// </summary>
    public class ProjectManager
    {
        public class ProjectData
        {
            public string Name { get; set; }
            public string GtaPath { get; set; }
            public List<SceneObject> SceneObjects { get; set; }
            public DateTime LastModified { get; set; }
        }

        private string _currentProjectPath;

        /// <summary>
        /// Crea un nuevo proyecto en la ruta especificada.
        /// </summary>
        public void CreateProject(string name, string path)
        {
            var data = new ProjectData
            {
                Name = name,
                SceneObjects = new List<SceneObject>(),
                LastModified = DateTime.Now
            };
            
            _currentProjectPath = Path.Combine(path, $"{name}.f5p");
            SaveProject(data);
        }

        /// <summary>
        /// Guarda el estado actual del proyecto en formato JSON.
        /// </summary>
        public void SaveProject(ProjectData data)
        {
            if (string.IsNullOrEmpty(_currentProjectPath)) return;

            data.LastModified = DateTime.Now;
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_currentProjectPath, json);
        }

        /// <summary>
        /// Carga un proyecto desde un archivo .f5p.
        /// </summary>
        public ProjectData LoadProject(string path)
        {
            if (!File.Exists(path)) return null;

            _currentProjectPath = path;
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ProjectData>(json);
        }
    }
}
