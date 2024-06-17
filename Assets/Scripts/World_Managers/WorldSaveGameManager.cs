using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace TK
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager intance;
        [SerializeField] PlayerManager player;

        [Header("Save/Load")]
        [SerializeField] bool saveGame;
        [SerializeField] bool loadGame;

        [Header("World Scene Index")]
        [SerializeField] int worldIndexNumber = 1;

        [Header("Save Data Writer")]
        private SaveFileDataWriter saveFileDataWrite;

        [Header("Current Character Data")]
        public CharacterSlot currentCharacterSlotBeingUsed;
        public CharacterSaveData currentCharacterData;
        private string saveFileName;
        [Header("Character Slots")]
        public CharacterSaveData characterSlot01;
       /* public CharacterSaveData characterSlot02;
        public CharacterSaveData characterSlot03;
        public CharacterSaveData characterSlot04;
        public CharacterSaveData characterSlot05;
        public CharacterSaveData characterSlot06;
        public CharacterSaveData characterSlot07;
        public CharacterSaveData characterSlot08;
        public CharacterSaveData characterSlot09;
        public CharacterSaveData characterSlot10; */
        private void Awake()
        {
            if(intance == null)
            {
                intance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        private void Update()
        {
            if(saveGame)
            {
                saveGame = false;
                SaveGame();
            }

            if(loadGame)
            {
                loadGame = false;
                LoadGame();
            }
        }
        private void DecideCharacterFleNameBasedOnCharacterSlotBeingUsed()
        {
            switch(currentCharacterSlotBeingUsed)
            {
                case CharacterSlot.CharacterSlot_01:
                    saveFileName = "CharacterSlot_01";
                    break;
                case CharacterSlot.CharacterSlot_02:
                    saveFileName = "CharacterSlot_02";
                    break;
                case CharacterSlot.CharacterSlot_03:
                    saveFileName = "CharacterSlot_03";
                    break;
                case CharacterSlot.CharacterSlot_04:
                    saveFileName = "CharacterSlot_04";
                    break;
                case CharacterSlot.CharacterSlot_05:
                    saveFileName = "CharacterSlot_05";
                    break;
                case CharacterSlot.CharacterSlot_06:
                    saveFileName = "CharacterSlot_06";
                    break;
                case CharacterSlot.CharacterSlot_07:
                    saveFileName = "CharacterSlot_07";
                    break;
                case CharacterSlot.CharacterSlot_08:
                    saveFileName = "CharacterSlot_08";
                    break;
                case CharacterSlot.CharacterSlot_09:
                    saveFileName = "CharacterSlot_09";
                    break;
                case CharacterSlot.CharacterSlot_010:
                    saveFileName = "CharacterSlot_10";
                    break;
                default: break;
            }
        }

        public void CreateNewGame()
        {
            // CREATE A NEW FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            DecideCharacterFleNameBasedOnCharacterSlotBeingUsed();

            currentCharacterData = new CharacterSaveData();
        }

        public void LoadGame()
        {
            // LOAD A PREVIOUS FILE , WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            DecideCharacterFleNameBasedOnCharacterSlotBeingUsed();
            saveFileDataWrite = new SaveFileDataWriter();
            // GENERALLT WORKS ON MULTIPLE MACHINE TYPES (Application.persistentDataPath)
            saveFileDataWrite.saveDataDirectory = Application.persistentDataPath;
            saveFileDataWrite.saveFileName = saveFileName;
            currentCharacterData = saveFileDataWrite.LoadSaveFile();

            StartCoroutine(LoadWorldScene());
        }

        public void SaveGame()
        {
            // SAVE THE CURRENT FILE UNDER A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            DecideCharacterFleNameBasedOnCharacterSlotBeingUsed();

            saveFileDataWrite = new SaveFileDataWriter();
            // GENERALLT WORKS ON MULTIPLE MACHINE TYPES (Application.persistentDataPath)
            saveFileDataWrite.saveDataDirectory = Application.persistentDataPath;
            saveFileDataWrite.saveFileName = saveFileName;
            Debug.Log("Testing");
            // PASS THE PLAYERS INFO, FROM GAME, TO THEIR SAVE FILE
            player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);
            
            // WRITE THAT INFO ONTO A JSON FILE, SAVED TO THIS MACHINE
            saveFileDataWrite.CreateNewCharacterSaveFile(currentCharacterData);
        }
        public IEnumerator LoadWorldScene()
        {
            AsyncOperation loadOperator = SceneManager.LoadSceneAsync(worldIndexNumber);

            yield return null;
        }

        public int getWorldIndex()
        {
            return worldIndexNumber;
        }
    }

}

