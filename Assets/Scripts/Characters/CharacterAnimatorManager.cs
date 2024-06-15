using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace TK
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;
        int vertical;
        int horizontal;
        protected virtual private void Awake()
        {
            character= GetComponent<CharacterManager>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }
        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue, bool isSprinting)
        {
            float horizontalAmount = horizontalValue;
            float verticalAmount = verticalValue;

            if(isSprinting )
            {
                verticalAmount= 2;
            }
            character.animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
            character.animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
        }

        public virtual void PlayerTargetActionAnimation(string targetAnimation, 
                                                        bool isPerforming, 
                                                        bool applyRootMotion = true, 
                                                        bool canRotate =false, 
                                                        bool canMove = false)
        {
            character.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnimation, 0.2f);
            // CAN BE USED TO STOP CHARACTER FORM ATTEMPTING NEW ACTIONS
            // FOR EXAMPLE, IF YOU GET A DAMAGED, AND YOU BEGIN PERFORMING A DAMAGE ANIMATION
            // THIS FLAG WILL TURN TRUE IF YOU ARE STUNNED
            // WE CAN THEN CHECK FOR THIS BEGORE ATTEMPTING NEW ACTIONS
            character.isPerformingAction = isPerforming;
            character.canMove = canMove;
            character.canRotate = canRotate;

            // TELL THE SERVER/HOST WE PLAYED AN ANIMATION, AND TO PLAY THAT ANIMATION FOR EVERYBODY ELSE PRESENT
            character.characterNetworkManager.NotifyTheSeverOfActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
            Debug.Log("Local Client ID: " + NetworkManager.Singleton.LocalClientId);
        }
    }
}

