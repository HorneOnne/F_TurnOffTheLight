using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TurnOffTheLight
{
    public class LevelBtn : CustomCanvas
    {
        public static event System.Action OnLevelBtnClicked;

        [Header("Buttons")]
        [SerializeField] private Button _levelBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _levelBtnText;

        [Header("Others")]
        [SerializeField] private Sprite _selectSprite;
        [SerializeField] private Sprite _unSelectSprite;
        [SerializeField] private Color _selectTextColor;
        [SerializeField] private Color _unSelectTextColor;


        [Header("Data")]
        [SerializeField] private LevelData _levelData;

        private void OnEnable()
        {
            OnLevelBtnClicked += UpdateVisual;
        }

        private void OnDisable()
        {
            OnLevelBtnClicked -= UpdateVisual;
        }

        private void Start()
        {
            UpdateVisual();

            _levelBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                GameManager.Instance.PlayingLevelData = _levelData;
                

                OnLevelBtnClicked?.Invoke();
            });
        }

        private void OnDestroy()
        {
            _levelBtn.onClick.RemoveAllListeners();
        }

        private void UpdateVisual()
        {
            if (GameManager.Instance.PlayingLevelData == _levelData)
            {
                _levelBtn.image.sprite = _selectSprite;
                _levelBtnText.color = _selectTextColor;
            }
            else
            {
                _levelBtn.image.sprite = _unSelectSprite;
                _levelBtnText.color = _unSelectTextColor;
            }
        }
    }
}
