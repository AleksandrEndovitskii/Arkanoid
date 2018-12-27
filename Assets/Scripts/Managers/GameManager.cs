using UnityEngine;

/*
Геймплей:
    Задача: разбить кирпич, находящийся в произвольном месте.
    Кирпичи могут быть разных видов, в зависимости от вида требуется k попаданий шариком для уничтожения кирпича.
    Каждые n секунд, кирпичи опускаются на одну ячейку вниз;

Бонусные задачи:
    - Написать шейдер, уводящий цвет кирпича в красный по мере приближения к нижней границе экрана.
    - рандомная генерация уровней
    - AssetBundles
 */

namespace Managers
{
    [RequireComponent(typeof(UserInterfaceManager))]
    [RequireComponent(typeof(GameObjectsManager))]
    [RequireComponent(typeof(GameFlowManager))]
    public class GameManager : MonoBehaviour
    {
        // static instance of GameManager which allows it to be accessed by any other script 
        public static GameManager Instance;

        public UserInterfaceManager UserInterfaceManager
        {
            get { return this.gameObject.GetComponent<UserInterfaceManager>(); }
        }

        public GameObjectsManager GameObjectsManager
        {
            get { return this.gameObject.GetComponent<GameObjectsManager>(); }
        }

        public GameFlowManager GameFlowManager
        {
            get { return this.gameObject.GetComponent<GameFlowManager>(); }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject); // sets this to not be destroyed when reloading scene 
            }
            else
            {
                if (Instance != this)
                {
                    // this enforces our singleton pattern, meaning there can only ever be one instance of a GameManager 
                    Destroy(gameObject);
                }
            }

            Initialization();
        }

        private void Initialization()
        {
            UserInterfaceManager.Initialize();
            GameObjectsManager.Initialize();
            GameFlowManager.Initialize();
        }
    }
}
