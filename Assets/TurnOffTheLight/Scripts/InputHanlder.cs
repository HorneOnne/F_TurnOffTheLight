using UnityEngine;

namespace TurnOffTheLight
{
    public class InputHanlder : MonoBehaviour
    {
        public static InputHanlder Instance { get; private set; }
        public float cellSize = 1.4f;

        private void Awake()
        {
            Instance = this;
        }

       
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int rowIndex = Mathf.FloorToInt(mouseWorldPos.y / cellSize);
                int columnIndex = Mathf.FloorToInt(mouseWorldPos.x / cellSize);

                Vector3 topLeft = new Vector3(columnIndex * cellSize, rowIndex * cellSize, 0);
                Vector3 topRight = new Vector3((columnIndex + 1) * cellSize, rowIndex * cellSize, 0);
                Vector3 bottomLeft = new Vector3(columnIndex * cellSize, (rowIndex + 1) * cellSize, 0);
                Vector3 bottomRight = new Vector3((columnIndex + 1) * cellSize, (rowIndex + 1) * cellSize, 0);

                Debug.DrawLine(topLeft, topRight, Color.red);
                Debug.DrawLine(topRight, bottomRight, Color.red);
                Debug.DrawLine(bottomRight, bottomLeft, Color.red);
                Debug.DrawLine(bottomLeft, topLeft, Color.red);
            }
        }

    }
}
