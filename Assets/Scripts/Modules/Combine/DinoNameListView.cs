using System.Collections.Generic;
using Lifecycle;
using UnityEngine;

namespace Modules.Combine
{
    public class DinoNameListView : BaseView
    {
        [SerializeField] private GameObject _dinoNameViewPrefab = null;

        private readonly List<DinoNameItemView> _allListNameViews = new List<DinoNameItemView>();

        public void AddName(string dinoName)
        {
            var dinoNameGameObject = Instantiate(_dinoNameViewPrefab, transform);
            var dinoNameView = dinoNameGameObject.GetComponent<DinoNameItemView>();
            dinoNameView.SetData(dinoName);
            _allListNameViews.Add(dinoNameView);
        }

        public override void Dispose()
        {
            foreach (var itemView in _allListNameViews)
            {
                itemView.Dispose();
                Destroy(itemView.gameObject);
            }

            _allListNameViews.Clear();
        }
    }
}