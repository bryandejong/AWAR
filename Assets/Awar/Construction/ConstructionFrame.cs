using JetBrains.Annotations;
using UnityEngine;

namespace Awar.Construction
{
    public class ConstructionFrame : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] public float Progress = 0f;

        public void Initialize()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Adds progress to the building
        /// </summary>
        /// <param name="progress">Progress to add to the current progress. Ranges from 0 to 1f</param>
        /// <returns></returns>
        public bool AddProgress(float progress)
        {
            Progress += progress;

            if (Progress >= 1)
            {
                CompleteConstruction();
                return true;
            }

            return false;
        }
        public void CompleteConstruction()
        {
            gameObject.SetActive(false);
        }
    }
}
