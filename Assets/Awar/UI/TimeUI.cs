using UnityEngine;
using UnityEngine.UI;

namespace Awar.UI
{
    public class TimeUI : MonoBehaviour
    {
        [SerializeField] private Image[] _playButtons;
        [SerializeField] private Sprite _pauseImage;
        [SerializeField] private Sprite _playImageUnselected;
        [SerializeField] private Sprite _playImageSelected;
        public void UpdateUI(int timeScale)
        {
            for (int i = 0; i < _playButtons.Length; i++)
            {
                _playButtons[i].sprite = i < timeScale ? _playImageSelected : _playImageUnselected;
            }
        }
    }
}
