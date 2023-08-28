using System.Runtime.CompilerServices;
using UnityEngine;

namespace TurnOffTheLight
{
    public class InputHanlder : MonoBehaviour
    {
        public static InputHanlder Instance { get; private set; }
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



        private void Awake()
        {
            Instance = this;
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
                _mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
                int rowIndex = Mathf.FloorToInt(_mousePosition.y / _cellSize);
                int columnIndex = Mathf.FloorToInt(_mousePosition.x / _cellSize);
                int clampedRowIndex = Mathf.Clamp(rowIndex, 0, gridHeight - 2);
                int clampedColumnIndex = Mathf.Clamp(columnIndex, 0, gridWidth - 2);
                int index = clampedRowIndex * gridWidth + clampedColumnIndex;
                if (index >= 0 && index < _gridSystem.GridMap.Length)
                {
                    Light[] nbLights = _gridSystem.GetFourLights(index);
                    for(int i = 0;i < nbLights.Length;i++)
                    {
                        nbLights[i].ToggleLight();
                    }
                }
            }

        }
    }
}
