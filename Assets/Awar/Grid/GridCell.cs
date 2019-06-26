using System.Linq.Expressions;
using UnityEngine;

namespace Awar.Grid
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer = default;


        public void EnableHover(float alpha)
        {
            _spriteRenderer.material.color = new Color(.6f, .55f, .8f, alpha);
            _spriteRenderer.enabled = true;
        }

        public void DisableHover()
        {
            _spriteRenderer.enabled = false;
        }

    }
}
