using System.Linq.Expressions;
using UnityEngine;

namespace Awar.Grid
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer = default;

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
