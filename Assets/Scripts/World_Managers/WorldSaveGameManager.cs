using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace TK
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager intance;
        [SerializeField] int worldIndexNumber = 1;

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

        public IEnumerator LoadNewGame()
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

