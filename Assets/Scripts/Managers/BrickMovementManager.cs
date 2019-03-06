using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Managers
{
    public class BrickMovementManager : MonoBehaviour, IInitializable, IUninitializable
    {
        public void Initialize()
        {
            GameManager.Instance.CountdownManager.SecondsLeftCountChanged += CountdownManagerOnSecondsLeftCountChanged;
        }

        public void Uninitialize()
        {
            GameManager.Instance.CountdownManager.SecondsLeftCountChanged -= CountdownManagerOnSecondsLeftCountChanged;
        }

        private void CountdownManagerOnSecondsLeftCountChanged(int secondsLeftCount)
        {
            if (secondsLeftCount == 0)
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
