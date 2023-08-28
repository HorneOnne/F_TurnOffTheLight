using System.Runtime.CompilerServices;
using UnityEngine;

namespace TurnOffTheLight
{
    public class InputHanlder : MonoBehaviour
    {
        public static InputHanlder Instance { get; private set; }
        public static event System.Action OnMove;

        private float _cellSize = 1.4f;
        private int gridWidth;
        private int gridHeight;

        [Header("References")]
        [SerializeField] private Transform _selection;

        private float _updateTimer = 0.0f;

        // Cached
        private Camera _mainCam;
        private GridSystem _gridSystem;
        private Vector2 _mousePosition;

        private bool _isFirstClick = false;


        #region Properties
        public int MoveCount { get; private set; }
        #endregion

        private void Awake()
        {
            Instance = this;
        }


        private void OnEnable()
        {
            GridSystem.Instance.OnFinishShuffleGrid += DisplayGhotSelection;
            GameplayManager.OnWaiting += HideGhotSelection;

            UIGameplay.OnSwitchBtnClicked += ResetFirstClick;
            UIGameplay.OnSwitchBtnClicked += ResetMoveCount;
        }

        private void OnDisable()
        {
            GridSystem.Instance.OnFinishShuffleGrid -= DisplayGhotSelection;
            GameplayManager.OnWaiting -= HideGhotSelection;

            UIGameplay.OnSwitchBtnClicked -= ResetFirstClick;
            UIGameplay.OnSwitchBtnClicked -= ResetMoveCount;
        }


        private void Start()
        {
           

            _cellSize = GridSystem.Instance.CellSize;
            gridWidth = GridSystem.Instance.LevelData.Width; // Define your grid width
            gridHeight = GridSystem.Instance.LevelData.Height; // Define your grid height
            _mainCam = Camera.main;
            _gridSystem = GridSystem.Instance;
        }

        private void Update()
        {

            if (Time.time - _updateTimer > 0.1f)
            {     
                _updateTimer = Time.time;
                _mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
                int rowIndex = Mathf.FloorToInt(_mousePosition.y / _cellSize);
                int columnIndex = Mathf.FloorToInt(_mousePosition.x / _cellSize);
                int clampedRowIndex = Mathf.Clamp(rowIndex, 0, gridHeight - 2);
                int clampedColumnIndex = Mathf.Clamp(columnIndex, 0, gridWidth - 2);
                int index = clampedRowIndex * gridWidth + clampedColumnIndex;
                if (index >= 0 && index < _gridSystem.GridMap.Length)
                {
                    Vector3 topLeft = new Vector3(clampedColumnIndex * _cellSize, clampedRowIndex * _cellSize, 0);
                    _selection.transform.position = topLeft;
                }
            }


            if(Input.GetMouseButtonDown(0))
            {
                if(_isFirstClick == false && GridSystem.Instance.IsFinishShuffle)
                {
                    _isFirstClick = true;
                    GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.PLAYING);
                }


                if(GameplayManager.Instance.CurrentState == GameplayManager.GameState.PLAYING)
                {
                    MoveCount++;
                    SoundManager.Instance.PlaySound(SoundType.Hit, false);

                    _mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
                    int rowIndex = Mathf.FloorToInt(_mousePosition.y / _cellSize);
                    int columnIndex = Mathf.FloorToInt(_mousePosition.x / _cellSize);
                    int clampedRowIndex = Mathf.Clamp(rowIndex, 0, gridHeight - 2);
                    int clampedColumnIndex = Mathf.Clamp(columnIndex, 0, gridWidth - 2);
                    int index = clampedRowIndex * gridWidth + clampedColumnIndex;
                    if (index >= 0 && index < _gridSystem.GridMap.Length)
                    {
                        Light[] nbLights = _gridSystem.GetFourLights(index);
                        for (int i = 0; i < nbLights.Length; i++)
                        {
                            nbLights[i].ToggleLight();
                        }
                    }

                    OnMove?.Invoke();

                    bool canWin = GridSystem.Instance.CheckWinCondition();
                    if(canWin)
                    {
                        GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.WIN);
                    }
                }
               
            }
        }

        public void DisplayGhotSelection()
        {
            _selection.gameObject.SetActive(true);
        }

        public void HideGhotSelection()
        {
            _selection.gameObject.SetActive(false);
        }

        private void ResetFirstClick()
        {
            _isFirstClick = false;
        }

        private void ResetMoveCount()
        {
            MoveCount = 0;
        }
    }
}
