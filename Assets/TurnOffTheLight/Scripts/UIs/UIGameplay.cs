using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TurnOffTheLight
{
    public class UIGameplay : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _homeBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void OnEnable()
        {
            GameManager.OnScoreUp += UpdateScoreUI;
        }

        private void OnDisable()
        {
            GameManager.OnScoreUp -= UpdateScoreUI;
        }


        private void Start()
        {
            UpdateScoreUI();
            
            _homeBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                Loader.Load(Loader.Scene.MenuScene);
            });

        }

        private void OnDestroy()
        {
            _homeBtn.onClick.RemoveAllListeners();
        }

        private void UpdateScoreUI()
        {
            _scoreText.text = $"{GameManager.Instance.Score}";
        }

    }
}
