using System.Collections.Generic;
using Core;
using UnityEngine;
using Utils;

namespace Modules.Site
{
    public class SiteModule : BaseModule
    {
        [SerializeField] private CountdownWidget _countdownWidget = null;
        [SerializeField] private SandboxView _sandboxView = null;
        [SerializeField] private BasketView _basketView = null;
        [SerializeField] private GameObject _bonePrefab = null;
        [SerializeField] private Transform _sandboxContainer = null;

        private GameController _gameController;
        private Canvas _canvas;

        private Timer _timer;
        private readonly List<BoneSiteView> _boneSiteViews = new List<BoneSiteView>();

        public override void Activate(GameController gameController, Canvas canvas)
        {
            _gameController = gameController;
            _canvas = canvas;

            _basketView.Connect();
            _sandboxView.Connect(_canvas);

            for (var i = 0; i < 20; i++)
            {
                CreateRandomPart();
            }
        }

        public override void OnModuleLoaded()
        {
            _timer = new Timer(_gameController.GameModel.Params.SiteDuration);
            _timer.OnTimerElapsed += OnTimerElapsed;
            _countdownWidget.SetTimer(_timer);
        }

        private void CreateRandomPart()
        {
            var boneGameObject = Instantiate(_bonePrefab, _sandboxContainer);
            var boneSiteView = boneGameObject.GetComponent<BoneSiteView>();

            boneGameObject.transform.position += new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 3f), 0);
            var allParts = _gameController.GameModel.Params.GetAllParts();
            var randomPart = allParts[Random.Range(0, allParts.Count)];
            boneSiteView.Connect(_gameController, _canvas, randomPart, OnPointerEnterBone);

            _boneSiteViews.Add(boneSiteView);
        }

        private void OnTimerElapsed()
        {
            // TODO: Pause before next module start
            // var sequence = DOTween.Sequence();
            // sequence.InsertCallback(1f, () => { _gameController.OpenCombineModule(); });
            // sequence.SetAutoKill(true);

            _gameController.OpenCombineModule();
        }

        private void OnPointerEnterBone(BoneSiteView boneSiteView)
        {
            if (_sandboxView.IsClearing)
            {
                boneSiteView.IsFound = true;
            }
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            _timer?.Tick(deltaTime);
        }

        public override void Dispose()
        {
            _countdownWidget.Dispose();
            foreach (var boneSiteView in _boneSiteViews)
            {
                boneSiteView.Dispose();
                Destroy(boneSiteView.gameObject);
            }
            _boneSiteViews.Clear();
        }
    }
}