using Lifecycle;
using Model;
using Modules.Combine;
using Modules.Map;
using Modules.Menu;
using Modules.Site;
using UnityEngine;

namespace Core
{
    public class GameController : ITick
    {
        public readonly GameModel GameModel;

        private readonly ModuleManager _moduleManager;

        public GameController(GameModel gameModel, Canvas canvas, Transform moduleContainer)
        {
            GameModel = gameModel;
            _moduleManager = new ModuleManager(this, canvas, moduleContainer);
        }

        public void OpenMenuModule()
        {
            _moduleManager.OpenModule<MenuModule>();
        }

        public void OpenMapModule()
        {
            _moduleManager.OpenModule<MapModule>();
        }

        public void OpenSiteModule()
        {
            _moduleManager.OpenModule<SiteModule>();
        }

        public void OpenCombineModule()
        {
            _moduleManager.OpenModule<CombineModule>();
        }

        public void Tick(float deltaTime)
        {
            _moduleManager.Tick(deltaTime);
        }
    }
}