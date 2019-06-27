using System.Collections.Generic;
using Awar.AI;
using UnityEngine;

namespace Awar.Construction
{
    public class ConstructionFrame : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] public float Progress = 0f;

        [SerializeField] private TargetPosition[] _constructionPoints = default;

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

            transform.localScale = transform.localScale + new Vector3(0, progress, 0);

            return false;
        }
        public void CompleteConstruction()
        {
            gameObject.SetActive(false);
        }

        public TargetPosition GetEmptyPosition()
        {
            for (int i = 0; i < _constructionPoints.Length; i++)
            {
                if (_constructionPoints[i].Occupant == null && _constructionPoints[i].TargetedBy == null)
                {
                    return _constructionPoints[i];
                }
            }

            return null;
        }
    }
}
