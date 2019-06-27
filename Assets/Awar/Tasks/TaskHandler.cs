using Awar.AI;
using Awar.Tasks.Actions;
using UnityEngine;
using AnimationState = Awar.Characters.AnimationState;

namespace Awar.Tasks
{
    public class TaskHandler : ITaskHandler
    {
        public ITask[] TaskQueue { get; set; }
        public IAction CurrentAction { get; set; }
        public AIBrain Brain { get; set; }

        public TaskHandler(AIBrain brain)
        {
            TaskQueue = new ITask[0];
            Brain = brain;
        }

        public void ScheduleNextTask()
        {
            Debug.Log("Scheduled next task");
            TaskQueue[0]?.Schedule(Brain);
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
            newQueue[TaskQueue.Length] = task;
            TaskQueue = newQueue;
        }

        public void RemoveTask(int taskIndex)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveTask(ITask task)
        {
            throw new System.NotImplementedException();
        }

        public void CompleteTask()
        {
            if (TaskQueue.Length < 1)
            {
                TaskQueue = new ITask[0];
                return;
            }

            for (int i = 0; i < TaskQueue.Length - 1; i++)
            {
                TaskQueue[i] = TaskQueue[i + 1];
            }
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
            IAction action = task.Tick(Brain);
            if (action != null)
            {
                CurrentAction = action;
                action.Execute(Brain);
            }
            else
            {
                Brain.SetAnimationState(AnimationState.Idle);
                CompleteTask();
            }
        }
    }
}
