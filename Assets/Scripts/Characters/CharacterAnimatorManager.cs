using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace TK
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;
        float vertical;
        float horizontal;
        protected virtual private void Awake()
        {
            character= GetComponent<CharacterManager>();
        }
        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
        {
            // OPTION 1
            if (character == null)
            {
                Debug.Log("Character Animator Null");
                return;
            }         
            else
            {
                character.animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
                character.animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);
            }
            
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

