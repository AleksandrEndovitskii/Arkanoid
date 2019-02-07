using System;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class ScoreManager : MonoBehaviour, IInitializable, IUninitializable
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
            Score = 0;
        }

        public void Uninitialize()
        {
            //
        }
    }
}
