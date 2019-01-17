using UnityEngine;
using UserInterface;
using Utilities;

/*
UI:
    По окончанию игры должно выводится окно с результатами(Выиграл/Проиграл и кнопка рестарта)
 */

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour, IInitializable
    {
        public Canvas UserInterfaceCanvas;

        public UserInterfaceView UserInterfaceViewPrefab;
        public GameOverWindowView GameOverWindowViewPrefab;

        private UserInterfaceView _userInterfaceViewInstance;
        private GameOverWindowView _gameOverWindowViewInstance;

        public void Initialize()
        {
            _userInterfaceViewInstance = InstantiateElement<UserInterfaceView>(UserInterfaceViewPrefab, UserInterfaceCanvas.transform);
            //_gameOverWindowViewInstance = InstantiateElement<GameOverWindowView>(GameOverWindowViewPrefab, UserInterfaceCanvas.transform);
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
