using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace TK { 
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;

        public PlayerManager player;
        public PlayerControls playerControls;
        // THINK ABOUT THE GOAL IN STEP
        // 1. FIND THE WAY TO READ THE VALUE OF A JOY STICK
        // 2. MOVE THE CHARACTER BASED ON THOSE VALUES

        [Header("CAMERA MOVEMENT INPUT")]
        [SerializeField] Vector2 cameraInput;
        public float cameraHorizontalInput;
        public float cameraVerticalInput;

        [Header("PLAYER MOVEMENT INPUT")]
        [SerializeField] Vector2 movementInput;
        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;

        [Header("PLAYER ACTION INPUT")]
        [SerializeField] bool dodgeInput = false;

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
            
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            //WHEN THE SCENE CHANGE, RUN THIS LOGIC 
            SceneManager.activeSceneChanged += OnSceneChange;

            instance.enabled = false;    
        }
        private void Update()
        {
            HandleAllInputs();
        }
        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            // IF WE ARE LOADING INTO OUR WORLD SCENE, ENABLE OUR PLAYER CONTROLS
            if(newScene.buildIndex == WorldSaveGameManager.intance.getWorldIndex())
            {
                instance.enabled = true;
            }
            // OTHERWISE WE MUST BE AT THE MAIN MENU, DISABLE OUR PLAYER CONTROLS
            // THIS IS SO OUR PLAYER CANT MOVE AROUND IF WE ENTER THING LIKE A CHARACTER CREATION MENU ETC
            else
            {
                instance.enabled = false;
            }
        }
        private void OnEnable()
        {
            if(playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
            }
            playerControls.Enable();
        }

        private void OnDestroy()
        {
            //IF WE DESTROY THIS OBJECT, UNSUBCRIBE FROM THIS EVENT 
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        // IF WE MINUMIZE OR LOWER THE WINDOWN, STOP ADJUSTING  INPUTS
        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }

        private void HandleAllInputs()
        {
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
        }
        //  MOVEMENT
        private void HandlePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            //  RETURN THE ABSOLUTE NUMBER, (meaning number without the negative sign, so its always positive)
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            //WE CLAMP THE VALUES, SO THEY ARE 0, 0.5, 1 (optional)
            if( moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;

            }
            else if(moveAmount >  0.5 && moveAmount < 1)
            {
                moveAmount = 1;
            }

            // WHY DO WE PASS 0 ON THE HORIZONTAL? BECAUSE WE ONLY WANT NON-STARFING MOVEMENT
            // WE USE THE HORIZONTAL WHEN WE ARE STRAFING OR LOCKED ON


            if (player == null)
                return;
            // IF WE ARE NOT LOCKED ON, ONLY USE MOVE AMOUNT
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);

            // IF WE ARE LOCKED ON PASS THE HORIZONTAL MOVEMENT AS WELL
        }
        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }

        // ACTION
        private void HandleDodgeInput()
        {
            if(dodgeInput)
            {
                dodgeInput = false;
                // FUTURE NOTE: RETURN IF (DO NOTHING) IF MENU OR UI WINDOW IS OPEN
                //PERFORM A DODGE
                player.playerLocomotionManager.AttempToPerformDodge();
            }
        }
    }
}
