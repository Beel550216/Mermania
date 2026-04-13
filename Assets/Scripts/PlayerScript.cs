using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using System.Security.Cryptography;
using System.Threading;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Playables;

namespace Player
{

    public class PlayerScript : MonoBehaviour
    {
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
        public Rigidbody rb;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        float turnSmoothVelocity;
        public float turnSmoothTime = 0.1f;

        public LevelManager lm;

        public StateMachine sm;

        public AudioManager audioManager;

        public Animator anim;

        public IdleState idleState;
        public WalkState walkState;
        public RunState runState;
        public JumpState jumpState;
        public FloatState floatState;
        public SwimState swimState;
        public DeathState deathState;

        private void Start()
        {
            sm = gameObject.AddComponent<StateMachine>();
            rb  = GetComponent<Rigidbody>();
            anim  = GetComponent<Animator>();
            audioManager = GetComponent<AudioManager>();

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


        }
        private void Awake()
        {
            lm = GetComponent<LevelManager>();
        }

        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }

        // Update is called once per frame
        void Update()
        {
            velocity.y += gravity * Time.deltaTime; //gravity
            controller.Move(velocity * Time.deltaTime);

            sm.CurrentState.LogicUpdate();

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
            if (timer.remainingTime <= 0.1f)
            {
                Debug.Log("Dead");
                return true;

            }
            return false;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Water"))
            {
                inWater = true;
                print("in Water");
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Water"))
            {
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

        public void PlaySFX(int sfx)
        {
            //AudioManager.instance.PlaySFX(sfx);
        }


    }



    }

