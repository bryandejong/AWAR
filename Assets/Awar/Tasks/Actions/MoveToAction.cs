using Awar.AI;
using UnityEngine;

namespace Awar.Tasks.Actions
{
    public class MoveToAction : IAction
    {
        private Vector3 _target;

        public MoveToAction(Vector3 target)
        {
            _target = target;
        }

        public void Execute(AIBrain brain)
        {
            brain.SetMovementTarget(_target);
        }
    }
}
