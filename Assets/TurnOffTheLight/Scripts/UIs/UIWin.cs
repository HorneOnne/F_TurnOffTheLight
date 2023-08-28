using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TurnOffTheLight
{
    public class UIWin : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _backBtn;
        [SerializeField] private Button _replayBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _moveText;
        [SerializeField] private TextMeshProUGUI _recordTimeText;
        [SerializeField] private TextMeshProUGUI _recordMoveText;
        

        private void OnEnable()
        {
            GameplayManager.OnWin += LoadTexts;
        }

        private void OnDisable()
        {
            GameplayManager.OnWin -= LoadTexts;
        }


        private void Start()
        {
            LoadTexts();

            _backBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                Loader.Load(Loader.Scene.MenuScene);
            });

            _replayBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                Loader.Load(Loader.Scene.GameplayScene);
            });
        }

        private void OnDestroy()
        {
            _backBtn.onClick.RemoveAllListeners();
            _replayBtn.onClick.RemoveAllListeners();
        }

        private void LoadTexts()
        {
            _timeText.text = $"{TimerManager.Instance.TimeToText()}";
            _moveText.text = $"{InputHanlder.Instance.MoveCount}";

            _recordMoveText.text = $"RECORD {GameManager.Instance.BestMove}";
            _recordTimeText.text = $"RECORD {TimerManager.Instance.TimeToText(GameManager.Instance.BestTime)}";
        }
    }
}
