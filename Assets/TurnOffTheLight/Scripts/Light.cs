using UnityEngine;

namespace TurnOffTheLight
{
    public class Light : MonoBehaviour
    {
        [SerializeField] private Color _onColor;
        [SerializeField] private Color _offColor;

        private SpriteRenderer _sr;

        public enum LightState
        {
            On, Off
        }

        #region Properties
        [field: SerializeField] public LightState State { get; set; }
        #endregion


        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            
        }

        public void SetOnState()
        {
            _sr.color = _onColor;
        }

        public void SetOffState()
        {
            _sr.color = _offColor;
        }
    }
}
