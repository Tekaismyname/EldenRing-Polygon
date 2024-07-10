using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {
        [Header("Attacking Character")]
        public CharacterManager characterCausingDamage; // (when calculating damage this is used to check for attackers damage modifiers, effects ect)

    }
}