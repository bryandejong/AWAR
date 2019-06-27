using System.Collections.Generic;
using Awar.AI;
using Awar.Tasks.Actions;
using UnityEngine;
using AnimationState = Awar.Characters.AnimationState;

namespace Awar.Tasks
{
    public class TaskHandler : ITaskHandler
    {
        public List<ITask> TaskQueue { get; set; }
        public IAction CurrentAction { get; set; }
        public AIBrain Brain { get; set; }

        public TaskHandler(AIBrain brain)
        {
            TaskQueue = new List<ITask>(0);
            Brain = brain;
        }

        public void ScheduleNextTask()
        {
            bool isValidTask = TaskQueue[0].Schedule();

            if (!isValidTask)
            {
                CompleteTask(TaskQueue[0]);
            }
        }

        public void Prioritize(ITask task)
        {
            throw new System.NotImplementedException();
        }

        public void SetPriority(ITask task, int priority)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Adds a task at the end of the queue
        /// </summary>
        /// <param name="task">Task to add to queue</param>
        public void AddTask(ITask task)
        {
            TaskQueue.Add(task);
        }

        public void RemoveTask(int taskIndex)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveTask(ITask task)
        {
            throw new System.NotImplementedException();
        }

        public void CompleteTask(ITask task)
        {
            TaskQueue.Remove(task);
        }

        public bool InProgress()
        {
            return TaskQueue[0].InProgress;
        }

        /// <summary>
        /// Handles default task handler behavior
        /// </summary>
        /// <returns>Returns false if there are no tasks queued</returns>
        public bool Tick()
        {
            if (TaskQueue.Count == 0)  return false; 

            while (!InProgress() && TaskQueue.Count > 0)
            {
                ScheduleNextTask();
                if (TaskQueue.Count == 0) return false;
            }

            ITask task = TaskQueue[0];
            IAction action = task.Tick();
            if (action != null)
            {
                CurrentAction = action;
                action.Execute(Brain);
            }
            else
            {
                Brain.SetAnimationState(AnimationState.Idle);
                CompleteTask(task);
            }

            return true;
        }
    }
}
