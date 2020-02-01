using Model;
using Params;
using UnityEngine;

namespace Core
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private GameParams _gameParams = null;
        [SerializeField] private Transform _moduleContainer = null;

        private GameController _gameController;
        private GameModel _gameModel;

        private void Awake()
        {
            _gameModel = new GameModel(_gameParams);
            _gameController = new GameController(_gameModel, _moduleContainer);

            _gameController.OpenMenuModule();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            _gameController.Tick(deltaTime);
        }
    }
}