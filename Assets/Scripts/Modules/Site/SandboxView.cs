using Lifecycle;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.Site
{
    public class SandboxView : BaseView, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject _brush = null;

        private void Awake()
        {
            _brush.gameObject.SetActive(false);
        }

        private void Update()
        {
            _brush.transform.position = Input.mousePosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _brush.gameObject.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _brush.gameObject.SetActive(false);
        }
    }
}