using System.Collections.Generic;
using Awar.AI;
using Awar.Construction;
using Awar.Core;
using Awar.Map.Vegetation;
using Awar.Tasks;
using Awar.Tasks.TaskDefinitions;
using UnityEngine;

namespace Awar.Village
{
    public class VillageController : AwarBehavior
    {
        public static VillageController Get { get; private set; }

        public List<VillageBuilding> ActiveBuildings = new List<VillageBuilding>();
        public List<ConstructionObject> ConstructionBuildings = new List<ConstructionObject>();
        public List<VegetationObject> MarkedVegetation = new List<VegetationObject>();

        public override void Initialize()
        {
            Get = this;
        }

        public override void Tick()
        {
            throw new System.NotImplementedException();
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

            for (int i = 0; i < MarkedVegetation.Count; i++)
            {
                VegetationObject vegetationObject = MarkedVegetation[i];
                if(vegetationObject.Targetable())
                    return new WoodcuttingTask(brain, vegetationObject);
            }

            return null;
        }
    }
}
