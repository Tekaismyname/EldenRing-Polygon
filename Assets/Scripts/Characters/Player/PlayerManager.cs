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
            // HANDLE ALL CHARACTER MOVEMENT
            playerLocomotionManager.HandleAllMovement();
        }
    }
}
