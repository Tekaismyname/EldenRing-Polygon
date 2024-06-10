using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace TK { 
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        // THINK ABOUT THE GOAL IN STEP
        // 1. FIND THE WAY TO READ THE VALUE OF A JOY STICK
        // 2. MOVE THE CHARACTER BASED ON THOSE VALUES
        [Header("PLAYER MOVEMENT INPUT")]
        PlayerControls playerControls;
        [SerializeField] Vector2 movementInput;
        public float horizontalInput;
        public float verticalInput;

        [Header("CAMERA MOVEMENT INPUT")]
        [SerializeField] Vector2 cameraInput;
        public float cameraHorizontalInput;
        public float cameraVerticalInput;


        public float moveAmount;
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
            }
            playerControls.Enable();
        }

        private void OnDestroy()
        {
            //IF WE DESTROY THIS OBJECT, UNSUBCRIBE FROM THIS EVENT 
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void Update()
        {
            HandleMovementInput();

            HandleCameraMovementInput();
        }
        private void HandleMovementInput()
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

        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }
    }
}
