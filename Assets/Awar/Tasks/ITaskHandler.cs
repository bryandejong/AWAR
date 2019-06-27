using Awar.AI;
using Awar.Tasks.Actions;

namespace Awar.Tasks
{
    public interface ITaskHandler
    {
        ITask[] TaskQueue { get; set; }
        IAction CurrentAction { get; set; }

        void ScheduleNextTask();

        void Prioritize(ITask task);

        void SetPriority(ITask task, int priority);

        void QueueTask(ITask task);

        void RemoveTask(int taskIndex);

        void RemoveTask(ITask task);
    
        bool InProgress();

        void Tick();
    }
}
