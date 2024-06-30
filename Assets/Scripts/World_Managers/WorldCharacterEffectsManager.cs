using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class WorldCharacterEffectsManager : MonoBehaviour
    {
        public static WorldCharacterEffectsManager instance;

        [Header("Damage")]
        public TakeDamageEffect takeDamageEffect;

        [SerializeField] List<InstantCharacterEffect> instantEffects;
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            GenerateEffectIDS();
        }
        private void GenerateEffectIDS()
        {
            for(int i = 0; i < instantEffects.Count; i++) 
            {
                instantEffects[i].instantEffectID = i;
            }
        }
    }

}
