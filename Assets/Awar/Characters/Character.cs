using UnityEngine;

namespace Awar.Characters
{
    public class Character : AwarObject
    {
        [SerializeField] private Animator _animator = default;

        protected bool IsMoving = false;

        public override void Initialize()
        {
        }

        public override void Tick()
        {
        }

        public void Move(Vector3 direction)
        {
            _animator.SetBool("isMoving", true);
            IsMoving = true;
        }

        public void StopMoving()
        {
            _animator.SetBool("isMoving", false);
            IsMoving = false;
        }
    }
}
