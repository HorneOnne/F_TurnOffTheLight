using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnOffTheLight
{
    public class GridSystem : MonoBehaviour
    {
        public static GridSystem Instance { get; private set; }

        [Header("References")]
        [SerializeField] private Light _lightPrefab;

        [Header("Data")]
        [SerializeField] private LevelData _levelData;

        [Header("Properties")]
        [SerializeField] private float _cellSize = 0.2f;


        // Cached
        private Light[] _gridMap;
        public Transform Selection;

        private void Awake()
        {
            Instance = this;
        }



        private void Start()
        {
            LoadLevelData();
            CreateGrid();
        }


        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                int gridWidth = _levelData.Width; // Define your grid width
                int gridHeight = _levelData.Height; // Define your grid height

                int rowIndex = Mathf.FloorToInt(mouseWorldPos.y / _cellSize);
                int columnIndex = Mathf.FloorToInt(mouseWorldPos.x / _cellSize);

                int clampedRowIndex = Mathf.Clamp(rowIndex, 0, gridHeight - 2);
                int clampedColumnIndex = Mathf.Clamp(columnIndex, 0, gridWidth - 2);

                int index = clampedRowIndex * gridWidth + clampedColumnIndex;

                if (index >= 0 && index < _gridMap.Length)
                {
                    Vector3 topLeft = new Vector3(clampedColumnIndex * _cellSize, clampedRowIndex * _cellSize, 0);
                    Vector3 topRight = new Vector3((clampedColumnIndex + 1) * _cellSize, clampedRowIndex * _cellSize, 0);
                    Vector3 bottomLeft = new Vector3(clampedColumnIndex * _cellSize, (clampedRowIndex + 1) * _cellSize, 0);
                    Vector3 bottomRight = new Vector3((clampedColumnIndex + 1) * _cellSize, (clampedRowIndex + 1) * _cellSize, 0);

                    //Debug.DrawLine(topLeft, topRight, Color.red);
                    //Debug.DrawLine(topRight, bottomRight, Color.green);
                    //Debug.DrawLine(bottomRight, bottomLeft, Color.blue);
                    //Debug.DrawLine(bottomLeft, topLeft, Color.cyan);

                    //_gridMap[index].SetOnState();
                    //_gridMap[index + 1].SetOnState();
                    //_gridMap[index + gridWidth].SetOnState();
                    //_gridMap[index + 1 + gridWidth].SetOnState();

                    Selection.transform.position = topLeft;
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                //for(int i = 0;i < _gridMap.Length;i++)
                //{
                //    _gridMap[i].SetOffState();
                //}
            }
        }

        private void LoadLevelData()
        {
            // Load Levedata from GameManger.
            //this._levelData = GameManager.Instance.PlayingLevelData;

            var mainCam = Camera.main;
            mainCam.orthographicSize = _levelData.OrthographicCameraSize;
            Vector3 newPosition = new Vector3(mainCam.transform.position.x + _levelData.CameraOffset.x, mainCam.transform.position.y + _levelData.CameraOffset.y, mainCam.transform.position.z);
            mainCam.transform.position = newPosition;

            _gridMap = new Light[_levelData.Width * _levelData.Height];
        }

        private void CreateGrid()
        {
            int rows = _levelData.Width;
            int columns = _levelData.Height;

            // Calculate the center position of the nodes.
            Vector3 centerOffset = new Vector3((columns - 1) * _cellSize * 0.5f, -(rows - 1) * _cellSize * 0.5f, 0f);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Vector3 position = new Vector3(i * _cellSize, j * _cellSize, 0f);
                    _gridMap[i + rows * j] = Instantiate(_lightPrefab, position, Quaternion.identity, this.transform);
                }
            }
        }
    }
}
