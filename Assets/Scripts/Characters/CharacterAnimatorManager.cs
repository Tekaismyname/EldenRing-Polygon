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

        [Header("Damage Animations")]
        public string lastDamageAnimationPlayed;

        [SerializeField] string hit_Forward_Medium_01 = "hit_Forward_Medium_01";
        [SerializeField] string hit_Forward_Medium_02 = "hit_Forward_Medium_02";

        [SerializeField] string hit_Backward_Medium_01 = "hit_Backward_Medium_01";
        [SerializeField] string hit_Backward_Medium_02 = "hit_Backward_Medium_02";

        [SerializeField] string hit_Left_Medium_01 = "hit_Left_Medium_01";
        [SerializeField] string hit_Left_Medium_02 = "hit_Left_Medium_02";

        [SerializeField] string hit_Right_Medium_01 = "hit_Right_Medium_01";
        [SerializeField] string hit_Right_Medium_02 = "hit_Right_Medium_02";

        public List<string> forward_Medium_Damage = new List<string>();
        public List<string> backward_Medium_Damage = new List<string>();
        public List<string> left_Medium_Damage = new List<string>();
        public List<string> right_Medium_Damage = new List<string>();
        protected virtual private void Awake()
        {
            character= GetComponent<CharacterManager>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        protected virtual void Start()
        {
            forward_Medium_Damage.Add(hit_Forward_Medium_01);
            forward_Medium_Damage.Add(hit_Forward_Medium_02);

            backward_Medium_Damage.Add(hit_Backward_Medium_01);
            backward_Medium_Damage.Add(hit_Backward_Medium_02);

            left_Medium_Damage.Add(hit_Left_Medium_01);
            left_Medium_Damage.Add(hit_Left_Medium_02);

            right_Medium_Damage.Add(hit_Right_Medium_01);
            right_Medium_Damage.Add(hit_Right_Medium_02);

        }

        public string GetRandomAnimationFromList(List<string> animationList)
        {
            List<string> finalList = new List<string>();

            foreach(var item in animationList)
            {
                finalList.Add(item);
            }

            // CHECK IF WE HAVE ALREADY PLAYED THIS DAMAGE ANIMATION SO IT DOESNT REPEAT
            finalList.Remove(lastDamageAnimationPlayed);
            // CHECK THE LIST FOR NULL ENTRIES, AND REMOVE THEM
            for(int i = finalList.Count - 1; i > -1; i--)
            {
                if (finalList[i] == null)
                {
                    finalList.RemoveAt(i);
                }
            }

            int randomValue = Random.Range(0, finalList.Count);
            return finalList[randomValue];
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
            Debug.Log("PLAYING ANIMATION+ " + targetAnimation);
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

        public virtual void PlayerTargetAttackActionAnimation( AttackType attackType,
                                                       string targetAnimation,
                                                       bool isPerforming,
                                                       bool applyRootMotion = true,
                                                       bool canRotate = false,
                                                       bool canMove = false)
        {
            // KEEP TRACK OF LAST ATTACK PERFORMED ( FOR COMBOS )
            // KEEP TRACK OF CURRENT ATTACK TYUPE (LIGHT, HEAVY, ETC)
            // UPDATE ANIMATOR SET TO CURRENT WEAPONS ANIMATIONS
            // DECIDE IF OUR ATTACK CAN BE PARRIED
            // TELL THE NETWORK OUR "ATTACKING" FLAG IS ACTIVE ( FOR COUNTER DAMANAGE ETC)
            character.characterCombatManager.currentAttackType= attackType;
            character.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnimation, 0.2f);
            character.isPerformingAction = isPerforming;
            character.canMove = canMove;
            character.canRotate = canRotate;

            // TELL THE SERVER/HOST WE PLAYED AN ANIMATION, AND TO PLAY THAT ANIMATION FOR EVERYBODY ELSE PRESENT
            character.characterNetworkManager.NotifyTheSeverOfAttackActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
            Debug.Log("Local Client ID: " + NetworkManager.Singleton.LocalClientId);
        }
    }
}

