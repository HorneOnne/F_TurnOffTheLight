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


        public float CellSize { get; private set; } = 1.4f;

        // Cached
        public Light[] GridMap { get; private set; }
  

        #region Properties
        public LevelData LevelData { get => _levelData; }

        #endregion
        private void Awake()
        {
            Instance = this;
        }



        private void Start()
        {
            LoadLevelData();
            CreateGrid();
            StartCoroutine(PerformShuffleGrid(Random.Range(LevelData.MinShuffle, LevelData.MaxShuffle), 0.1f, ()=>
            {
                
            }));  
        }
 

        public Light[] GetFourLights(int fromIndex)
        {
            int gridWidth = _levelData.Width;
            Light[] lights = new Light[4];
            lights[0] = GridMap[fromIndex];
            lights[1] = GridMap[fromIndex + 1];
            lights[2] = GridMap[fromIndex + gridWidth];
            lights[3] = GridMap[fromIndex + 1 + gridWidth];

            return lights;
        }

        private void LoadLevelData()
        {
            // Load Levedata from GameManger.
            //this._levelData = GameManager.Instance.PlayingLevelData;

            var mainCam = Camera.main;
            mainCam.orthographicSize = _levelData.OrthographicCameraSize;
            Vector3 newPosition = new Vector3(mainCam.transform.position.x + _levelData.CameraOffset.x, mainCam.transform.position.y + _levelData.CameraOffset.y, mainCam.transform.position.z);
            mainCam.transform.position = newPosition;

            GridMap = new Light[_levelData.Width * _levelData.Height];
        }

        private void CreateGrid()
        {
            int rows = _levelData.Width;
            int columns = _levelData.Height;

            // Calculate the center position of the nodes.
            Vector3 centerOffset = new Vector3((columns - 1) * CellSize * 0.5f, -(rows - 1) * CellSize * 0.5f, 0f);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Vector3 position = new Vector3(i * CellSize, j * CellSize, 0f);
                    GridMap[i + rows * j] = Instantiate(_lightPrefab, position, Quaternion.identity, this.transform);
                }
            }
        }


        private IEnumerator PerformShuffleGrid(int times, float timeEachShuffle, System.Action OnFhuffleFinished)
        {
            for(int i = 0;i < times; i++)
            {
                int index = GetRandomIndex(_levelData.Width, _levelData.Height);
                Light[] lightsNB = GetFourLights(index);

                foreach(var light in lightsNB)
                {
                    light.ToggleLight();
                }

                yield return new WaitForSeconds(timeEachShuffle);
            }

            OnFhuffleFinished?.Invoke();
        }


        private int GetRandomIndex(int width, int height)
        {
            int maxWidth = width - 1;
            int maxHeight = height - 1;

            int randomWidth = Random.Range(0, maxWidth);
            int randomHeight = Random.Range(0, maxHeight);
            int index = randomWidth + width * randomHeight;
            return index;
        }


        public bool CheckWinCondition()
        {
            Light.LightState firstLightState = GridMap[0].State;

            for(int i = 1; i < GridMap.Length; i++)
            {
                if (GridMap[i].State != firstLightState)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
