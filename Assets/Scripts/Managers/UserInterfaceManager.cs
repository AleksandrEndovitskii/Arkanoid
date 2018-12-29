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

        private UserInterfaceView _userInterfaceViewInstance;

        public void Initialize()
        {
            _userInterfaceViewInstance = InstantiateElement<UserInterfaceView>(UserInterfaceViewPrefab, UserInterfaceCanvas.transform);
        }

        private T InstantiateElement<T>(T prefab, Transform parentContainer) where T : MonoBehaviour
        {
            var instance = Instantiate(prefab);

            instance.gameObject.transform.SetParent(parentContainer);

            return instance;
        }
    }
}
