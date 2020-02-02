using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.Menu
{
    public class MenuModule : BaseModule, IPointerDownHandler
    {
        [SerializeField] private Image _backgroundImage = null;
        [SerializeField] private List<Sprite> _backgroundSprites = null;

        private GameController _gameController;

        public override void Activate(GameController gameController, Canvas canvas)
        {
            _gameController = gameController;

            var sprite = _backgroundSprites[Random.Range(0, _backgroundSprites.Count)];
            _backgroundImage.sprite = sprite;
        }

        public override void OnModuleLoaded()
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _gameController.GameModel.CreateNewWalkthroughModel();
            _gameController.OpenMapModule();
        }

        public override void Dispose()
        {
        }
    }
}