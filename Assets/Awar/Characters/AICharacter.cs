using Awar.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Awar.Characters
{
    public class AICharacter : Character
    {
        [SerializeField] private NavMeshAgent _agent = default;

        public void Start()
        {
            _agent.updateRotation = true;
        }

        //Test code, remove when Awar's Tick() is implemented
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = MouseControl.MouseRay();

                if (Physics.Raycast(ray, out var hit, 150))
                {
                    Debug.Log(hit.transform.gameObject);
                    SetTarget(hit.point);
                }
            }

            if (IsMoving)
            {
                if ((_agent.remainingDistance < _agent.stoppingDistance) && !_agent.pathPending)
                {
                    _agent.ResetPath();
                    StopMoving();
                }
            }
        }

        private void SetTarget(Vector3 targetPos)
        {
            if(_agent.SetDestination(targetPos))
            {
                Move(_agent.steeringTarget);
            };
        }
    }
}
