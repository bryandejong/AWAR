using Awar.AI;
using Awar.Map.Vegetation;
using Awar.Tasks.Actions;
using Cinemachine;
using UnityEngine;

namespace Awar.Tasks.TaskDefinitions
{
    public class WoodcuttingTask : Task
    {
        private VegetationObject _tree;


        public WoodcuttingTask(AIBrain brain, VegetationObject tree) : base(brain)
        {
            _tree = tree;
            tree.Targeted = true;
        }

        public override bool Schedule()
        {
            base.Schedule();


            Actions = new IAction[]
            {
                new MoveToAction(_tree.transform.position),
                new CutTreeAction(_tree), 
            };
            return true;
        }

        public override IAction Tick()
        {
            if (Vector3.Distance(Brain.transform.position, _tree.transform.position) > 1)
                return Actions[0];
            if (_tree.Health > 0)
                return Actions[1];

            Object.Destroy(_tree.gameObject);
            return null;
        }
    }
}
