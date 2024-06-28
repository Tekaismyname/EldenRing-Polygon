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
    }

}
