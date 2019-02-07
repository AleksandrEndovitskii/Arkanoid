using System;
using GameObjects.Ball;
using GameObjects.Brick.Frame;
using Managers;
using UnityEngine;

namespace GameObjects.Brick
{
    public class BrickView : MonoBehaviour, IBallOnCollisionEnterReactor
    {
        public event Action<BrickView> WasDestroyed = delegate { };

        public BrickType BrickType;
        public Color BrickColor;
        public int Score;

        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;

                if (_health <= 0)
                {
                    GameManager.Instance.ScoreManager.Score += Score;

                    WasDestroyed.Invoke(this);

                    Destroy(this.gameObject);
                }
            }
        }

        public bool Targeted
        {
            get
            {
                return _targeted;
            }
            set
            {
                _targeted = value;

                _frameView.gameObject.SetActive(_targeted);
            }
        }

        [SerializeField]
        private FrameView _frameView;

        private int _initialHealth;
        private int _health;
        private bool _targeted;

        public void Initialize(BrickType brickType)
        {
            BrickType = brickType;
            _initialHealth = GameManager.Instance.BrickHealthManager.GetHealthForBrickType(BrickType);
            Health = _initialHealth;
            BrickColor = GameManager.Instance.BrickColorManager.GetColorForBrickType(BrickType);
            Score = (int)BrickType + 1;
            this.gameObject.GetComponent<Renderer>().material.color = BrickColor;
            Targeted = false;
        }

        public void ReactOnCollisionEnter(BallView ballView)
        {
            Health--;
        }
    }
}
