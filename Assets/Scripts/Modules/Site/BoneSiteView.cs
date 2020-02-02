using System;
using Core;
using Lifecycle;
using Params;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.Site
{
    public class BoneSiteView : BaseView, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler,
        IPointerExitHandler
    {
        [SerializeField] private Image _image = null;
        [SerializeField] private CanvasGroup _canvasGroup = null;

        private GameController _gameController;
        private Canvas _canvas;
        private PartParams _params;

        private Action<BoneSiteView> _onPointerEnter;

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

        private bool _isFound;

        public bool IsFound
        {
            get => _isFound;
            set
            {
                _isFound = value;
                var tempColor = _image.color;
                tempColor.a = _isFound ? 1f : 0f;
                _image.color = tempColor;
            }
        }

        public void Connect(GameController gameController, Canvas canvas, PartParams partParams,
            Action<BoneSiteView> onPointerEnter)
        {
            _gameController = gameController;
            _canvas = canvas;
            _params = partParams;
            _onPointerEnter = onPointerEnter;

            _image.sprite = _params.BoneImage;
            IsFound = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsFound)
            {
                return;
            }

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
            if (!IsFound)
            {
                return;
            }

            if (IsCollected)
            {
                return;
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
                Input.mousePosition, _canvas.worldCamera, out var pos);
            transform.position = _canvas.transform.TransformPoint(pos);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            if (IsCollected)
            {
                return;
            }

            if (!IsFound)
            {
                return;
            }

            transform.position = _startPosition;
            transform.localScale = _startScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _onPointerEnter.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _onPointerEnter.Invoke(this);
        }
    }
}