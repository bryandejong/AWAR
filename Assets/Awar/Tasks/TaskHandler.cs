namespace Awar.Tasks
{
    public class TaskHandler : ITaskHandler
    {
        public ITask[] TaskQueue { get; set; }

        public TaskHandler()
        {
            TaskQueue = new ITask[5];
        }

        public void ExecuteNextTask()
        {
            TaskQueue[0].Execute();
        }

        public void Prioritize(ITask task)
        {
            ITask[] newQueue = new ITask[TaskQueue.Length];
            newQueue[0] = task;
            for (int i = 1; i < TaskQueue.Length; i++)
            {
                if (task == TaskQueue[i]) { continue; }

                newQueue[i] = TaskQueue[i];
            }
        }

        public void SetPriority(ITask task, int priority)
        {
            throw new System.NotImplementedException();
        }

        public void QueueTask(ITask task)
        {
            ITask[] newQueue = new ITask[TaskQueue.Length + 1];
            newQueue[TaskQueue.Length + 1] = task;
        }

        public void RemoveTask(int taskIndex)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveTask(ITask task)
        {
            throw new System.NotImplementedException();
        }

        public bool InProgress()
        {
            ITask task = TaskQueue[0];
            if (task != null)
            {
                return task.InProgress;
            }

            return false;
        }

        public void Tick()
        {
            ITask task = TaskQueue[0];
            task.Tick();
        }
    }
}
