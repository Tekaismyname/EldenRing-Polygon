using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace TK
{
    public class CharacterStatsManager : MonoBehaviour
    {
        CharacterManager character;
        [Header("Stamina Regeneration")]
        private float staminaRegenerationTimer = 0;
        private float staminaStickTimer;
        [SerializeField] float staminaRegenerationDelay = 1;
        [SerializeField] float staminaRegenerationAmount = 2;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }
        public int CalculateStaminaBasedEnduranceLevel(int endurance)
        {
            float stamina = 0;

            // CREATE AN EQUATION FOR HOW YOU WANT YOUR STAMINA TO BE CALCULATED

            stamina = endurance * 10;

            return Mathf.RoundToInt(stamina);
        }
        public virtual void RegenerateStamina()
        {
            //  ONLY OWNER CAN EDIT THEIR NETWORK VARIABLES
            if (!character.IsOwner)
            {
                return;

            }
            //  WE DO NOT WANT TO REGENARATE STAMINA IF WE ARE USING IT
            if (character.characterNetworkManager.isSprinting.Value)
            {
                return;
            }

            if (character.isPerformingAction)
            {
                return;
            }

            staminaRegenerationTimer += Time.deltaTime;

            if (staminaRegenerationTimer >= staminaRegenerationDelay)
            {
                if (character.characterNetworkManager.currentStamina.Value < character.characterNetworkManager.maxStamina.Value)
                {
                    staminaStickTimer += Time.deltaTime;
                    if (staminaStickTimer >= 0.1)
                    {
                        staminaStickTimer = 0;
                        character.characterNetworkManager.currentStamina.Value += staminaRegenerationAmount;
                    }
                }
            }
        }

        public virtual void ResetStaminaRegenTimer(float previousStaminaAmount, float currentStaminaAmount)
        {
            // WE ONLY WANT TO RESET THE REGENERATION IF THE ACTION USED STAMINA
            // WE DONT WANT TO RESET THE REGENEERATION IF WE ARE ALREADY REGENERATION STAMINA
            if(currentStaminaAmount < previousStaminaAmount)
            {
                staminaRegenerationTimer = 0;
            }
        }
    }
}
