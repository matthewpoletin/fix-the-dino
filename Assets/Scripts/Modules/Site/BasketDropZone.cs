using System;
using Lifecycle;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.Site
{
    public class BasketDropZone : BaseView, IDropHandler
    {
        private Action<GameObject> _onDrop;

        public void Connect(Action<GameObject> onDrop)
        {
            _onDrop = onDrop;
        }

        public void OnDrop(PointerEventData eventData)
        {
            _onDrop?.Invoke(eventData.pointerDrag);
        }
    }
}