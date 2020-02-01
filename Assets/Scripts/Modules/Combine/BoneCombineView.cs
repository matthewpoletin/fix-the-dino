using System;
using Lifecycle;
using Params;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.Combine
{
    public class BoneCombineView : BaseView, IPointerDownHandler
    {
        [SerializeField] private Image _image = null;

        private PartParams _partParams;
        private Action<BoneCombineView> _onButtonClick = null;

        public PartParams PartParams => _partParams;

        public void Connect(PartParams partParams, Action<BoneCombineView> onButtonClick)
        {
            _partParams = partParams;
            _onButtonClick = onButtonClick;

            _image.sprite = partParams.BoneImage;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _onButtonClick?.Invoke(this);
        }
    }
}