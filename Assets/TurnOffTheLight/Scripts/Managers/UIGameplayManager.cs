using UnityEngine;

namespace TurnOffTheLight
{
    public class UIGameplayManager : MonoBehaviour
    {
        public static UIGameplayManager Instance { get; private set; }

        public UIGameplay UIGameplay;
        public UIWin UIWin;


        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            CloseAll();
            DisplayGameplayMenu(true);
        }

        public void CloseAll()
        {
            DisplayGameplayMenu(false);
            DisplayWinMenu(false);
        }


        public void DisplayGameplayMenu(bool isActive)
        {
            UIGameplay.DisplayCanvas(isActive);
        }

        public void DisplayWinMenu(bool isActive)
        {
            UIWin.DisplayCanvas(isActive);
        }

    }
}
