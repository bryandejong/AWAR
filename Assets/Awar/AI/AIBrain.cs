using System.Collections;
using Awar.Characters;
using Awar.Tasks;
using UnityEngine;

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
            _taskHandler = new TaskHandler();
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
                _taskHandler.ExecuteNextTask();
            }
        }
    }
}
