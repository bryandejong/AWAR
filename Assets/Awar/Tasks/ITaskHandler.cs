namespace Awar.Tasks
{
    public interface ITaskHandler
    {
        ITask[] TaskQueue();

        void ExecuteNextTask();

        void SetPriority(ITask task);

        void QueueTask(ITask task);

        void RemoveTask(int taskIndex);

        void RemoveTask(ITask task);
    }
}
