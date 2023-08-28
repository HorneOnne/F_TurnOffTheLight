using UnityEngine;

namespace TurnOffTheLight
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public UIMainMenu UIMainMenu;
        public UILevel UILevel;
        public UIOptions UIOptions;


 
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CloseAll();
            DisplayMainMenu(true);
        }

        public void CloseAll()
        {
            DisplayMainMenu(false);
            DisplayOptionsMenu(false);
            DisplayLevelMenu(false);
        }

        public void DisplayMainMenu(bool isActive)
        {
            UIMainMenu.DisplayCanvas(isActive);
        }


        public void DisplayLevelMenu(bool isActive)
        {
            UILevel.DisplayCanvas(isActive);
        }


        public void DisplayOptionsMenu(bool isActive)
        {
            UIOptions.DisplayCanvas(isActive);
        }



    }
}
