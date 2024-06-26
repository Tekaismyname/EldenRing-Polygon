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
        [HideInInspector] public float horizontalMovement;
        [HideInInspector] public float verticalMovement;
        [HideInInspector] public float moveAmount;


        [Header("Movement Settings")]
        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;
        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 5;
        [SerializeField] float rotationSpeed = 15;
        [SerializeField] float sprintingSpeed = 6.5f;
        [SerializeField] int sprintingStaminaCost = 5;
        [Header("Dodge")]
        private Vector3 rollDirection;
        [SerializeField] float dodgeStaminaCost = 25;
        [SerializeField] float backstepStaminaCost = 15;
        [SerializeField] float jumpStaminaCost = 10;
        protected override void Awake()
        {
            base.Awake();
            player = gameObject.GetComponent<PlayerManager>();  
        }

        protected override void Update()
        {
            base.Update();

            if (player.IsOwner)
            {
                player.characterNetworkManager.verticalMovement.Value = verticalMovement;
                player.characterNetworkManager.horizontalMovement.Value = horizontalMovement;
                player.characterNetworkManager.moveAmout.Value = moveAmount;
            }
            else
            {
                verticalMovement = player.characterNetworkManager.verticalMovement.Value;
                horizontalMovement = player.characterNetworkManager.horizontalMovement.Value;
                moveAmount = player.characterNetworkManager.moveAmout.Value;

                // IF NOT CLOCKED ON, PASS MOVE AMOUT
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);

                // IF LOCKED ON, PASS HORIZONTAL AND VERTICAL
            }
        }
        public void HandleAllMovement()
        {
            
            //  GROUNDED MOVEMENT
            HandleGroundMovement();
            HandleRotation();
            // AERIAL MOVEMENT
        }

        private void GetMovementValues()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
            moveAmount = PlayerInputManager.instance.moveAmount;
            //CLAMP THE MOVEMENTS
        } 
        private void HandleGroundMovement()
        {
            if (!player.canMove) return;
            GetMovementValues();
            //  OUR MOVE DIRECTION IS BASED ON OUR CAMERA FACING PERSPECTIVE & OUR MOVEMENT INPUTS
            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if(player.playerNetworkManager.isSprinting.Value)
            {
                player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
            }
            else
            {
                if (PlayerInputManager.instance.moveAmount > 0.5f)
                {
                    //  MOVE AT RUNNING SPEED
                    player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
                }
                else if (PlayerInputManager.instance.moveAmount <= 0.5f)
                {
                    //  MOVE AT WALKING SPEED
                    player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
                }
            }
  
        }
        private void HandleRotation()
        {
            if (!player.canRotate) return;
            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if(targetRotationDirection == Vector3.zero )
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
        public void HandleSprinting()
        {
            if (player.isPerformingAction)
            {
                // SET SPRINTING TO FALSE
                player.playerNetworkManager.isSprinting.Value = false;
            }
            // IF WE ARE OUT THE STAMINA, SET SPRINTING TO FALSE
            if(player.playerNetworkManager.currentStamina.Value <= 0)
            {
                player.playerNetworkManager.isSprinting.Value = false;
                return;
            }
            // IF WE ARE MOVING SET SPRINTING TO TRUE
            if(moveAmount >= 0.5)
            {
                player.playerNetworkManager.isSprinting.Value = true;
            }
            // // IF WE ARE STATIONARY/MOVING SLOWLY SET SPRINTING TO FALSE
            else
            {
                player.playerNetworkManager.isSprinting.Value = false;
            }
            
            if(player.playerNetworkManager.isSprinting.Value)
            {
                player.playerNetworkManager.currentStamina.Value -= sprintingStaminaCost * Time.deltaTime;
            }
        }
        public void AttempToPerformDodge()
        {
            if(player.isPerformingAction) return;
            if (player.playerNetworkManager.currentStamina.Value <= 0) return;
            // IF WE ARE MOVING WHEN WE ATTEMPT TO DODGE, WE PERFORM A ROLL
            if(PlayerInputManager.instance.moveAmount > 0)
            {
                rollDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
                rollDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;
                // show direction of player before his roll
                Debug.DrawLine(transform.position, rollDirection, Color.red);
                rollDirection.y = 0;
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;

                //PERFORM A ROLL ANIMATION
                player.playerAnimatorManager.PlayerTargetActionAnimation("Roll_Forward_01", true);

                // AFTER ROLLING STAMINA REDUCE 
                player.playerNetworkManager.currentStamina.Value -= dodgeStaminaCost;
            }
            // IF WE ARE STATIONARY, WE PERFORM A BACKSTEP
            else
            {
                // PERFORM A BACKSTEP ANIMATION
                player.playerAnimatorManager.PlayerTargetActionAnimation("Back_Step_01", true);
                player.playerNetworkManager.currentStamina.Value -= backstepStaminaCost;
            }
        }
        public void AttempToPerformJump()
        {
            // IF WE ARE PERFORMING A GENARAL ACTION, WE DO NOT WANT TO ALLOW A JUMP (WILL CHANGE WHEN COMBAT IS ADDED)
            if (player.isPerformingAction) return;
            // IF WE ARE OUT OF STAMINA, WE DO NOT WISH TO ALLOW A JUMP
            if (player.playerNetworkManager.currentStamina.Value <= 0) return;
            // IF WE ARE ALREADY IN A JUMP, WE DO NOT WANT TO ALLOW A JUMP AGAIN UNTILL THE CURRENT HUMP HAS FINISHED
            if (player.isJumping) return;
            // IF WE ARE NOT GROUNDED, WE DO NOT WANT TO ALLOW A JUMP
            if (player.isGrounded) return;
            // IF WE ARE TWO HANDING OUR WEAPON, PLAY THE TWO HANDED JUMP ANIMATION, OTHERWISE PLAY THE ONE HANDED ANIMATION ( TO DO )
            player.playerAnimatorManager.PlayerTargetActionAnimation("Main_Jump_Start_01", false);
            player.isJumping = true;
            player.playerNetworkManager.currentStamina.Value -= jumpStaminaCost;
        }
        public void ApplyJumpingVelocity()
        {
            // APPLY AN UPWARD VELOCITY 
        }
     }   
}

