using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace TK
{
    public class UI_Character_Save_Slot : MonoBehaviour
    {
        SaveFileDataWriter saveFileDataWriter;

        [Header("Game Slot")]
        public CharacterSlot characterSlot;

        [Header("Character Info")]
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI timePlayed;

        private void Awake()
        {
            characterName = GetComponentInChildren<TextMeshProUGUI>();

        }
        private void OnEnable()
        {
            LoadSaveSlots();
        }

        private void LoadSaveSlots()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectory = Application.persistentDataPath;

            // SAVE SLOT 01
            if(characterSlot == CharacterSlot.CharacterSlot_01)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFOMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {                  
                    characterName.text = WorldSaveGameManager.instance.characterSlot01.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 02
            else if (characterSlot == CharacterSlot.CharacterSlot_02)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFOMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot02.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 03
            else if (characterSlot == CharacterSlot.CharacterSlot_03)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFOMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot03.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 04
            else if (characterSlot == CharacterSlot.CharacterSlot_04)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFOMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot04.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 05
            else if (characterSlot == CharacterSlot.CharacterSlot_05)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFOMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot05.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 06
            else if (characterSlot == CharacterSlot.CharacterSlot_06)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFOMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot06.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 07
            else if (characterSlot == CharacterSlot.CharacterSlot_07)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFOMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot07.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 08
            else if (characterSlot == CharacterSlot.CharacterSlot_08)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFOMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot08.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 09
            else if (characterSlot == CharacterSlot.CharacterSlot_09)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFOMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot09.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 10
            else if (characterSlot == CharacterSlot.CharacterSlot_10)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFOMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot10.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public void LoadGameFromCharacterSlot()
        {
            WorldSaveGameManager.instance.currentCharacterSlotBeingUsed = characterSlot;
            WorldSaveGameManager.instance.LoadGame();
        }

        public void SelectCurrentSLot()
        {
            TitleScreenManager.instance.SelectCharacterSlot(characterSlot);
        }
    }
}

