using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace UserInterface
{
    public class GameOverWindowView : MonoBehaviour, IInitializable, IUninitializable
    {
        [SerializeField] private TextMeshProUGUI _gameOverTextMeshProUgui;

        public void Initialize()
        {
            switch (GameManager.Instance.GameFlowManager.CurrentGameStatus)
            {
                case GameFlowManager.GameStatus.InProgress:
                    _gameOverTextMeshProUgui.text = "";
                    break;
                case GameFlowManager.GameStatus.Win:
                    _gameOverTextMeshProUgui.text = GameFlowManager.GameStatus.Win.ToString();
                    break;
                case GameFlowManager.GameStatus.Los:
                    _gameOverTextMeshProUgui.text = GameFlowManager.GameStatus.Los.ToString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(GameManager.Instance.GameFlowManager.CurrentGameStatus),
                        GameManager.Instance.GameFlowManager.CurrentGameStatus, null);
            }
        }

        public void Uninitialize()
        {
            //
        }

        public void OnRestartGameButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //GameManager.Instance.Reinitialize();
        }
    }
}
