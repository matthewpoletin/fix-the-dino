using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Modules.Map
{
    public class MapModule : BaseModule
    {
        [SerializeField] private List<MapSiteView> _destinations = null;

        private GameController _gameController;

        public override void Activate(GameController gameController)
        {
            _gameController = gameController;

            foreach (var mapSiteView in _destinations)
            {
                var walkthroughModel = gameController.GameModel.WalkthroughModel;
                var siteModel = walkthroughModel.Sites.FirstOrDefault(site => site.Params == mapSiteView.SiteParams);

                if (siteModel == null)
                {
                    Debug.LogError("Site of this type not found");
                }

                mapSiteView.Connect(_gameController, walkthroughModel, siteModel);
            }
        }

        public override void OnModuleLoaded()
        {
        }
    }
}