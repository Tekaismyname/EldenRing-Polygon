using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        // PROCESS INSTANT EFFECTS (TAKE DAMAGE, HEAL)

        // PROCESS TIMED EFFECTS (POISON, BUILD UPS)

        // PROCESS STATIC EFFECTS (ADDING/REMOVING BUFFS FROM TALISMANS ETC)
        CharacterManager character;

        [Header("VFX")]
        [SerializeField] GameObject bloodSplatterVFX;
        protected virtual void Awake()
        {
            character = gameObject.GetComponent<CharacterManager>();
        }
        public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
        {
            // TAKE IN AN EFFECT
            // PROCESS IT
            effect.ProcessEffect(character);
        }

        public void PlayBloodSplatterVFX(Vector3 contactPoint)
        {
            // IF WE MANUALLY HAVE A BLOOD SPLATTER VFDX ON THIS MODEL, PLAY ITS VERSION
            if(bloodSplatterVFX != null)
            {
                GameObject bloodSplatter = Instantiate(bloodSplatterVFX, contactPoint, Quaternion.identity);
            }
            // ELSE, UUSE THE GENERIC (DEFAULT VERSION) WE HAVE ELSEWHERE
            else
            {
                GameObject bloodSplatter = Instantiate(WorldCharacterEffectsManager.instance.bloodSplatterVFX, contactPoint, Quaternion.identity);
            }
        }
    }

}
