using UnityEngine;

namespace Awar.Grid
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void EnableHover()
        {
            _spriteRenderer.enabled = true;
        }

        public void DisableHover()
        {
            _spriteRenderer.enabled = false;
        }

    }
}
