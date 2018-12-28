using Managers;
using UnityEngine;

namespace UserInterface
{
    public class UserInterfaceView : MonoBehaviour
    {
        public CounterView ScoreCounterView;
        public CounterView SecondsBeforeNextBricksMovementTimerCounterView;

        public void Initialize()
        {
            ScoreManagerOnScoreChanged(GameManager.Instance.ScoreManager.Score);
            GameManager.Instance.ScoreManager.ScoreChanged += ScoreManagerOnScoreChanged;

            TimeManagerOnSecondsPassedCountChanged(GameManager.Instance.TimeManager.SecondsPassedCount);
            GameManager.Instance.TimeManager.SecondsPassedCountChanged += TimeManagerOnSecondsPassedCountChanged;
        }

        private void ScoreManagerOnScoreChanged(int score)
        {
            ScoreCounterView.TextMeshProUGUIText.text = score.ToString();
        }

        private void TimeManagerOnSecondsPassedCountChanged(int secondsPassedCount)
        {
            SecondsBeforeNextBricksMovementTimerCounterView.TextMeshProUGUIText.text = secondsPassedCount.ToString();
        }
    }
}
