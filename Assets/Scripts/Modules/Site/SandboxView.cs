using Lifecycle;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.Site
{
    public class SandboxView : BaseView, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject _brush = null;
        [SerializeField] private GameObject _dustGameObject = null;

        private Canvas _canvas;

        private float _clearingStartTime;
        public bool IsClearing { get; private set; }
        public float ClearingDuration => !IsClearing ? 0f : Mathf.Max(Time.time - _clearingStartTime, 0f);

        private void Awake()
        {
            _brush.gameObject.SetActive(false);
            _dustGameObject.gameObject.SetActive(false);
        }

        public void Connect(Canvas canvas)
        {
            _canvas = canvas;
        }

        private void Update()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
                Input.mousePosition, _canvas.worldCamera, out var pos);
            var position = _canvas.transform.TransformPoint(pos);
            _brush.transform.position = position;
            _dustGameObject.transform.position = position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsClearing = true;
            _clearingStartTime = Time.time;
            _brush.gameObject.SetActive(true);
            _dustGameObject.gameObject.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsClearing = false;
            _brush.gameObject.SetActive(false);
            _dustGameObject.gameObject.SetActive(false);
        }
    }
}