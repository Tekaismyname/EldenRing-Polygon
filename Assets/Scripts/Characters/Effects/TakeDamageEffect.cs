using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Damage")]
    public class TakeDamageEffect : InstantCharacterEffect
    {
        [Header("Character Causing Damage")]
        public CharacterManager characterCausingDamage; //If the damage is caused by another characters attack it will be stored here

        [Header("Damage")]
        public float physicalDamage = 0; // (in the fure will be split into "standard", "Strile". "Slash" and "Pierce"
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float lightningDamage = 0;
        public float holyDamage = 0;
        public float gravityDamage = 0;

        [Header("Final Damage")]
        public int finalDamageDealt = 0;         // The damage the character takes after all calculations have been made

        [Header("Poise")]
        public float poiseDamage = 0;
        public bool poiseIsBroken = false;          // If a character's poise is broken,  they will be "Stunned" and play a damage animation

        // (TO DO) BUILD UPS
        // build up effect amounts

        [Header("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;


        [Header("Sound FX")]
        public bool willPlayDamageFX = true;
        public AudioClip elementalDamageSoundFX;    // USED ON TOP OF REGULAR SFX IF THERE IS ELEMENTAL DAMAGE PRESENT (Magic/Fire/Lightning/Holy)

        [Header("Direction Damage Taken From")]
        public float angleHitFrom;                  // USED TO DETERMINE WHAT DAMAGE ANIATION TO PLAY (Move backwards, to the left, to the right ect)
        public Vector3 contactPoint;                // USED TO DETERMINE WHERE THE BLOOD FX INSTANTIATE 


        public override void ProcessEffect(CharacterManager character)
        {
            base.ProcessEffect(character);
            // IF THE CHARACTER IS DEAD, NO ADDITIONAL DAMAGE EFFECTS SHOULD BE PROCESSED
            if(character.isDead.Value) return;

            // CHECK FOR "INVULNERABILITY"

            // CALCULATE DAMAGE
            CalculateDamage(character);
            // CHECK WHICH DIRECTIONAL DAMAGE CAME FROM 
            // PLAY A DAMAGE ANIMATION
            PlayDirectionalBasedDamageAnimation(character);
            // CHECK FOR BUILD UPS (POISE, BLEED ECT)
            // PLAY DAMAGE SOUND FX
            PlayDamageSFX(character);
            // PLAYE DAMAGE VFX (BLOOD)
            PlayDamageVFX(character);

            // IF CHARACTER IS A.I CHECK FOR NEW TARGET IF CHARACTER CAUSING DAMAGE IS PRESENT

        }

        private void CalculateDamage(CharacterManager character)
        {
            if(!character.IsOwner) return;

            if(characterCausingDamage != null)
            {
                // CHECK FOR DAMAGE MODIFIERS AND MODIFY BASE DAMAGE (PHYSICAL/ELEMENTTAL DAMAGE BUFF)   
            }
            // CHECK CHARACTER FOR FLAT DEPENSES AND SUBTRACT THEN FROM THE DAMAGE

            // CHECK CHARACTER FOR ARMOR ABSORPTIONS, AND SUBTRACT THE PERCENTAGE FROM THE DAMAGE

            // ADD ALL DAMAGE TYPES TOGETHER, AND APPLY FINAL DAMAGE
            finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + fireDamage + lightningDamage + holyDamage + gravityDamage);

            if(finalDamageDealt <= 0)
            {
                finalDamageDealt = 1;
            }

            Debug.Log("FINAL DAMAGE GIVEN: " + finalDamageDealt);
            character.characterNetworkManager.currentHealth.Value -= finalDamageDealt;

            // CACULATE POSE DAMAGE TO DETERMINE IF THE CHARACTER WILL BE STUNNED
        }

        private void PlayDamageVFX(CharacterManager character)
        {
            // IF WE HAVE FIRE DAMAGE, PLAY FIRE PARTICLES
            // LIGHTNING DAMAGE, LIGHTNING PARTICLES ECT

            character.characterEffectsManager.PlayBloodSplatterVFX(contactPoint);
        }

        private void PlayDamageSFX(CharacterManager character)
        {
            AudioClip physicalDamageSFX = WorldSoundFXManager.instance.ChooseRandomSGXFromArray(WorldSoundFXManager.instance.physicalDamageSFX);

            character.characterSoundFXManager.PlaySoundFX(physicalDamageSFX);
            // IF FIRE DAMAGHE IS GREATER THAN O, PLAY BURN SFX
            // IF LIGHTNING DMAAGE IS GREATER THAN 0, PLAY ZAP SFX
        }

        private void PlayDirectionalBasedDamageAnimation(CharacterManager character)
        { 
            if(!character.IsOwner) return;
            // TODO CALCULATE IF POISE IS BROKEN
            poiseIsBroken = true;

            if(angleHitFrom >= 145 && angleHitFrom <= 180)
            {
                damageAnimation = character.characterAnimatorManager.GetRandomAnimationFromList(character.characterAnimatorManager.forward_Medium_Damage);
            }
            else if (angleHitFrom <= -145 && angleHitFrom >= -180)
            {
                damageAnimation = character.characterAnimatorManager.GetRandomAnimationFromList(character.characterAnimatorManager.forward_Medium_Damage);
            }
            else if (angleHitFrom >= -45 && angleHitFrom <= 45)
            {
                damageAnimation = character.characterAnimatorManager.GetRandomAnimationFromList(character.characterAnimatorManager.backward_Medium_Damage);
            }
            else if (angleHitFrom >= -144 && angleHitFrom <= -45)
            {
                damageAnimation = character.characterAnimatorManager.GetRandomAnimationFromList(character.characterAnimatorManager.left_Medium_Damage);
            }
            else if (angleHitFrom >= 45 && angleHitFrom <= 144)
            {
                damageAnimation = character.characterAnimatorManager.GetRandomAnimationFromList(character.characterAnimatorManager.right_Medium_Damage);
            }

            // IF POISE IS BROKEN, PLAY A STAGGERING DAMAGE ANIMATON
            if (poiseIsBroken)
            {
                character.characterAnimatorManager.lastDamageAnimationPlayed = damageAnimation;
                character.characterAnimatorManager.PlayerTargetActionAnimation(damageAnimation, true);
            }
        }
    }

}
