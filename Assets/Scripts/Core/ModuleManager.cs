using System.Collections.Generic;
using System.Linq;
using Lifecycle;
using Modules;
using UnityEngine;

namespace Core
{
    public class ModuleManager : ITick
    {
        private readonly GameController _gameController;
        private readonly Canvas _canvas;

        private readonly List<BaseModule> _allModules;
        private BaseModule _currentModule;

        public ModuleManager(GameController gameController, Canvas canvas, Transform moduleContainer)
        {
            _gameController = gameController;
            _canvas = canvas;

            _allModules = new List<BaseModule>();

            for (var i = 0; i < moduleContainer.childCount; i++)
            {
                _allModules.Add(moduleContainer.GetChild(i).GetComponent<BaseModule>());
            }

            if (_allModules.Count == 0)
            {
                Debug.LogError("No modules found");
            }
        }

        public void Tick(float deltaTime)
        {
            if (_currentModule != null)
            {
                _currentModule.Tick(deltaTime);
            }
        }

        public void OpenModule<T>() where T : BaseModule
        {
            if (_currentModule != null)
            {
                _currentModule.Dispose();
                _currentModule = null;
            }

            foreach (var module in _allModules)
            {
                module.gameObject.SetActive(false);
            }

            var desiredModule = _allModules.OfType<T>().FirstOrDefault();
            if (desiredModule == null)
            {
                Debug.LogError($"Module of type '{typeof(T).Name}' not found");
                return;
            }

            desiredModule.gameObject.SetActive(true);
            desiredModule.Activate(_gameController, _canvas);

            _currentModule = desiredModule;
            desiredModule.OnModuleLoaded();
        }
    }
}