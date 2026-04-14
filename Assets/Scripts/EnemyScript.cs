using UnityEngine;

namespace Enemy
{
    public class EnemyScript : MonoBehaviour
    {
        public float speed = 15;
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

        public IdleState idleState;
        public AttackState attackState;
        public SwimState swimState;
        public DeathState deathState;

        public bool dead = false;

        GameObject player;
        public Transform target;
        public float range = 15f;
        
        void Start()
        {
            sm = gameObject.AddComponent<StateMachine>();
            rb  = GetComponent<Rigidbody>();
            audioManager = GetComponent<AudioManager>();

            player = GameObject.FindGameObjectWithTag("Player");
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            idleState = new IdleState(this, sm);
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

        void Update()
        {
            Debug.Log("ENEMY DISTANCE " + Vector3.Distance(transform.position, player.transform.position));
            //velocity.y += gravity * Time.deltaTime; //gravity
            //controller.Move(velocity * Time.deltaTime);

            /*if(IsPlayerInRange(range) && sm.CurrentState != deathState)
            {
                //sm.CurrentState = EnemyState.Follow;
            }
            else if(!IsPlayerInRange(range)&& sm.CurrentState != deathState)                        //
            {
                Debug.Log("Not Dead");
                //sm.CurrentState = EnemyState.Wander;
            }*/

            //Follow();

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            sm.CurrentState.LogicUpdate();
        }

        private bool IsPlayerInRange(float range)
        {
            return Vector3.Distance(transform.position, player.transform.position) <= range;  //
        }

        public void Follow()
        {
            /*if (transform.position.x > target.position.x)
            {
                //target is left
                transform.localScale = new Vector2(-1, 1);
                rb.linearVelocity = new Vector2(-speed, 0f);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
            }
            else if (transform.position.x < target.position.x)
            {
                //target is right
                transform.localScale = new Vector2(1, 1);
                rb.linearVelocity = new Vector2(speed, 0f);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
            }*/

        }


        public bool CheckForIdle()
        {

            if(!IsPlayerInRange(range)&& sm.CurrentState != deathState)                       //CHANGE THIS!!!
            {
                Debug.Log("Not Dead");
                //sm.CurrentState = EnemyState.Wander;
                return true;
            }
            return false;
        }

        public bool CheckForDeath()
        {
            if (dead == true)
            {
                Debug.Log("Dead");
                return true;

            }
            return false;
        }

        public bool CheckForSwim()
        {
            if(IsPlayerInRange(range) && sm.CurrentState != deathState) //if (dead == true)  //change this to like, if the player is in a range/distance --> then swim towards (will need to use NavMeshAgent)
            {
                return true;
            }
            return false;
        }

        public bool CheckForAttack()
        {
            if (dead == true)
            {
                Debug.Log("Dead");
                return true;

            }
            return false;
        }
    }

}