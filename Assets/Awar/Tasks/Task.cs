using Awar.AI;
using Awar.Tasks.Actions;
using UnityEngine;

namespace Awar.Tasks
{
    public abstract class Task : ITask
    {
        public string Name { get; set; }
        public bool  InProgress { get; set; }
        public IAction[] Actions { get; set; }

        protected Task(string name)
        {
            Name = name;
        }
        
        public virtual void Schedule(AIBrain brain)
        {
            InProgress = true;
            Debug.Log("Scheduled actual task!?");
        }

        public abstract IAction Tick(AIBrain brain);
    }
}
