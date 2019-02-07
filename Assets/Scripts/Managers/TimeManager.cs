using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class TimeManager : MonoBehaviour, IInitializable, IUninitializable
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

        private Coroutine _stopwatchCoroutine;

        public void Initialize()
        {
            SecondsPassedCount = 0;

            _stopwatchCoroutine = StartCoroutine(Stopwatch());
        }

        public void Uninitialize()
        {
            if (_stopwatchCoroutine != null)
            {
                StopCoroutine(_stopwatchCoroutine);
                _stopwatchCoroutine = null;
            }
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
