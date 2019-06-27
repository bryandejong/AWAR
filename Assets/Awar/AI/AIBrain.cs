using System.Collections;
using Awar.Characters;
using Awar.Construction;
using Awar.Tasks;
using Awar.Tasks.TaskDefinitions;
using Awar.Village;
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
        
        private ITaskHandler _taskHandler;

        public void Start()
        {
            _taskHandler = new TaskHandler(this);
            StartCoroutine(Coroutine_AiTick());
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

        private void AiTick()
        {
            if (!_taskHandler.Tick())
            {
                ITask newTask = VillageController.Get.GetTask(this);
                if (newTask != null)
                {
                    _taskHandler.AddTask(newTask);
                }
            }
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
    }
}
