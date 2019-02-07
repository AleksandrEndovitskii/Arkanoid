using System.Collections.Generic;
using UnityEngine;

namespace GameObjects.Brick.Frame
{
    public class FrameView : MonoBehaviour
    {
        [SerializeField]
        private List<Renderer> _walls = new List<Renderer>();

        [SerializeField]
        private Color _color;

        private void Awake()
        {
            foreach (var wall in _walls)
            {
                wall.gameObject.GetComponent<Renderer>().material.color = _color;
            }
        }
    }
}
