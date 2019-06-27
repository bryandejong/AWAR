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

        public ConstructionTask(AIBrain brain, ConstructionObject constructionTarget) : base(brain)
        {
            _constructionTarget = constructionTarget;
        }
        
        public override bool Schedule()
        {
            base.Schedule();
            TargetPosition targetPosition = _constructionTarget.GetEmptyPosition();

            if (targetPosition == null) return false;

            if (targetPosition.TargetedBy != null || targetPosition.Occupant != null)
            {
                return false;
            }

            _targetPosition = targetPosition;
            _targetPosition.TargetedBy = Brain;
            
            Actions = new IAction[]
            {
                new MoveToAction(_targetPosition.transform.position),
                new ConstructAction(_targetPosition, _constructionTarget), 
            };
            return true;
        }

        public override IAction Tick()
        {
            if (_constructionTarget == null) return null;

            if (_targetPosition?.Occupant != Brain && _targetPosition)
            {
                return Actions[0];
            }

            return Actions[1];
        }
    }
}
