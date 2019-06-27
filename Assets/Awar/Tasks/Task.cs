using UnityEngine;

namespace Awar.Tasks
{
    public class Task : ITask
    {
        public string Name { get; set; }
        public bool  InProgress { get; set; }

        public Task(string name)
        {
            Name = name;
        }
        
        public void Execute()
        {
            InProgress = true;
        }

        public void Tick()
        {
            Debug.Log(Name + " has ticked");
        }
    }
}
