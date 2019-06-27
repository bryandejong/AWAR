using System.Collections.Generic;
using Awar.AI;
using Awar.Construction;
using Awar.Tasks;
using Awar.Tasks.TaskDefinitions;
using UnityEngine;

namespace Awar.Village
{
    public class VillageController : MonoBehaviour
    {
        public static VillageController Get { get; private set; }

        public List<VillageBuilding> ActiveBuildings = new List<VillageBuilding>();
        public List<ConstructionObject> ConstructionBuildings = new List<ConstructionObject>();

        private void Awake()
        {
            Get = this;
        }

        public ITask GetTask(AIBrain brain)
        {
            for (int i = 0; i < ConstructionBuildings.Count; i++)
            {
                ConstructionObject constructionObject = ConstructionBuildings[i];
                TargetPosition emptyPosition = ConstructionBuildings[i].GetEmptyPosition();
                if (emptyPosition != null)
                {
                    return new ConstructionTask(brain, constructionObject);
                }
            }
            return null;
        }
    }
}
