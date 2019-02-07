using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Managers
{
    public class BrickMovementManager : MonoBehaviour, IInitializable, IUninitializable
    {
        public int SecondsPassedCountRequaredForBrickMovement = 10;

        public void Initialize()
        {
            GameManager.Instance.TimeManager.SecondsPassedCountChanged += TimeManagerOnSecondsPassedCountChanged;
        }

        public void Uninitialize()
        {
            GameManager.Instance.TimeManager.SecondsPassedCountChanged -= TimeManagerOnSecondsPassedCountChanged;
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
            var oneCellHeight = GameManager.Instance.GameObjectsManager._bricksContainerInstance
                .GetComponent<GridLayoutGroup>().cellSize.y;
            var verticalMovementDirectionCoefficient = -1;
            GameManager.Instance.GameObjectsManager._bricksContainerInstance.transform.position +=
                new Vector3(0, verticalMovementDirectionCoefficient * oneCellHeight, 0);
        }
    }
}
