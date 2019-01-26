using UnityEngine;

namespace Utilities
{
    public class HorizontalMovementController : MonoBehaviour
    {
        float _speed = 10.0f;

        private void Update()
        {
            var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += move * _speed * Time.deltaTime;
        }
    }
}
