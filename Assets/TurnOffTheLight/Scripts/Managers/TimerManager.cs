﻿using UnityEngine;

namespace TurnOffTheLight
{
    public class TimerManager : MonoBehaviour
    {
        public static TimerManager Instance { get; private set; }

        private float timer = 0f;
        // Cached
        private GameplayManager gameplayManager;
        private bool _startTimer = false;

        #region Properties
        public float Time { get { return timer; } }
        #endregion


        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            GameplayManager.OnPlaying += () =>
            {
                _startTimer = true;
            };

            UIGameplay.OnSwitchBtnClicked += ResetTimer;
        }

        private void OnDisable()
        {
            GameplayManager.OnPlaying -= () =>
            {
                _startTimer = true;
            };

            UIGameplay.OnSwitchBtnClicked -= ResetTimer;
        }

        private void Start()
        {
            gameplayManager = GameplayManager.Instance;
        }

        private void Update()
        {

            if (gameplayManager.CurrentState == GameplayManager.GameState.PLAYING && _startTimer)
            {
                timer += UnityEngine.Time.deltaTime;
            }
        }

        public string TimeToText()
        {
            int minutes = Mathf.FloorToInt(timer);
            int seconds = Mathf.RoundToInt((timer - minutes) * 60);

            return $"{minutes:D1}.{seconds:D2}";
        }

        public string TimeToText(float value)
        {
            int minutes = Mathf.FloorToInt(value);
            int seconds = Mathf.RoundToInt((value - minutes) * 60);

            return $"{minutes:D1}.{seconds:D2}";
        }

        private void ResetTimer()
        {
            timer = 0.0f;
        }
    }
}
