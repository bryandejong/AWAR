using Awar.AI;
using Awar.Map.Vegetation;
using Awar.Tasks.Actions;

namespace Awar.Tasks.TaskDefinitions
{
    public class WoodcuttingTask : Task
    {
        private VegetationTree _tree;


        public WoodcuttingTask(AIBrain brain, VegetationTree tree) : base(brain)
        {
            _tree = tree;
        }

        public override bool Schedule()
        {
            base.Schedule();
            
            
            Actions = new IAction[]
            {
                new MoveToAction(_tree.transform.position)
            };
            return true;
        }

        public override IAction Tick()
        {

            return Actions[0];
        }
    }
}
