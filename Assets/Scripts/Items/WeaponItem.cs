using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class WeaponItem : Item
    {
        // ANIMATOR CONTROLLER OVERIDE ( Change attack animations based on weapon you are currently using)

        [Header("Weapon Model")]
        public GameObject weaponModel;

        [Header("Weapon Requirements")]
        public int strREQ = 0;
        public int dexREQ = 0;
        public int intREQ = 0;
        public int faithREQ = 0;

        [Header("Weapon Base Damage")]
        public int physicalDamage = 0;
        public int fireDamage = 0;
        public int magicDamage = 0;
        public int holyDamage = 0;
        public int lightningDamage = 0;
        public int gravityDamage = 0;

        // WEAPON GUARD ABSORPTIONS (BLOCKING POWER)

        [Header("Weapon Base Poise Damage")]
        public float poseDamage = 10;
        // OFFENSIVE POISE BONUS WHEN ATTACKING

        // WEAPON MODIFIERS
        // LIGHT ATTACK MODIFIER
        // HEAVY ATTACK MODIFIER
        // CRITICAL DAMAGE MODIFIER ECT

        [Header("Stamina Costs")]
        public int baseStaminaCost = 20;
        // RUNNING ATTACK STAMINA COST MODIFIER
        // LIGHT ATTACK STAMINA COST MODIFIER
        // HEAVY ATTACK STAMINA COST MODIFIER 

        [Header("Actions")]
        public WeaponItemAction oh_RB_Action; // ONE HAND RIGHT BUMPER ACTION

        // ASH OF WAR

        // BLOCKING SOUNDS
            
    }
}
