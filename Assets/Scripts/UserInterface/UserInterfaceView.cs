using Managers;
using UnityEngine;
using Utilities;

namespace UserInterface
{
    public class UserInterfaceView : MonoBehaviour, IInitializable
    {
        public CounterView ScoreCounterView;
        public CounterView SecondsBeforeNextBricksMovementTimerCounterView;

        public void Initialize()
        {
            ScoreManagerOnScoreChanged(GameManager.Instance.ScoreManager.Score);
            GameManager.Instance.ScoreManager.ScoreChanged += ScoreManagerOnScoreChanged;

            TimeManagerOnSecondsLeftCountChanged(GameManager.Instance.CountdownManager.SecondsLeftCount);
            GameManager.Instance.CountdownManager.SecondsLeftCountChanged += TimeManagerOnSecondsLeftCountChanged;
        }

        private void ScoreManagerOnScoreChanged(int score)
        {
            ScoreCounterView.TextMeshProUGUIText.text = score.ToString();
        }

        private void TimeManagerOnSecondsLeftCountChanged(int secondsPassedCount)
        {
            SecondsBeforeNextBricksMovementTimerCounterView.TextMeshProUGUIText.text = secondsPassedCount.ToString();
        }
    }
}
