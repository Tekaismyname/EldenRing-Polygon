using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.TextCore.Text;

namespace TK
{
    public class CharacterManager : NetworkBehaviour
    {
        [Header("Status")]
        public NetworkVariable<bool> isDead = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        
        
        [HideInInspector] public Animator animator;
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public CharacterNetworkManager characterNetworkManager;
        [HideInInspector] public CharacterEffectsManager characterEffectsManager;
        [HideInInspector] public CharacterAnimatorManager characterAnimatorManager;

        [Header("Flags")]
        public bool isPerformingAction = false;
        public bool isGrounded = true;

        public bool applyRootMotion = false;
        public bool canRotate = true;
        public bool canMove = true;

        
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            characterNetworkManager = GetComponent<CharacterNetworkManager>();
            characterEffectsManager = GetComponent<CharacterEffectsManager>();
            characterAnimatorManager = GetComponent<CharacterAnimatorManager>();    
        }

        protected virtual void Start()
        {
            IngnoreMyOwnColliders();
        }

        protected virtual void Update()
        {
            
            animator.SetBool("isGrounded", isGrounded);
            // IF THIS CHARACTER CONTROLLED FORM OUR SIDE, THEN ASSIGN ITS NETWORK POSITION TO THE POSITION OF OUR TRANSFORM
            if (IsOwner)
            {
                characterNetworkManager.networkPosition.Value = transform.position;
                characterNetworkManager.networkRotation.Value = transform.rotation;
                
            }
            //  IF THIS CHARACTER CONTROLLED FORM ELSE WHERE, THEN ASSIGN ITS POSITION HERE LOCALLY BY THE POSITION  OF ITS  NETWORK TRANSFORM
            else
            {
                // Position
                transform.position = Vector3.SmoothDamp(transform.position
                    , characterNetworkManager.networkPosition.Value
                    , ref characterNetworkManager.networkPositionVelocity
                    , characterNetworkManager.networkPositionSmoothTime);
                // Rotation
                transform.rotation = Quaternion.Slerp(transform.rotation
                    , characterNetworkManager.networkRotation.Value
                    , characterNetworkManager.networkRorationSmoothTime);
            }
        }

        protected virtual void LateUpdate()
        {

        }

        public virtual IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            if (IsOwner)
            {
                characterNetworkManager.currentHealth.Value = 0;
                isDead.Value = true;

                // RESET ANY FLAGS HERE THAT NEED TO BE RESET
                // NOTHING YET

                // IF WE ARE NOT GROUNDED, PLAY AERIAL DEATH ANIMATION
                if(!manuallySelectDeathAnimation)
                {
                    characterAnimatorManager.PlayerTargetActionAnimation("Dead_01", true);
                }
            }

            // PLAY SOME DEATH SFX

            yield return new WaitForSeconds(5);

            // AWARD PLAYERS WITH RUNES

            // DISABLE CHARACTER
        }
        
        public virtual void ReviveCharacter()
        {

        }

        protected virtual void IngnoreMyOwnColliders()
        {
            Collider characterControllerCollider = GetComponent<Collider>();
            Collider[] damageableCharacterColliders = GetComponentsInChildren<Collider>();

            List<Collider> ignoreColliders = new List<Collider>();

            // ADDS ALL OF OUR DAMAGEABLE CHARACTER COLLIDERS, TO THE LIST THAT WILL BE USED TO IGNORE COLLISIONS
            foreach(var collider in damageableCharacterColliders)
            {
                ignoreColliders.Add(collider);
            }

            // ADDS OUR CHARACTER CONTROLLDER COLLIDER TO THE LIST THAT WILL BE USED TO IGNORE COLLISIONS
            ignoreColliders.Add(characterControllerCollider);
            
            // GOES THROUGH EVERY COLLIDER ON THE LIST, AND IGNORES COLLISION WITH EACH OTHER
            foreach(var collider in ignoreColliders)
            {
                foreach (var otherCollider in ignoreColliders)
                {
                    Physics.IgnoreCollision(collider, otherCollider, true);
                }
            }
        }
    }
}

