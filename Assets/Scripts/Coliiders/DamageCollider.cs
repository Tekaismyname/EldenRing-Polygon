using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class DamageCollider : MonoBehaviour
    {
        [Header("Collider")]
        protected Collider damageCollider;

        [Header("Damage")]
        public float physicalDamage = 0; // (in the fure will be split into "standard", "Strile". "Slash" and "Pierce"
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float lightningDamage = 0;
        public float holyDamage = 0;
        public float gravityDamage = 0;

        [Header("Contact Point")]
        private Vector3 contactPoint;

        [Header("Characters Damaged")]
        protected List<CharacterManager> characterDamaged = new List<CharacterManager>();
        private void OnTriggerEnter(Collider other)
        {
            CharacterManager damageTarget = other.GetComponent<CharacterManager>();

            if (damageTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                // CHECK IF WE CAN DAMAGE THIS TARGET BASED ON FRIENDLY FIRE

                // CHECK IF TARGET IS BLOCKING

                // CHECK IF TARGET IS INVULNERABLE

                // DAMAGE
                DamageTarget(damageTarget);
            }
        }

        protected virtual void DamageTarget(CharacterManager damageTarget)
        {
            // WE DON'T WANT TO DAMAGE THE SAME TARGET MORE THAN ONCE IN A SINGLE ATTACK
            // SO WE ADD THEM TO A LIST THAT CHECKS BEFORE APPLYING DAMAGE

            if (characterDamaged.Contains(damageTarget)) return;

            characterDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.lightningDamage = lightningDamage;
            damageEffect.holyDamage = holyDamage;
            damageEffect.gravityDamage = gravityDamage;
            damageEffect.contactPoint = contactPoint;

            damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
        }

        public virtual void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public virtual void DisableDamageCollider()
        {
            damageCollider.enabled = false;
            characterDamaged.Clear(); // WE RESET THE CHARACTERS THAT HAVE BEEN HT WHEN WE RESET THE COLLIDER, SO THEY MAY BE HIT AGAIN
        }
    }   

}
