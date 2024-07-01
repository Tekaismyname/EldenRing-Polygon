using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace TK
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;
        public PlayerManager player;

        [Header("Save/Load")]
        [SerializeField] bool saveGame;
        [SerializeField] bool loadGame;

        [Header("World Scene Index")]
        [SerializeField] int worldIndexNumber = 1;

        [Header("Save Data Writer")]
        private SaveFileDataWriter saveFileDataWriter;

        [Header("Current Character Data")]
        public CharacterSlot currentCharacterSlotBeingUsed;
        public CharacterSaveData currentCharacterData;
        private string saveFileName;
        [Header("Character Slots")]
        public CharacterSaveData characterSlot01;
        public CharacterSaveData characterSlot02;
        public CharacterSaveData characterSlot03;
        public CharacterSaveData characterSlot04;
        public CharacterSaveData characterSlot05;
        public CharacterSaveData characterSlot06;
        public CharacterSaveData characterSlot07;
        public CharacterSaveData characterSlot08;
        public CharacterSaveData characterSlot09;
        public CharacterSaveData characterSlot10; 
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

        private void Start()
        {
            LoadAllCharacterProfiles();
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
        public string DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot characterSlot)
        {
            string fileName = "";
            switch(characterSlot)
            {
                case CharacterSlot.CharacterSlot_01:
                    fileName = "CharacterSlot_01";
                    break;
                case CharacterSlot.CharacterSlot_02:
                    fileName = "CharacterSlot_02";
                    break;
                case CharacterSlot.CharacterSlot_03:
                    fileName = "CharacterSlot_03";
                    break;
                case CharacterSlot.CharacterSlot_04:
                    fileName = "CharacterSlot_04";
                    break;
                case CharacterSlot.CharacterSlot_05:
                    fileName = "CharacterSlot_05";
                    break;
                case CharacterSlot.CharacterSlot_06:
                    fileName = "CharacterSlot_06";
                    break;
                case CharacterSlot.CharacterSlot_07:
                    fileName = "CharacterSlot_07";
                    break;
                case CharacterSlot.CharacterSlot_08:
                    fileName = "CharacterSlot_08";
                    break;
                case CharacterSlot.CharacterSlot_09:
                    fileName = "CharacterSlot_09";
                    break;
                case CharacterSlot.CharacterSlot_10:
                    fileName = "CharacterSlot_10";
                    break;
                default: break;
            }
            return fileName;
        }

        public void AttemptToCreateNewGame()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectory = Application.persistentDataPath;
            // CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISITING FILES FIRST)
            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);
            

            if (!saveFileDataWriter.CheckToSeeIfFileExists())
            {
                // IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_01;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }


            // CREATE A NEW FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);


            if (!saveFileDataWriter.CheckToSeeIfFileExists())
            {
                // IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_02;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            // CREATE A NEW FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_03);


            if (!saveFileDataWriter.CheckToSeeIfFileExists())
            {
                // IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_03;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            // CREATE A NEW FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_04);


            if (!saveFileDataWriter.CheckToSeeIfFileExists())
            {
                // IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_04;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            // CREATE A NEW FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_05);


            if (!saveFileDataWriter.CheckToSeeIfFileExists())
            {
                // IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_05;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }


            // CREATE A NEW FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_06);


            if (!saveFileDataWriter.CheckToSeeIfFileExists())
            {
                // IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_06;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            // CREATE A NEW FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_07);


            if (!saveFileDataWriter.CheckToSeeIfFileExists())
            {
                // IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_07;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            // CREATE A NEW FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_08);


            if (!saveFileDataWriter.CheckToSeeIfFileExists())
            {
                // IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_08;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            // CREATE A NEW FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_09);


            if (!saveFileDataWriter.CheckToSeeIfFileExists())
            {
                // IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_09;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            // CREATE A NEW FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_10);


            if (!saveFileDataWriter.CheckToSeeIfFileExists())
            {
                // IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_10;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }
            // IF THERE ARE NO FREE SLOTS, NOTIFY THE PLAYER
            TitleScreenManager.instance.DisplayNoFreeCharacterSlotPopUp();
        }

        private void NewGame()
        {
            // SAVE THE NEWLY CREATED CHARACTERS STATS, AND ITEMS(WHEN CREATION SCREEN IS ADDED)
            player.playerNetworkManager.vitality.Value = 10;
            player.playerNetworkManager.endurance.Value = 10;

            SaveGame();
            StartCoroutine(LoadWorldScene());
        }
        public void LoadGame()
        {
            // LOAD A PREVIOUS FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);
            saveFileDataWriter = new SaveFileDataWriter();
            // GENERALLY WORKS ON MULTIPLE MACHINE TYPES(Application.persistentDataPath)
            saveFileDataWriter.saveDataDirectory = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;
            currentCharacterData = saveFileDataWriter.LoadSaveFile();

            StartCoroutine(LoadWorldScene());
            
        }

        public void SaveGame()
        {
            // SAVE THE CURRENT FILE UNDER A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

            saveFileDataWriter = new SaveFileDataWriter();
            // GENERALLT WORKS ON MULTIPLE MACHINE TYPES (Application.persistentDataPath)
            saveFileDataWriter.saveDataDirectory = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;
           
            // PASS THE PLAYERS INFO, FROM GAME, TO THEIR SAVE FILE
            player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);
            
            // WRITE THAT INFO ONTO A JSON FILE, SAVED TO THIS MACHINE
            saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
        }

        public void DeleteGame(CharacterSlot characterSlot)
        {
            // CHOOSE FILE BASED ON NAME
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectory = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(characterSlot);
           
            saveFileDataWriter.DeleteSaveFile();
        }

        // LOAD ALL CHARACTER PROFILES ON DEVICE WHEN STARTING GAME
        private void LoadAllCharacterProfiles()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectory = Application.persistentDataPath;

            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);
            characterSlot01 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);
            characterSlot01 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_03);
            characterSlot01 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_04);
            characterSlot01 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_05);
            characterSlot01 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_06);
            characterSlot01 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_07);
            characterSlot01 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_08);
            characterSlot01 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_09);
            characterSlot01 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFleNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_10);
            characterSlot01 = saveFileDataWriter.LoadSaveFile();
        }
        public IEnumerator LoadWorldScene()
        {
            // IF YOU JUST WANT 1 WORLD SCENE USE THIS
            AsyncOperation loadOperator = SceneManager.LoadSceneAsync(worldIndexNumber);

            // IF YOU WANT TO USE DIFFERENT SCENES FOR LEVELS IN YOUR PROJECT USE THIS
            //AsyncOperation loadOperator = SceneManager.LoadSceneAsync(currentCharacterData.sceneIndex);
            player.LoadGameDataFromCurrentCharacterData(ref currentCharacterData);
            yield return null;
        }
        // IF YOU WANT TO USE A MULTI SCENE SETUP, THERE IS NO CURRENT SCENE INDEX ON A NEW CHARACTER
        /*private IEnumerator LoadWorldSceneNewGame()
        {

        }*/

        public int getWorldIndex()
        {
            return worldIndexNumber;
        }
    }

}

