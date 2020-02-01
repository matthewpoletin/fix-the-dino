using Core;
using Params;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.Site
{
    public class BoneSiteView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _image = null;
        [SerializeField] private CanvasGroup _canvasGroup = null;

        private GameController _gameController;
        private PartParams _params;

        private Vector3 _startPosition;
        private Vector3 _startScale;

        private bool _isCollected;

        public bool IsCollected
        {
            get => _isCollected;
            set
            {
                _isCollected = value;
                if (_isCollected)
                {
                    _gameController.GameModel.WalkthroughModel.CollectNewPart(_params);
                }
            }
        }

        public void Connect(GameController gameController, PartParams partParams)
        {
            _gameController = gameController;
            _params = partParams;

            _image.sprite = _params.BoneImage;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsCollected)
            {
                return;
            }

            _canvasGroup.blocksRaycasts = false;

            _startPosition = transform.position;
            _startScale = transform.localScale;

            transform.localScale = _startScale * 1.2f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsCollected)
            {
                return;
            }

            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            if (IsCollected)
            {
                return;
            }

            transform.position = _startPosition;
            transform.localScale = _startScale;
        }
    }
}