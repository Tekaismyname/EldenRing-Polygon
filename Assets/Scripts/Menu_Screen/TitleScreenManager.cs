using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
namespace TK
{
    public class TitleScreenManager : MonoBehaviour
    {
        public static TitleScreenManager instance;
        [Header("Menus")]
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleScreenLoadMenu;

        [Header("Buttons")]
        [SerializeField] Button loadMenuReturnButton;
        [SerializeField] Button mainMenuLoadGameButton;
        [SerializeField] Button mainMenuNewGameButton;

        [Header("Pop Up")]
        [SerializeField] GameObject noCharacterSlotPopUp;
        [SerializeField] Button noCharacterSlotOkayButton;
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void StartNetworkAtHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame()
        {
            WorldSaveGameManager.intance.AttemptToCreateNewGame();
            
        }

        public void OpenLoadGameMenu()
        {
            // CLOSE MAIN MENU
            titleScreenMainMenu.SetActive(false);

            // OPEN LOAD MENU
            titleScreenLoadMenu.SetActive(true);

            // SELECT THE RETURN BUTTON FIRST
            loadMenuReturnButton.Select();
        }

        public void CloseLoadGameMenu()
        {
            // OPEN LOAD MENU
            titleScreenLoadMenu.SetActive(false);

            // CLOSE MAIN MENU
            titleScreenMainMenu.SetActive(true);

            // SELECT THE LOAD BUTTON
            mainMenuLoadGameButton.Select();
        }

        public void DisplayNoFreeCharacterSlotPopUp()
        {
            noCharacterSlotPopUp.SetActive(true);
            noCharacterSlotOkayButton.Select();
        }
        public void CloseNoFreeCharacterSlotPopUp()
        {
            noCharacterSlotPopUp.SetActive(false);
            mainMenuNewGameButton.Select();
        }
    }
}

