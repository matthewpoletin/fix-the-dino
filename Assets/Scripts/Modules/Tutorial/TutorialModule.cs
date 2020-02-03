using Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.Tutorial
{
    public class TutorialModule : BaseModule, IPointerDownHandler
    {
        private GameController _gameController;
        
        public override void Activate(GameController gameController, Canvas canvas)
        {
            _gameController = gameController;
        }

        public override void OnModuleLoaded()
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _gameController.OpenMenuModule();
        }
    }
}