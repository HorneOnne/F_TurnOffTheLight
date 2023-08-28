using UnityEngine;
using UnityEngine.UI;

namespace TurnOffTheLight
{
    public class UILevel : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _backBtn;
        [SerializeField] private LevelBtn _5x5Btn;
        [SerializeField] private LevelBtn _6x6Btn;
        [SerializeField] private LevelBtn _7x7Btn;

        private void Start()
        {
            _backBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);

                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            _backBtn.onClick.RemoveAllListeners();
        }
    }
}
