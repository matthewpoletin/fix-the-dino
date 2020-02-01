using Lifecycle;
using UnityEngine;

namespace Modules.Combine
{
    public class DinoNameListView : BaseView
    {
        [SerializeField] private GameObject _dinoNameViewPrefab = null;

        public void AddName(string dinoName)
        {
            var dinoNameGameObject = GameObject.Instantiate(_dinoNameViewPrefab, transform);
            var dinoNameView = dinoNameGameObject.GetComponent<DinoNameItemView>();
            dinoNameView.SetData(dinoName);
        }
    }
}