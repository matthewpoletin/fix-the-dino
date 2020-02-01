using Core;
using Model;
using Params;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.Map
{
    public class MapSiteView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TextMeshProUGUI _nameText = null;
        [SerializeField] private GameObject _unvisitedState = null;
        [SerializeField] private GameObject _visitedState = null;
        [SerializeField] private SiteParams _siteParams = null;
        
        public Object SiteParams => _siteParams;

        private GameController _gameController;
        private WalkthroughModel _walkthroughModel;
        private SiteModel _siteModel;

        public void Connect(GameController gameController, WalkthroughModel walkthroughModel, SiteModel siteModel)
        {
            _gameController = gameController;
            _walkthroughModel = walkthroughModel;
            _siteModel = siteModel;

            _nameText.text = siteModel.Params.Name;

            _siteModel.OnVisitedChanged += OnSiteStatusChanged;
            OnSiteStatusChanged(_siteModel.IsVisited);
        }

        private void OnSiteStatusChanged(bool isVisited)
        {
            _unvisitedState.SetActive(!isVisited);
            _visitedState.SetActive(isVisited);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_siteModel.IsVisited)
            {
                _walkthroughModel.CurrentSite = _siteModel;
                _gameController.OpenSiteModule();
            }
        }
    }
}