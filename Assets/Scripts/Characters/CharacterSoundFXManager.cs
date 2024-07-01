using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        private AudioSource audioSource;

        protected virtual void Awake()
        {

        }

        public void PlayRollSoundFX()
        {
            //audioSource.PlayOneShot(WorldSoundFXManager.instance.rollSFX);
            
        }
    }
}
