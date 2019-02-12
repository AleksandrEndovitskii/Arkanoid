using UnityEngine;
using UserInterface;
using Utilities;

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour, IInitializable, IUninitializable
    {
        public Canvas UserInterfaceCanvasPrefab;
        public Canvas WindowsCanvasPrefab;
        public UserInterfaceView UserInterfaceViewPrefab;
        public GameOverWindowView GameOverWindowViewPrefab;

        private Canvas _userInterfaceCanvasInstance;
        private Canvas _windowsCanvasInstance;
        private UserInterfaceView _userInterfaceViewInstance;
        private GameOverWindowView _gameOverWindowViewInstance;

        public void Initialize()
        {
            _userInterfaceCanvasInstance = Instantiate(UserInterfaceCanvasPrefab);

            _windowsCanvasInstance = Instantiate(WindowsCanvasPrefab);

            _userInterfaceViewInstance = InstantiateElement(UserInterfaceViewPrefab, _userInterfaceCanvasInstance.transform);

            GameManager.Instance.GameFlowManager.CurrentGameStatusChanged += CurrentGameFlowManagerOnCurrentGameStatusChanged;
        }

        private void CurrentGameFlowManagerOnCurrentGameStatusChanged(GameFlowManager.GameStatus gameStatus)
        {
            if (gameStatus == GameFlowManager.GameStatus.Win ||
                gameStatus == GameFlowManager.GameStatus.Loss)
            {
                _gameOverWindowViewInstance = InstantiateElement(GameOverWindowViewPrefab, _windowsCanvasInstance.transform);
            }
        }

        public void Uninitialize()
        {
            if (_userInterfaceCanvasInstance != null)
            {
                Destroy(_userInterfaceCanvasInstance.gameObject);
                _userInterfaceCanvasInstance = null;
            }

            if (_userInterfaceViewInstance)
            {
                Destroy(_userInterfaceViewInstance.gameObject);
                _userInterfaceViewInstance = null;
            }

            GameManager.Instance.GameFlowManager.CurrentGameStatusChanged -= CurrentGameFlowManagerOnCurrentGameStatusChanged;

            if (_gameOverWindowViewInstance != null)
            {
                Destroy(_gameOverWindowViewInstance.gameObject);
                _gameOverWindowViewInstance.Uninitialize();
                _gameOverWindowViewInstance = null;
            }
        }

        private T InstantiateElement<T>(T prefab, Transform parentContainer) where T : MonoBehaviour, IInitializable
        {
            var instance = Instantiate(prefab);

            instance.gameObject.transform.SetParent(parentContainer);

            instance.Initialize();

            return instance;
        }
    }
}
