using UnityEngine;

namespace Awar.Utils
{
    public class GameObjectToggler : MonoBehaviour
    {
        public void Enable(GameObject target)
        {
            target.SetActive(true);
        }

        public void Disable(GameObject target)
        {
            target.SetActive(false);
        }
    }
}
