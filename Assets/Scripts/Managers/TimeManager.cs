using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class TimeManager : MonoBehaviour, IInitializable
    {
        public event Action<int> SecondsPassedCountChanged = delegate { };

        private int _secondsPassedCount;
        public int SecondsPassedCount
        {
            get
            {
                return _secondsPassedCount;
            }
            set
            {
                _secondsPassedCount = value;

                SecondsPassedCountChanged.Invoke(_secondsPassedCount);
            }
        }

        public void Initialize()
        {
            StartCoroutine(Stopwatch());
        }

        private IEnumerator Stopwatch()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                SecondsPassedCount++;
            }
        }
    }
}
