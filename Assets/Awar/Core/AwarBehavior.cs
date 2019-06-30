using UnityEngine;

namespace Awar.Core
{
    public abstract class AwarBehavior : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Tick();
    }
}
