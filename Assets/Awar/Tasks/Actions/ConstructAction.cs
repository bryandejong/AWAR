using Awar.AI;
using Awar.Characters;
using Awar.Construction;

namespace Awar.Tasks.Actions
{
    public class ConstructAction : IAction
    {
        private TargetPosition _targetPosition;
        private ConstructionObject _constructionObject;
        private bool _started = false;

        public ConstructAction(TargetPosition targetPosition, ConstructionObject constructionObject)
        {
            _constructionObject = constructionObject;
            _targetPosition = targetPosition;
        }
        public void Execute(AIBrain brain)
        {
            if (!_started)
            {
                brain.SetRotationTarget(_targetPosition.transform.forward);
                brain.SetAnimationState(AnimationState.Constructing);
                _started = true;
            }

            brain.SetAnimationState(AnimationState.Constructing);

            _constructionObject.AddEffort(10);
        }
    }
}
