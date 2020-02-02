using System.Collections.Generic;
using Core;
using Lifecycle;
using Params;
using UnityEngine;

namespace Modules.Combine
{
    public class DinoNameListView : BaseView
    {
        [SerializeField] private GameObject _dinoNameViewPrefab = null;
        [SerializeField] private DinoCardView _dinoCardView = null;

        private GameController _gameController;

        private readonly List<DinoNameListItemView> _allListNameViews = new List<DinoNameListItemView>();

        private void Awake()
        {
            _dinoCardView.gameObject.SetActive(false);
        }

        public void Connect(GameController gameController, PartParams tailParams, PartParams bodyParams, PartParams headParams)
        {
            _gameController = gameController;
            
            var dinoNameGameObject = Instantiate(_dinoNameViewPrefab, transform);
            var dinoNameView = dinoNameGameObject.GetComponent<DinoNameListItemView>();
            dinoNameView.Connect(tailParams, bodyParams, headParams, OnPointerEnter, OnPointerExit);
            _allListNameViews.Add(dinoNameView);
        }

        private void OnPointerEnter(PartParams tailParams, PartParams bodyParams, PartParams headParams)
        {
            _dinoCardView.gameObject.SetActive(true);
            _dinoCardView.SetData(tailParams, bodyParams, headParams, _gameController.GameModel.Params.ReviewParams);
        }

        private void OnPointerExit()
        {
            _dinoCardView.CleanUp();
            _dinoCardView.gameObject.SetActive(false);
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