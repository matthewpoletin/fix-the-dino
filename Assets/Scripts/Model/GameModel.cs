using System;
using System.Collections.Generic;
using Params;
using UnityEngine.Assertions;

namespace Model
{
    public class GameModel
    {
        public GameParams Params { get; private set; }

        public WalkthroughModel WalkthroughModel { get; private set; }

        public GameModel(GameParams gameParams)
        {
            Assert.IsNotNull(gameParams, "gameParams == null");
            Params = gameParams;
        }

        public void CreateNewWalkthroughModel()
        {
            WalkthroughModel = new WalkthroughModel(Params.AllSites);
        }
    }

    public class WalkthroughModel
    {
        public WalkthroughModel(List<SiteParams> allSites)
        {
            foreach (var siteParams in allSites)
            {
                var siteModel = new SiteModel(siteParams);
                _sites.Add(siteModel);
            }
        }

        private readonly List<SiteModel> _sites = new List<SiteModel>();
        public IReadOnlyList<SiteModel> Sites => _sites;

        public SiteModel CurrentSite { get; set; }

        public event Action CollectedPartsChanged;
        private readonly List<PartParams> _partsCollected = new List<PartParams>();
        public IReadOnlyList<PartParams> PartsCollected => _partsCollected;

        public void CollectNewPart(PartParams partParams)
        {
            _partsCollected.Add(partParams);
            CollectedPartsChanged?.Invoke();
        }
    }

    public class SiteModel
    {
        public SiteModel(SiteParams siteParams)
        {
            Params = siteParams;
        }

        public event Action<bool> OnVisitedChanged;

        private bool _isVisited;

        public SiteParams Params { get; private set; }

        public bool IsVisited
        {
            get => _isVisited;
            set
            {
                var newVisitedStatus = value;
                if (newVisitedStatus != _isVisited)
                {
                    _isVisited = newVisitedStatus;
                    OnVisitedChanged?.Invoke(_isVisited);
                }
            }
        }
    }
}