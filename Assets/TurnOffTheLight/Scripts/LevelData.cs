using UnityEngine;

namespace TurnOffTheLight
{
    [CreateAssetMenu(fileName = "LevelData_", menuName = "TurnOffTheLight/LevelData", order = 51)]
    public class LevelData : ScriptableObject
    {
        [Header("Level")]
        public int Level;
        public bool IsLocking;

        [Header("Camera zoom")]
        public float OrthographicCameraSize = 5;
        public Vector2 CameraOffset;

        [Header("Grid size")]
        public int Width;
        public int Height;
    }
}
