using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace TK
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;
        [Header("NETWORK JOIN")]
        [SerializeField] bool startGameAsClient;

        [HideInInspector] public PlayerUiHudManager playerUiHudManager;
        [HideInInspector] public PlayerUIPopUpManager playerUIPopUpManager;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
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
            playerUiHudManager = GetComponentInChildren<PlayerUiHudManager>();
            playerUIPopUpManager= GetComponentInChildren<PlayerUIPopUpManager>();
        }
        private void Update()
        {
            if(startGameAsClient)
            {
                startGameAsClient = false;
                //WE MUST FIRST SHUT DOWN, BECAUSE WE HAVE STARTED AS A HOST DURING THE TITLE SCREEN
                NetworkManager.Singleton.Shutdown();

                
                //WE THEN RESTART, AS A CLIENT
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}

