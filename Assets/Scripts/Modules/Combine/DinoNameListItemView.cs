using System;
using Lifecycle;
using Params;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.Combine
{
    public class DinoNameListItemView : BaseView, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _text = null;

        private PartParams _tailParams;
        private PartParams _bodyParams;
        private PartParams _headParams;
        private Action<PartParams, PartParams, PartParams> _onPointerEnter;
        private Action _onPointerExit;

        public void Connect(PartParams tailParams, PartParams bodyParams, PartParams headParams,
            Action<PartParams, PartParams, PartParams> onPointerEnter, Action onPointerExit)
        {
            _tailParams = tailParams;
            _bodyParams = bodyParams;
            _headParams = headParams;

            _text.text = $"{headParams.Name}{bodyParams.Name}{tailParams.Name}";
            _onPointerEnter = onPointerEnter;
            _onPointerExit = onPointerExit;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _onPointerEnter.Invoke(_tailParams, _bodyParams, _headParams);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _onPointerExit.Invoke();
        }
    }
}