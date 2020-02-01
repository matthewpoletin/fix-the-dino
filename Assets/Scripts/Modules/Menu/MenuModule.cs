using System.Linq;
using Core;
using UnityEngine.EventSystems;

namespace Modules.Menu
{
    public class MenuModule : BaseModule, IPointerDownHandler
    {
        private GameController _gameController;

        public override void Activate(GameController gameController)
        {
            _gameController = gameController;
        }

        public override void OnModuleLoaded()
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // TODO: Если всё пройдено, показать мультик
            // if (_gameController.GameModel.WalkthroughModel.Sites.Any(siteModel => !siteModel.IsVisited))
            // {
            // }

            _gameController.GameModel.CreateNewWalkthroughModel();
            _gameController.OpenMapModule();
        }

        public override void Dispose()
        {
        }
    }
}