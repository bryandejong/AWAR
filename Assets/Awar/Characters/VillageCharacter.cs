using Awar.Village;

namespace Awar.Characters
{
    public class VillageCharacter : AICharacter
    {
        public JobPriorities JobPriorities;

        public new void Start()
        {
            base.Start();
            JobPriorities = new JobPriorities();
        }
    }
}
