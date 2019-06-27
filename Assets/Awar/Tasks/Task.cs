using Awar.AI;
using Awar.Tasks.Actions;
using UnityEngine;

namespace Awar.Tasks
{
    public abstract class Task : ITask
    {
        public string Name { get; set; }
        public bool  InProgress { get; set; }
        protected AIBrain Brain { get; set; }
        public IAction[] Actions { get; set; }

        protected Task(AIBrain brain)
        {
            Brain = brain;
        }
        
        public virtual bool Schedule()
        {
            InProgress = true;
            return true;
        }

        public abstract IAction Tick();
    }
}
