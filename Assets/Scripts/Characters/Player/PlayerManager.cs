using System.Collections;
using System.Collections.Generic;
using Tk;
using UnityEngine;

namespace TK
{
    public class PlayerManager : CharacterManager
    {
        PlayerLocomotionManager playerLocomotionManager;
        protected override void Awake()
        {
            base.Awake();

            //DO MORE STUFF ONLY FOR THE PLAYER
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
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
            }
        }
    }
}
