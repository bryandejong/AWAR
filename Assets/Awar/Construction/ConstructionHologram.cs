using UnityEngine;

namespace Awar.Construction
{
    public class ConstructionHologram : MonoBehaviour
    {
        private static Color _hologramColorValid = new Color(1, 1, 1, .35f);
        private static Color _hologramColorInvalid = new Color(1, .2f, .2f, .35f);

        [SerializeField] private MeshRenderer[] _meshes = default;
        private HologramMode _mode = HologramMode.Valid;

        public void SetHologramMode(HologramMode mode)
        {
            if (mode == _mode) { return; }

            switch (mode)
            {
                case(HologramMode.Valid):
                    SetValid();
                    break;
                case(HologramMode.Invalid):
                    SetInvalid();
                    break;
                case(HologramMode.Disabled):
                    _mode = HologramMode.Disabled;
                    gameObject.SetActive(false);
                    break;
            }
        }

        private void SetInvalid()
        {

            _mode = HologramMode.Invalid;
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].material.color = _hologramColorInvalid;
            }
        }

        private void SetValid()
        {

            _mode = HologramMode.Valid;
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].material.color = _hologramColorValid;
            }
        }
    }

    public enum HologramMode
    {
        Valid, Invalid, Disabled
    }
}
