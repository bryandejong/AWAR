namespace Awar.Tasks
{
    public interface ITaskHandler
    {
        ITask[] TaskQueue { get; set; }

        void ExecuteNextTask();

        void Prioritize(ITask task);

        void SetPriority(ITask task, int priority);

        void QueueTask(ITask task);

        void RemoveTask(int taskIndex);

        void RemoveTask(ITask task);
    
        bool InProgress();

        void Tick();
    }
}
