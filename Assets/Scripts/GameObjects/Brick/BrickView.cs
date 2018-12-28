using Managers;
using UnityEngine;

namespace GameObjects.Brick
{
    public class BrickView : MonoBehaviour
    {
        public BrickType BrickType;

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
        }
    }
}
