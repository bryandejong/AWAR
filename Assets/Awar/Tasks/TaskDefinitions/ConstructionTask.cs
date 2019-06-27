using System;
using Awar.AI;
using Awar.Construction;
using Awar.Tasks.Actions;
using UnityEngine;

namespace Awar.Tasks.TaskDefinitions
{
    public class ConstructionTask : Task
    {
        private ConstructionObject _constructionTarget;
        private TargetPosition _targetPosition;

        public ConstructionTask(string name, ConstructionObject constructionTarget) : base(name)
        {
            _constructionTarget = constructionTarget;
        }
        
        public override void Schedule(AIBrain brain)
        {
            base.Schedule(brain);
            TargetPosition targetPosition = _constructionTarget.GetEmptyPosition();

            _targetPosition = targetPosition;
            _targetPosition.TargetedBy = brain;
            
            Actions = new IAction[]
            {
                new MoveToAction(_targetPosition.transform.position),
                new ConstructAction(_targetPosition, _constructionTarget), 
            };

        }

        public override IAction Tick(AIBrain brain)
        {
            if (_constructionTarget != null)
            {
                if (_targetPosition?.Occupant != brain && _targetPosition)
                {
                    return Actions[0];
                }

                return Actions[1];
            }
            return null;
        }
    }
}
