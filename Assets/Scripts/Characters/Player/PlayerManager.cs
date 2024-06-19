using System.Collections;
using System.Collections.Generic;
using Tk;
using UnityEngine;

namespace TK
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;
        protected override void Awake()
        {
            base.Awake();

            //DO MORE STUFF ONLY FOR THE PLAYER
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager= GetComponent<PlayerAnimatorManager>();
            playerNetworkManager= GetComponent<PlayerNetworkManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
        }

        protected override void Update()
        {
            base.Update();
            // IF WE DO NOT OWNER THIS GAMEOBJECT, WE DO NOT CONTROL OR EDIT IT
            if (!IsOwner)
            {
                return;
            }

            // HANDLE ALL CHARACTER MOVEMENT
            playerLocomotionManager.HandleAllMovement();
            // REGENATE STAMINA
            playerStatsManager.RegenerateStamina();
        }

        protected override void LateUpdate()
        {
            if (!IsOwner)
            {
                return;
            }
            base.LateUpdate();

            PlayerCamera.instance.HandleAllCameraActions();
        }

        // THIS FUNCTION WILL CALL WHEN OBJECT SPAWN AS NETWORK
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            // IF THIS THE PLAYER OBJECT OWNED BY THIS CLIENT
            if(IsOwner)
            {
                PlayerCamera.instance.player = this;
                PlayerInputManager.instance.player = this;
                WorldSaveGameManager.intance.player = this;

                playerNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.instance.playerUiHudManager.SetNewStaminaValue;
                Debug.Log(playerNetworkManager.currentStamina.OnValueChanged);
                playerNetworkManager.currentStamina.OnValueChanged += playerStatsManager.ResetStaminaRegenTimer;
                Debug.Log("IsOwner");

                // THIS WILL BE MOVED WHEN SAVING AN LOADING IS ADDED
                playerNetworkManager.maxStamina.Value = playerStatsManager.CalculateStaminaBasedEnduranceLevel(playerNetworkManager.endurance.Value);
                playerNetworkManager.currentStamina.Value = playerStatsManager.CalculateStaminaBasedEnduranceLevel(playerNetworkManager.endurance.Value);
                PlayerUIManager.instance.playerUiHudManager.SetMaxStaminaValue(playerNetworkManager.maxStamina.Value);
            }
        }

        public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            Debug.Log("Save Data to current Character Player success");
            currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
            currentCharacterData.xPosition = transform.position.x;
            currentCharacterData.yPosition = transform.position.y;
            currentCharacterData.zPosition = transform.position.z;
        }
        public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            playerNetworkManager.characterName.Value = currentCharacterData.characterName;
            Vector3 myPosition = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
            transform.position= myPosition;
        }
    }
}
