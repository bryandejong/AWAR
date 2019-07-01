using System;
using System.Collections.Generic;

namespace Awar.Village
{
    public class JobPriorities
    {
        public Job[] Jobs;

        public JobPriorities()
        {
            Jobs = new Job[Enum.GetNames(typeof(JobType)).Length];
            for (int i = 0; i < Jobs.Length; i++)
            {
                Jobs[i] = new Job((JobType)Enum.Parse(typeof(JobType), Enum.GetNames(typeof(JobType))[i]), 0);
            }
        }
    }

    public class Job
    {
        public JobType Type;
        public int Priority;

        public Job(JobType type, int priority)
        {
            Priority = priority;
            Type = type;
        }
    }

    public enum JobType
    {
        Woodcutting,
        Mining,
        Farming,
        Construction
    }
}
