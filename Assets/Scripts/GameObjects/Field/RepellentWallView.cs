using GameObjects.Ball;
using UnityEngine;

namespace GameObjects.Field
{
    [RequireComponent(typeof(BoxCollider))]
    public class RepellentWallView : WallView
    {
        public override void ReactOnCollisionEnter(BallView ballView)
        {
            base.ReactOnCollisionEnter(ballView);

            //ballView.BounceInOppositeDirection();
        }
    }
}
