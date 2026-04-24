using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FiveMTool.SceneSystem
{
    public class SceneManager : ISceneSystem
    {
        public SceneObject Root { get; private set; }
        private Dictionary<Guid, SceneObject> _objectRegistry;

        public event Action? OnSceneChanged;

        public SceneManager()
        {
            Root = new SceneObject { Name = "Escena Raíz", Id = Guid.NewGuid() };
            _objectRegistry = new Dictionary<Guid, SceneObject>();
            _objectRegistry.Add(Root.Id, Root);
        }

        public void AddObject(SceneObject obj, SceneObject? parent = null)
        {
            parent ??= Root;
            parent.Children.Add(obj);
            RegisterObjectRecursive(obj);
            OnSceneChanged?.Invoke();
        }

        private void RegisterObjectRecursive(SceneObject obj)
        {
            if (!_objectRegistry.ContainsKey(obj.Id))
            {
                _objectRegistry.Add(obj.Id, obj);
            }
            foreach (var child in obj.Children)
            {
                RegisterObjectRecursive(child);
            }
        }

        public void RemoveObject(Guid id)
        {
            if (id == Root.Id) return; // No se puede borrar la raíz

            var obj = FindObject(id);
            if (obj != null)
            {
                // Buscar al padre para removerlo de su lista de hijos
                RemoveFromParent(Root, id);
                UnregisterObjectRecursive(obj);
                OnSceneChanged?.Invoke();
            }
        }

        private bool RemoveFromParent(SceneObject current, Guid targetId)
        {
            var target = current.Children.FirstOrDefault(c => c.Id == targetId);
            if (target != null)
            {
                current.Children.Remove(target);
                return true;
            }

            foreach (var child in current.Children)
            {
                if (RemoveFromParent(child, targetId)) return true;
            }
            return false;
        }

        private void UnregisterObjectRecursive(SceneObject obj)
        {
            _objectRegistry.Remove(obj.Id);
            foreach (var child in obj.Children)
            {
                UnregisterObjectRecursive(child);
            }
        }

        public SceneObject? FindObject(Guid id)
        {
            _objectRegistry.TryGetValue(id, out var obj);
            return obj;
        }

        public void ClearScene()
        {
            Root.Children.Clear();
            _objectRegistry.Clear();
            _objectRegistry.Add(Root.Id, Root);
            OnSceneChanged?.Invoke();
        }
    }
}
