using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TK
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;
        float vertical;
        float horizontal;
        protected virtual private void Awake()
        {
            character= GetComponent<CharacterManager>();
        }
        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
        {
            // OPTION 1
            if (character == null)
            {
                Debug.Log("Character Animator Null");
                return;
            }         
            else
            {
                character.animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
                character.animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);
            }
            
        }
    }
}

