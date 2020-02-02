﻿using System.Collections.Generic;
using Core;
using Modules.Common;
using Params;
using UnityEngine;

namespace Modules.Combine
{
    public class CombineModule : BaseModule
    {
        [SerializeField] private CountdownWidget _countdownWidget = null;
        [SerializeField] private Transform _bonesContainer = null;
        [SerializeField] private GameObject _bonePrefab = null;
        [SerializeField] private DinoNameListView _dinoNameListView = null;
        [SerializeField] private SlotView _tailSlotView = null;
        [SerializeField] private SlotView _bodySlotView = null;
        [SerializeField] private SlotView _headSlotView = null;

        private GameController _gameController;
        private Timer _timer;

        private readonly Dictionary<PartType, SlotView> _slotByType = new Dictionary<PartType, SlotView>();

        public override void Activate(GameController gameController)
        {
            _gameController = gameController;

            _slotByType[_tailSlotView.SlotType] = _tailSlotView;
            _slotByType[_bodySlotView.SlotType] = _bodySlotView;
            _slotByType[_headSlotView.SlotType] = _headSlotView;

            foreach (var collectedPart in _gameController.GameModel.WalkthroughModel.PartsCollected)
            {
                var boneGameObject = Instantiate(_bonePrefab, _bonesContainer);
                var boneCombineView = boneGameObject.GetComponent<BoneCombineView>();
                boneCombineView.Connect(collectedPart, OnCollectedPartClicked);
            }
        }

        public override void OnModuleLoaded()
        {
            _timer = new Timer(_gameController.GameModel.Params.CombineDuration);
            _timer.OnTimerElapsed += OnTimerElapsed;
            _countdownWidget.SetTimer(_timer);
        }

        private void OnTimerElapsed()
        {
            // TODO: Вернуть позже для добавление задержки к переключению
            // var sequence = DOTween.Sequence();
            // sequence.InsertCallback(1f, _gameController.OpenMenuModule);
            // sequence.SetAutoKill(true);

            _gameController.OpenMenuModule();
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            _timer?.Tick(deltaTime);
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
                Destroy(tailBoneCombineView.gameObject);

                var bodyBoneCombineView = _bodySlotView.transform.GetChild(0).GetComponent<BoneCombineView>();
                var bodyPartParams = bodyBoneCombineView.PartParams;
                bodyBoneCombineView.Dispose();
                Destroy(bodyBoneCombineView.gameObject);

                var headBoneCombineView = _headSlotView.transform.GetChild(0).GetComponent<BoneCombineView>();
                var headPartParams = headBoneCombineView.PartParams;
                headBoneCombineView.Dispose();
                Destroy(headBoneCombineView.gameObject);

                // TODO: Сделать запись в модели о новом виде динозавра
                _dinoNameListView.AddName($"{headPartParams.Name}{bodyPartParams.Name}{tailPartParams.Name}");
            }
        }
    }
}