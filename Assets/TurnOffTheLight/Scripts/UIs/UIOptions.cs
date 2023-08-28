using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TurnOffTheLight
{
    public class UIOptions : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _backBtn;


        [Header("Sliders")]
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;


        private void Start()
        {
            _soundSlider.value = SoundManager.Instance.SFXVolume;
            _musicSlider.value = SoundManager.Instance.BackgroundVolume;

            _backBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);
            });

           

            _soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
            _musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        }

        private void OnDestroy()
        {
            _backBtn.onClick.RemoveAllListeners();

            _soundSlider.onValueChanged.RemoveAllListeners();
            _musicSlider.onValueChanged.RemoveAllListeners();
        }

        private void OnSoundSliderChanged(float value)
        {
            SoundManager.Instance.SFXVolume = value;
        }

        private void OnMusicSliderChanged(float value)
        {
            SoundManager.Instance.BackgroundVolume = value;
            SoundManager.Instance.UpdateBackgroundVolume();
        }
    }
}
