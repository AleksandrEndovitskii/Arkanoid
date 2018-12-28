using GameObjects.Ball;
using UnityEngine;

namespace GameObjects.Racket
{
    public class RacketView : MonoBehaviour, IBallOnCollisionEnterReactor
    {
        public void ReactOnCollisionEnter(BallView ballView)
        {
            ballView.BounceInOppositeDirection();
        }
    }
}
