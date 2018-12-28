using System;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public event Action<int> ScoreChanged = delegate { };

        private int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;

                ScoreChanged.Invoke(_score);
            }
        }

        public void Initialize()
        {
            //
        }
    }
}
