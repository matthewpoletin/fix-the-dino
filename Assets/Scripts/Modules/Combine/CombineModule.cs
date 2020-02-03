using System.Collections.Generic;
using System.Linq;
using Core;
using Model;
using Params;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Combine
{
    public class CombineModule : BaseModule
    {
        [SerializeField] private Transform _bonesContainer = null;
        [SerializeField] private GameObject _bonePrefab = null;
        [SerializeField] private DinoNameListView _dinoNameListView = null;
        [SerializeField] private SlotView _tailSlotView = null;
        [SerializeField] private SlotView _bodySlotView = null;
        [SerializeField] private SlotView _headSlotView = null;
        [SerializeField] private Button _continueButton = null;

        private GameController _gameController;

        private readonly Dictionary<PartType, SlotView> _slotByType = new Dictionary<PartType, SlotView>();

        private readonly List<BoneCombineView> _boneCombineViews = new List<BoneCombineView>();

        public override void Activate(GameController gameController, Canvas canvas)
        {
            _continueButton.onClick.RemoveAllListeners();
            _continueButton.onClick.AddListener(OnContinueButtonClick);
            _gameController = gameController;

            _slotByType[_tailSlotView.SlotType] = _tailSlotView;
            _slotByType[_bodySlotView.SlotType] = _bodySlotView;
            _slotByType[_headSlotView.SlotType] = _headSlotView;

            foreach (var collectedPart in _gameController.GameModel.WalkthroughModel.PartsCollected)
            {
                var boneGameObject = Instantiate(_bonePrefab, _bonesContainer);
                var boneCombineView = boneGameObject.GetComponent<BoneCombineView>();
                boneCombineView.Connect(collectedPart, OnCollectedPartClicked);
                _boneCombineViews.Add(boneCombineView);
            }
        }

        public override void OnModuleLoaded()
        {
        }

        private void OnContinueButtonClick()
        {
            if (_gameController.GameModel.WalkthroughModel.Sites.Any(siteModel => !siteModel.IsVisited))
            {
                _gameController.OpenMapModule();
            }
            else
            {
                _gameController.GameModel.CreateNewWalkthroughModel();
                _gameController.OpenMenuModule();
            }
        }

        private void OnCollectedPartClicked(BoneCombineView boneCombineView)
        {
            var slotView = _slotByType[boneCombineView.PartParams.PartType];
            if (slotView.HasItem)
            {
                // TODO: Play shake animation or show error text
                return;
            }

            boneCombineView.transform.SetParent(slotView.transform);
            boneCombineView.transform.localPosition = Vector3.zero;

            if (_tailSlotView.HasItem && _bodySlotView.HasItem && _headSlotView.HasItem)
            {
                var tailBoneCombineView = _tailSlotView.transform.GetChild(0).GetComponent<BoneCombineView>();
                var tailPartParams = tailBoneCombineView.PartParams;
                tailBoneCombineView.Dispose();
                _boneCombineViews.Remove(tailBoneCombineView);
                Destroy(tailBoneCombineView.gameObject);

                var bodyBoneCombineView = _bodySlotView.transform.GetChild(0).GetComponent<BoneCombineView>();
                var bodyPartParams = bodyBoneCombineView.PartParams;
                bodyBoneCombineView.Dispose();
                _boneCombineViews.Remove(bodyBoneCombineView);
                Destroy(bodyBoneCombineView.gameObject);

                var headBoneCombineView = _headSlotView.transform.GetChild(0).GetComponent<BoneCombineView>();
                var headPartParams = headBoneCombineView.PartParams;
                _boneCombineViews.Remove(headBoneCombineView);
                headBoneCombineView.Dispose();
                Destroy(headBoneCombineView.gameObject);

                // TODO: Сделать запись в модели о новом виде динозавра
                _dinoNameListView.Connect(_gameController, tailPartParams, bodyPartParams, headPartParams);
            }
        }

        public override void Dispose()
        {
            _dinoNameListView.Dispose();
            foreach (var boneCombineView in _boneCombineViews)
            {
                boneCombineView.Dispose();
                Destroy(boneCombineView.gameObject);
            }

            _boneCombineViews.Clear();
        }
    }
}