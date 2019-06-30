using Awar.Core;
using UnityEngine;

namespace Awar.Characters
{
    public class Character : AwarBehavior
    {
        [SerializeField] protected Animator Animator = default;

        protected bool IsMoving = false;

        public override void Initialize()
        {
        }

        public override void Tick()
        {
        }

        public void Move(Vector3 direction)
        {
            SetAnimation(AnimationState.Walking);
            IsMoving = true;
        }

        public void StopMoving()
        {
            SetAnimation(AnimationState.Idle);
            IsMoving = false;
        }
        public void SetAnimation(AnimationState state)
        {
            switch (state)
            {
                case(AnimationState.Idle):
                    Animator.SetBool("isMoving", false);
                    Animator.SetBool("isConstructing", false);
                    break;
                case(AnimationState.Walking):
                    Animator.SetBool("isMoving", true);
                    Animator.SetBool("isConstructing", false);
                    break;
                case(AnimationState.Constructing):
                    Animator.SetBool("isMoving", false);
                    Animator.SetBool("isConstructing", true);
                    break;
            }
        }
    }
}
