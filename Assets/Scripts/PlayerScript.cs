using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using System.Security.Cryptography;
//using System.Threading;
//using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
//using UnityEngine.Playables;
using UnityEngine.InputSystem;

namespace Player
{

    public class PlayerScript : MonoBehaviour
    {
        public PlayerInput playerInput;
        UnityEngine.VFX.VisualEffect vfx;

        public bool inWater;

        public GameObject[] playerModelPrefab;
        public GameObject currentPlayerModel;

        public CharacterController controller;
        public Transform cam;
        public Timer timer;

        public float speed = 6;
        public float gravity = -9.81f;
        public float jumpHeight = 3;
        public Vector3 velocity;
        public bool isGrounded;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        float turnSmoothVelocity;
        public float turnSmoothTime = 0.1f;

        public LevelManager lm;
        public GameObject lmObject;
        public StateMachine sm;

        public AudioManager audioManager;
        public GameObject audioManagerObject;

        public Animator anim;

        public IdleState idleState;
        public WalkState walkState;
        public RunState runState;
        public JumpState jumpState;
        public FloatState floatState;
        public SwimState swimState;
        public DeathState deathState;

        public bool sprint;
        public bool escapeMenu;

        //public bool jumpPressed;

        private void Start()
        {
            sm = gameObject.AddComponent<StateMachine>();
            //rb  = GetComponent<Rigidbody>();
            anim  = GetComponent<Animator>();
            audioManager = GetComponent<AudioManager>();
            lm = lmObject.GetComponent<LevelManager>();
            audioManager = audioManagerObject.GetComponent<AudioManager>();
            

            //add variables here to be shared across all states

            vfx = GetComponent<UnityEngine.VFX.VisualEffect>();


            // add new states here
            idleState = new IdleState(this, sm);
            runState = new RunState(this, sm);
            jumpState = new JumpState(this, sm);
            walkState = new WalkState(this, sm);
            floatState = new FloatState(this, sm);
            swimState = new SwimState(this, sm);
            deathState = new DeathState(this, sm);

            sm.Init(idleState);

            audioManager.PlayBGM(1);

        }
        private void Awake()
        {
            //lm = GetComponent<LevelManager>();
        }

        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }

        // Update is called once per frame
        void Update()
        {

            sm.CurrentState.LogicUpdate();

            velocity.y += gravity * Time.deltaTime; //gravity
            controller.Move(velocity * Time.deltaTime);


            /*
            //jump
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
                /*if (lm.inWater == false)
                {
                }
                if (lm.inWater == true)
                {
                    gravity = -12f;

                    //swim stuff
                }*/



        }

        public bool CheckForMovement()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            sprint = false;

            if (direction.magnitude >= 0.1f)
            {
                return true;

            }
            return false;
        }

        public bool CheckForJump()
        {
            if (Input.GetKey(KeyCode.Space) == true) //&& isGrounded
            {
                return true;

            }
            return false;
        }

        public bool CheckForDeath()
        {
            ///timer.remainingTime = 15f;

            if (timer.remainingTime <= 0.1f)
            {
                Debug.Log("Dead");
                return true;

            }
            /*if(lm.killPlayer == true)
            {
                Debug.Log("Dead");
                return true;
            }*/

            return false;
        }

        public void LMDead()
        {
            lm.Dead();
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Siren"))
            {
                timer.remainingTime = 0f;
                CheckForDeath();
            }
            if (other.gameObject.CompareTag("Portal"))
            {
                lm.end = true;
                //timer.remainingTime = 0f;
                //CheckForDeath();
                //
            }

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Water"))
            {
                if(audioManager.CurrentBGM() == 1)
                {
                    audioManager.StopBGM();
                    audioManager.PlayBGM(0);
                }
                inWater = true;
                print("in Water");
            }

            if (other.tag == "Stone")
            {
                anim.Play("mine");

            }

        }

        void OnTriggerStay(Collider other)
        {
            if (other.tag == "FishingZone" && Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("FISHING PLEASE");
                anim.Play("Go fish");

            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Water"))
            {
                if(audioManager.CurrentBGM() == 0)
                {
                    audioManager.StopBGM();
                    audioManager.PlayBGM(1);
                }
                inWater = false;
                print("out Water");
            }
        }


        public bool CheckForFloat()
        {
            if (inWater == true)
            {
                Debug.Log("Float");
                return true;
            }
            return false;
        }

        public bool CheckForSwim()
        {
            if (inWater == true)
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

                if (direction.magnitude >= 0.1f)
                {
                    return true;

                }
            }
            return false;
        }

        public bool CheckForSprint()
        {
            if(sprint == true)
            {
                return true;
            }
            return false;
        }

        public void PlaySFX(int sfx)
        {
            //AudioManager.instance.PlaySFX(sfx);
        }


        public void OnJump(InputValue value)
        {

            if (value.isPressed)
            {
                Debug.Log("PRESSED BUTTON JUMP");
                velocity.y = Mathf.Sqrt(5 * -2 * gravity);
            }
        }

        public void OnInteract(InputValue value)
        {

            if (value.isPressed)
            {
                Debug.Log("PRESSED BUTTON INTERACT");
                //elocity.y = Mathf.Sqrt(5 * -2 * gravity);
                lm.interaction = true;
            }
        }

        public void OnSprint(InputValue value)
        {

            if (value.isPressed)
            {
                Debug.Log("PRESSED BUTTON SPRINT");
                //elocity.y = Mathf.Sqrt(5 * -2 * gravity);
                sprint = true;
            }
            else
            {
                sprint = false;
            }
        }

        public void OnEscape(InputValue value)
        {

            if (value.isPressed)
            {
                Debug.Log("PRESSED BUTTON ESCAPE");
                //elocity.y = Mathf.Sqrt(5 * -2 * gravity);

                if(escapeMenu == true)
                {
                    escapeMenu = false;
                }
                else
                {
                    escapeMenu = true;
                }
            }
            /*else
            {
                escapeMenu = false;
            }*/
        }


        /* public void OnInteract(InputValue value)
         {

             if (value.isPressed)
             {
                 Debug.Log("PRESSED BUTTON INTERACT");
                 lm.interaction = true;
             }
         }*/

    }



    }

