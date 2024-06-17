using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
namespace TK
{
    public class SaveFileDataWriter
    {
        public string saveDataDirectory = "";
        public string saveFileName = "";

        // BEFORE WE CREATE A NEW SAVE FILE, WE MUST CHECK TO SEE IF ONE OF THIS CHARACTER SLOT ALREADY EXISTS (MAX 10 CHARACTER SLOTS)
        public bool CheckToSeeIfFileExists()
        {
            if (File.Exists(Path.Combine(saveDataDirectory, saveFileName)))
            {
                return true;
            }
            else
                return false;
        }
        // USED TO DELETE CHARACTER SAVE FILE 
        public void DeleteSaveFile()
        {
            File.Delete(Path.Combine(saveDataDirectory, saveFileName));
        }
        // USED TO CREATE A SAVE FILE UPON STARTING A NEW GAME
        public void CreateNewCharacterSaveFile(CharacterSaveData characterData)
        {
            // MAKE A PATH TO SAVE THE FILE( A lOCATION ON THE MACHINE)
            string savePath = Path.Combine(saveDataDirectory, saveFileName);
            try
            {
                // CREATE THE DIRECTORY THE FILE WILL BE WRITTEN TO, IF IT DOES NOT ELREADY EXISIS
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("Creating save file, at save path: " + savePath);

                // SERIALIZE THE C# GAME DATA OBJECT INTO JSON
                string dataToStore = JsonUtility.ToJson(characterData, true);

                // WRITE THE FILE TO OUR SYSTEM
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream)) 
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch(Exception e)
            {
                Debug.LogError("Error whilst trying to save character data, game not saved" + savePath + "\n" + e);
            }
        }
        // USED TO LOAD A SAVE FILE UPON LOADING A PREVIOUS GAME
        public CharacterSaveData LoadSaveFile()
        {
            CharacterSaveData characterData = null;
            // MAKE A PATH TO SAVE THE FILE( A lOCATION ON THE MACHINE)
            string loadPath = Path.Combine(saveDataDirectory, saveFileName);

            if(File.Exists(loadPath))
            {
                try
                {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }
                    // DESERIALIZE THE DATA FORM JSON BACK TO UNITY
                    characterData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
                }
                catch(Exception e)
                {
                    
                }
            }
            return characterData;
        }
    }

}