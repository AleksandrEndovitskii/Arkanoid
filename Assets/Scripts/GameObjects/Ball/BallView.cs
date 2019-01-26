using UnityEngine;

namespace GameObjects.Ball
{
    public class BallView : MonoBehaviour
    {
        private Vector3 _initialVelocity;

        private float _minVelocity = 10f;
        private float _speed = 10;

        private Vector3 _lastFrameVelocity;
        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            var randomVector = Random.insideUnitCircle.normalized;

            _initialVelocity = randomVector * _speed;

            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = _initialVelocity;
        }

        private void Update()
        {
            _lastFrameVelocity = _rigidbody.velocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var ballOnCollisionEnterReactor = collision.gameObject.GetComponent<IBallOnCollisionEnterReactor>();
            if (ballOnCollisionEnterReactor != null)
            {
                ballOnCollisionEnterReactor.ReactOnCollisionEnter(this);
            }

            Bounce(collision.contacts[0].normal);
        }

        private void Bounce(Vector3 collisionNormal)
        {
            var speed = _lastFrameVelocity.magnitude;
            var direction = Vector3.Reflect(_lastFrameVelocity.normalized, collisionNormal);

            _rigidbody.velocity = direction * Mathf.Max(speed, _minVelocity);
        }
    }
}
