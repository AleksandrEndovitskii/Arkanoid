using GameObjects.Ball;
using UnityEngine;

namespace GameObjects.Field
{
    [RequireComponent(typeof(BoxCollider))]
    public class RepellentWallView : WallView, IBallOnCollisionEnterReactor
    {
        public void ReactOnCollisionEnter(BallView ballView)
        {
            //
        }
    }
}
