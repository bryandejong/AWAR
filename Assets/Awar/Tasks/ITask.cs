using System;
using Awar.AI;
using Awar.Tasks.Actions;

namespace Awar.Tasks
{
    public interface ITask
    {
        string Name { get; set; }
        bool InProgress { get; set; }
        IAction[] Actions { get; set; }

        bool Schedule();

        IAction Tick();
    } 
}
