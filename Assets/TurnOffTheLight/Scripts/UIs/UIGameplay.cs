using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TurnOffTheLight
{
    public class UIGameplay : CustomCanvas
    {
        public static event System.Action OnSwitchBtnClicked;

        [Header("Buttons")]
        [SerializeField] private Button _backBtn;
        [SerializeField] private Button _switchBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _moveText;


        // Cached
        private float _updateTimerFrequence = 0.2f;
        private float _updateTimerFrequenceCount = 0.0f;

        private void OnEnable()
        {
            InputHanlder.OnMove += UpdateMoveCount;

        }

        private void OnDisable()
        {
            InputHanlder.OnMove -= UpdateMoveCount;
        }


        private void Start()
        {
            UpdateMoveCount();
            
            _backBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                Loader.Load(Loader.Scene.MenuScene);
            });

            _switchBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.WAITING);
                GridSystem.Instance.Stwitch();

                OnSwitchBtnClicked?.Invoke();

                UpdateMoveCount();
            });

        }

        private void OnDestroy()
        {
            _backBtn.onClick.RemoveAllListeners();
            _switchBtn.onClick.RemoveAllListeners();
        }

   

        private void Update()
        {
            if (Time.time - _updateTimerFrequenceCount > _updateTimerFrequence)
            {
                _updateTimerFrequenceCount = Time.time;
                UpdateTimeUI();
            }
        }

        private void UpdateTimeUI()
        {
            _timeText.text = TimerManager.Instance.TimeToText();
        }

        private void UpdateMoveCount()
        {
            _moveText.text = InputHanlder.Instance.MoveCount.ToString();
        }

    }
}
