using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace TK
{
    public class TitleScreenManager : MonoBehaviour
    {
        public void StartNetworkAtHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame()
        {
            StartCoroutine(WorldSaveGameManager.intance.LoadNewGame());
        }
    }
}

