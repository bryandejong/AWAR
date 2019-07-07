using Awar.AI;
using Awar.Characters;
using Awar.Map.Vegetation;

namespace Awar.Tasks.Actions
{
    public class CutTreeAction : IAction
    {
        private VegetationObject _tree;
        private bool _started = false;

        public CutTreeAction(VegetationObject tree)
        {
            _tree = tree;
        }

        public void Execute(AIBrain brain)
        {
            if (_started == false)
            {
                brain.SetAnimationState(AnimationState.Constructing);
                _started = true;
                return;
            }

            brain.SetAnimationState(AnimationState.Constructing);

            _tree.Health -= 1;
        }
    }
}
