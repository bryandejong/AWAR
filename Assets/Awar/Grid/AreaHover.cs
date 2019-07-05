using Awar.Core;
using UnityEngine;

namespace Awar.Grid
{
    public class AreaHover : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer = default;

        public void SetPosition(Vector3 position)
        {
            Vector3 newPosition = new Vector3(position.x, .04f, position.z);
            transform.position = newPosition;
        }

        public void SetDimensions(Vector2 dimensions)
        {
            _renderer.size = dimensions;
        }
    }
}
