using System.Collections;
using Awar.Characters;
using Awar.Construction;
using Awar.Tasks;
using Awar.Tasks.TaskDefinitions;
using UnityEngine;
using AnimationState = Awar.Characters.AnimationState;

namespace Awar.AI
{
    public class AIBrain : MonoBehaviour
    {
        [SerializeField] private bool _isAlive = true;
        [Range(0, 1)]
        [SerializeField] private float _updateInterval = .5f;

        [SerializeField] private AICharacter _aiCharacter = default;
        [SerializeField] private ConstructionObject _targetConstruction;
        
        private ITaskHandler _taskHandler;

        public void Start()
        {
            _taskHandler = new TaskHandler(this);
            _taskHandler.QueueTask(new ConstructionTask("Construct something", _targetConstruction));
            StartCoroutine(Coroutine_AiTick());
        }

        private IEnumerator Coroutine_AiTick()
        {
            while (_isAlive)
            {
                AiTick();
                yield return new WaitForSeconds(_updateInterval);
            }
            yield return null;
        }

        private void AiTick()
        {
            if (_taskHandler.InProgress())
            {
                _taskHandler.Tick();
            }
            else
            {
                _taskHandler.ScheduleNextTask();
            }
        }

        public void SetMovementTarget(Vector3 targetPos)
        {
            _aiCharacter.SetTarget(targetPos);
        }

        public void SetRotationTarget(Vector3 targetRotation)
        {
            transform.rotation = Quaternion.LookRotation(targetRotation, transform.up);
        }

        public void SetAnimationState(AnimationState state)
        {
            _aiCharacter.SetAnimation(state);
        }
    }
}
