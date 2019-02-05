using UnityEngine;
using UserInterface;
using Utilities;

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour, IInitializable, IUninitializable
    {
        public Canvas UserInterfaceCanvas;

        public UserInterfaceView UserInterfaceViewPrefab;
        public GameOverWindowView GameOverWindowViewPrefab;

        private UserInterfaceView _userInterfaceViewInstance;
        private GameOverWindowView _gameOverWindowViewInstance;

        public void Initialize()
        {
            _userInterfaceViewInstance = InstantiateElement<UserInterfaceView>(UserInterfaceViewPrefab, UserInterfaceCanvas.transform);

            GameManager.Instance.GameFlowManager.CurrentGameStatusChanged += CurrentGameFlowManagerOnCurrentGameStatusChanged;
        }

        private void CurrentGameFlowManagerOnCurrentGameStatusChanged(GameFlowManager.GameStatus gameStatus)
        {
            _gameOverWindowViewInstance = InstantiateElement<GameOverWindowView>(GameOverWindowViewPrefab, UserInterfaceCanvas.transform);
        }

        public void Uninitialize()
        {
            Destroy(_userInterfaceViewInstance.gameObject);
            _userInterfaceViewInstance = null;

            GameManager.Instance.GameFlowManager.CurrentGameStatusChanged -= CurrentGameFlowManagerOnCurrentGameStatusChanged;
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
