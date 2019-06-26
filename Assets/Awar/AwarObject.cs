using UnityEngine;

namespace Awar
{
    public abstract class AwarObject : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Tick();
    }
}
