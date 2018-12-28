using UnityEngine;

namespace GameObjects.Ball
{
    public class BallView : MonoBehaviour
    {
        public void BounceInOppositeDirection()
        {
            //
        }

        private void OnCollisionEnter(Collision collision)
        {
            var ballOnCollisionEnterReactor = collision.gameObject.GetComponent<IBallOnCollisionEnterReactor>();
            if (ballOnCollisionEnterReactor == null)
            {
                return;
            }
            ballOnCollisionEnterReactor.ReactOnCollisionEnter(this);
        }
    }
}
