using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TurnOffTheLight
{
    public class UIMainMenu : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _playBtn;
        [SerializeField] private Button _levelBtn;
        [SerializeField] private Button _optionsBtn;


        private void Start()
        {
            _playBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.GameplayScene);
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _levelBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayLevelMenu(true);
            });

            _optionsBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayOptionsMenu(true);         
            });
        }

        private void OnDestroy()
        {
            _playBtn.onClick.RemoveAllListeners();
            _levelBtn.onClick.RemoveAllListeners();
            _optionsBtn.onClick.RemoveAllListeners();
        }
    }
}
