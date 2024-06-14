using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace TK
{
    public class CharacterManager : NetworkBehaviour
    {
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator animator;
        [HideInInspector] public CharacterNetworkManager characterNetworkManager;

        [Header("Flags")]
        public bool isPerformingAction = false;
        public bool applyRootMotion = false;
        public bool canRotate = true;
        public bool canMove = true;
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            characterNetworkManager = GetComponent<CharacterNetworkManager>();
            
        }

        protected virtual void Update()
        {
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
                    ,ref characterNetworkManager.networkPositionVelocity
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
    }
}

