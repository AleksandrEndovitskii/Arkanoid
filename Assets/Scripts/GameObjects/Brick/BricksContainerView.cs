using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects.Brick
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class BricksContainerView : MonoBehaviour
    {
        public event Action BrickPositioningCompleted = delegate { };

        private void OnEnable()
        {
            StartCoroutine(WaitForEndOfFrame());
        }

        private IEnumerator WaitForEndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            this.gameObject.GetComponent<GridLayoutGroup>().enabled = false;

            BrickPositioningCompleted.Invoke();
        }
    }
}
