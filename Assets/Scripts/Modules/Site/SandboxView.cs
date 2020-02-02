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
            _brush.gameObject.SetActive(true);
            _dustGameObject.gameObject.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _brush.gameObject.SetActive(false);
            _dustGameObject.gameObject.SetActive(false);
        }
    }
}