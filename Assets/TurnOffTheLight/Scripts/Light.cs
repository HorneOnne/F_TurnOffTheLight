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
        [field: SerializeField] public LightState State { get; private set; } = LightState.On;
        #endregion


        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            UpdateLightVisual();
        }


        public void ToggleLight()
        {
            if(State == LightState.On)
            {
                SetOffState();
            }
            else
            {
                SetOnState();
            }
        }

        public void SetOnState()
        {
            State = LightState.On;
            _sr.color = _onColor;
        }

        public void SetOffState()
        {
            State = LightState.Off;
            _sr.color = _offColor;
        }


        private void UpdateLightVisual()
        {
            if (State == LightState.On)
            {
                _sr.color = _onColor;
            }
            else
            {
                _sr.color = _offColor;
            }
        }
    }
}
