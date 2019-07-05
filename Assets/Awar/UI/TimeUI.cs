using UnityEngine;
using UnityEngine.UI;

namespace Awar.UI
{
    public class TimeUI : MonoBehaviour
    {
        [SerializeField] private Image[] _playButtons = default;
        [SerializeField] private Sprite _playImageUnselected = default;
        [SerializeField] private Sprite _playImageSelected = default;
        public void UpdateUI(int timeScale)
        {
            for (int i = 0; i < _playButtons.Length; i++)
            {
                _playButtons[i].sprite = i < timeScale ? _playImageSelected : _playImageUnselected;
            }
        }
    }
}
