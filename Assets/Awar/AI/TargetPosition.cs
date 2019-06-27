using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Awar.AI
{
    public class TargetPosition : MonoBehaviour
    {
        public GameObjectEvent EnterEvent;
        public GameObjectEvent ExitEvent;
        public List<AIBrain> ObjectsInPosition = new List<AIBrain>();
        public AIBrain Occupant = null;
        public AIBrain TargetedBy = null;

        private void OnTriggerEnter(Collider col)
        {
            EnterEvent.Invoke(col.gameObject);
            AIBrain brain = col.gameObject.GetComponent<AIBrain>();
            ObjectsInPosition.Add(brain);
            if (brain == TargetedBy)
            {
                Occupant = brain;
                TargetedBy = null;
            }
        }

        private void OnTriggerExit(Collider col)
        {
            ExitEvent.Invoke(col.gameObject);
            AIBrain brain = col.gameObject.GetComponent<AIBrain>();
            ObjectsInPosition.Remove(brain);

            if(Occupant == brain)
            {
                Occupant = null;
            }
        }
    }

    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject>
    {
    }
}
