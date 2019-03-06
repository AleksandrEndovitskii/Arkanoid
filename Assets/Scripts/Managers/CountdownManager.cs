using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class CountdownManager : MonoBehaviour, IInitializable, IUninitializable
    {
        public event Action<int> SecondsLeftCountChanged = delegate { };

        public int SecondsPassedCountRequaredForBrickMovement = 10;
        private int _secondsLeftCount;
        public int SecondsLeftCount
        {
            get
            {
                return _secondsLeftCount;
            }
            set
            {
                _secondsLeftCount = value;

                SecondsLeftCountChanged.Invoke(_secondsLeftCount);

                if (_secondsLeftCount == 0)
                {
                    _secondsLeftCount = SecondsPassedCountRequaredForBrickMovement;
                }
            }
        }

        private Coroutine _stopwatchCoroutine;

        public void Initialize()
        {
            SecondsLeftCount = SecondsPassedCountRequaredForBrickMovement;

            _stopwatchCoroutine = StartCoroutine(Countdown());
        }

        public void Uninitialize()
        {
            if (_stopwatchCoroutine != null)
            {
                StopCoroutine(_stopwatchCoroutine);
                _stopwatchCoroutine = null;
            }
        }

        private IEnumerator Countdown()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                SecondsLeftCount--;
            }
        }
    }
}
