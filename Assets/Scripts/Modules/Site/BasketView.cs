using UnityEngine;

namespace Modules.Site
{
    public class BasketView : MonoBehaviour
    {
        [SerializeField] private BasketDropZone _dropZone = null;
        [SerializeField] private Transform _basketContainer = null;

        public void Connect()
        {
            _dropZone.Connect(OnItemDrop);
        }

        private void OnItemDrop(GameObject item)
        {
            var boneSiteView = item.GetComponent<BoneSiteView>();
            if (boneSiteView == null)
            {
                return;
            }
            
            boneSiteView.transform.SetParent(_basketContainer);
            boneSiteView.IsCollected = true;
        }
    }
}