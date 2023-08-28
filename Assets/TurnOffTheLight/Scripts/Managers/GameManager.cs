using System.Collections.Generic;
using UnityEngine;

namespace TurnOffTheLight
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static event System.Action OnScoreUp;

        // SCORE & BEST
        private int _bestMove = int.MaxValue;
        private float _bestTime = float.MaxValue;


        public List<LevelData> LevelData;
        public LevelData PlayingLevelData;

        #region Properties
        public int BestMove { get => _bestMove; }
        public float BestTime { get => _bestTime; }

        #endregion
        private void Awake()
        {
            // Check if an instance already exists, and destroy the duplicate
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            // FPS
            Application.targetFrameRate = 60;

            PlayingLevelData = LevelData[0];
        }

        private void Start()
        {
            // Make the GameObject persist across scenes
            DontDestroyOnLoad(this.gameObject);
            
        }


        public void SetBestMove(int move)
        {
            if (move < _bestMove)
            {
                _bestMove = move;
            }
        }

        public void SetBestTime(float time)
        {
            if (time < _bestTime)
            {
                _bestTime = time;
            }
        }
    }
}
