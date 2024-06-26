using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TK
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        CharacterManager character;
        [Header("Ground check & Jumping")]
        [SerializeField] float gravityForce = -5.55f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float groundCheckSphereRaidus = 1;
        [SerializeField] protected Vector3 yVelocity; // THE FORCE AT WHICH OUR CHARACTER IS PULLE UP OR DOWN (jumping or falling)
        [SerializeField] protected float groundedYVelocity = -20; // THE FORCE AT WHICH OUR CHARACTER IS STICKING TO THE GORUND WHILST THEY ARE GORUNDED
        [SerializeField] protected float fallStartYVelocity = -5; // THE FORCE AT WHICH OUR CHARACTER BEGINS TO FALL WHEN THEY BECOME UNGROUNDED ( RISE AS THEY FALL LONGER )
        protected bool fallingVelocityHasBeenSet = false;
        protected float inAirTimer = 0;
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Update()
        {
            HandleGroundCheck();

            if(character.isGrounded)
            {
                // IF NOT JUMP
                if(yVelocity.y < 0)
                {
                    inAirTimer = 0;
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            else
            {
                // IF NOT JUMP OR FALLING VERLOCITY HAS NOT BEN SET
                if(!character.isJumping && !fallingVelocityHasBeenSet) 
                {
                    fallingVelocityHasBeenSet= true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer += Time.deltaTime;
                character.animator.SetFloat("InAirTimer", inAirTimer);
                yVelocity.y += gravityForce * Time.deltaTime;
                
            }
            // IF THERE SHOULD ALWAYS BE SOME FORCE APPLIED TO THE Y VELOCIRY
            character.characterController.Move(yVelocity * Time.deltaTime);
        }

        protected void HandleGroundCheck()
        {
            character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRaidus, groundLayer);
        }
        // DRAWS OUR GROUND CHECK SPHERE IN SCENE VIEW
        protected void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(character.transform.position, groundCheckSphereRaidus);
        }
    }

}