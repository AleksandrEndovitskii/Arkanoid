using GameObjects.Ball;
using UnityEngine;

namespace GameObjects.Field
{
    [RequireComponent(typeof(BoxCollider))]
    public class WallView : MonoBehaviour
    {
        public virtual void ReactOnCollisionEnter(BallView ballView)
        {
            //
        }
    }
}
