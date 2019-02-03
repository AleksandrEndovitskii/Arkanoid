using System;
using GameObjects.Ball;
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
                    Destroy(this.gameObject);

                    GameManager.Instance.ScoreManager.Score += Score;

                    WasDestroyed.Invoke(this);
                }
            }
        }

        private int _initialHealth;
        private int _health;

        public void Initialize(BrickType brickType)
        {
            BrickType = brickType;
            _initialHealth = GameManager.Instance.BrickHealthManager.GetHealthForBrickType(BrickType);
            Health = _initialHealth;
            BrickColor = GameManager.Instance.BrickColorManager.GetColorForBrickType(BrickType);
            Score = (int)BrickType;
            this.gameObject.GetComponent<Renderer>().material.color = BrickColor;
        }

        public void ReactOnCollisionEnter(BallView ballView)
        {
            Health--;
        }
    }
}
