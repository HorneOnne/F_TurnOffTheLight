using UnityEngine;

namespace TurnOffTheLight
{
    public class Light : MonoBehaviour
    {
        [SerializeField] private Color _onColor;
        [SerializeField] private Color _offColor;

        public enum LightState
        {
            On, Off
        }

        #region Properties
        [field: SerializeField] public LightState State { get; set; }
        #endregion


        private void Start()
        {
            
        }
    }
}
