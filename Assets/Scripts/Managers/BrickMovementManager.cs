using UnityEngine;
using Utilities;

namespace Managers
{
    public class BrickMovementManager : MonoBehaviour, IInitializable
    {
        public int SecondsPassedCountRequaredForBrickMovement = 10;

        public void Initialize()
        {
            GameManager.Instance.TimeManager.SecondsPassedCountChanged += TimeManagerOnSecondsPassedCountChanged;
        }

        private void TimeManagerOnSecondsPassedCountChanged(int secondsPassedCount)
        {
            if (secondsPassedCount % SecondsPassedCountRequaredForBrickMovement == 0)
            {
                MoveBricks();
            }
        }

        private void MoveBricks()
        {
            //Каждые n секунд, кирпичи опускаются на одну ячейку вниз;
        }
    }
}
