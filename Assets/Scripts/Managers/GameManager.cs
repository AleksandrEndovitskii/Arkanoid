using UnityEngine;

/*
Геймплей:
    Есть бита, которой мы управляем влево-вправо (реализация управления за Вами), 
        и шарик, который отлетает от этой биты и краев экрана.
    Задача: разбить кирпич, находящийся в произвольном месте.
    Кирпичи могут быть разных видов, в зависимости от вида требуется k попаданий шариком для уничтожения кирпича.
    Каждые n секунд, кирпичи опускаются на одну ячейку вниз;
    В случае, если шарик касается нижней границы экрана, игра считается проигранной.
    В случае, если все кирпичи уничтожены, игра считается выигранной;

UI:
    В интерфейсе должны отображаться: 
        счётчик текущих очков
        таймер до следующего опускания блоков
    По окончанию игры должно выводится окно с результатами(Выиграл/Проиграл и кнопка рестарта)

Бонусные задачи:
    - Написать шейдер, уводящий цвет кирпича в красный по мере приближения к нижней границе экрана.
    - рандомная генерация уровней
    - AssetBundles
 */

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        // static instance of GameManager which allows it to be accessed by any other script 
        public static GameManager Instance;

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
            //
        }
    }
}
