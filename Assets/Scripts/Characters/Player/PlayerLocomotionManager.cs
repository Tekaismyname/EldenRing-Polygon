using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TK;
using UnityEngine;

namespace Tk
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player; 
        public float horizontalMovement;
        public float verticalMovement;
        public float moveAmount;
        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 5;
        private Vector3 moveDirection;
        protected override void Awake()
        {
            base.Awake();
            player = gameObject.GetComponent<PlayerManager>();  
        }
    
        public void HandleAllMovement()
        {
            //  GROUNDED MOVEMENT
            HandleGroundMovement();
            // AERIAL MOVEMENT
        }

        private void GetVerticalAndHorizontalInputs()
        {
            verticalMovement = PlayerInputManager.instance.GetVerticalInput();
            horizontalMovement = PlayerInputManager.instance.GetHorizontal();

            //CLAMP THE MOVEMENTS
        } 
        private void HandleGroundMovement()
        {
            GetVerticalAndHorizontalInputs();
            //  OUR MOVE DIRECTION IS BASED ON OUR CAMERA FACING PERSPECTIVE & OUR MOVEMENT INPUTS
            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if(PlayerInputManager.instance.moveAmount > 0.5f)
            {
                //  MOVE AT RUNNING SPEED
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if(PlayerInputManager.instance.moveAmount <= 0.5f ) 
            {
                //  MOVE AT WALKING SPEED
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }
     }   
}

