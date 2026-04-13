using Player;
using UnityEngine;

namespace Player
{
    public class WalkState : State
    {
        private float speed = 8;
        float turnSmoothVelocity;
        public float turnSmoothTime = 0.1f;

        public WalkState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }


        public override void Enter()
        {
            player.PlaySFX(0);
            //currentPlayerModel = playerModelPrefab[2];  //2 = 3
            player.anim.SetBool("Walk", true);

            Debug.Log("ENTERED WALK STATE");
        }
        public override void LogicUpdate()
        {
            //check death
            if (player.CheckForDeath() == true)
            {
                sm.ChangeState(player.deathState);
            }

            //check float
            if (player.CheckForFloat() == true)
            {
                sm.ChangeState(player.floatState);
            }

            if (player.CheckForSwim() == true)
            {
                sm.ChangeState(player.swimState);
            }

            if ( player.CheckForMovement() == false )
            {
                player.sm.ChangeState(player.idleState);

            }

            if(player.CheckForMovement() == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    sm.ChangeState(player.runState);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    sm.ChangeState(player.jumpState);
                }
                else
                {
                    float horizontal = Input.GetAxisRaw("Horizontal");
                    float vertical = Input.GetAxisRaw("Vertical");
                    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + player.cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    player.transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    player.controller.Move(moveDir.normalized * speed * Time.deltaTime);
                }

            }
        }

        public override void PhysicsUpdate()
        {
            player.gravity = -9.81f;  //edit this too
            //player.rb.AddForce(player.transform.up * 100f); //edit this
        }

        public override void Exit()
        {
            player.anim.SetBool("Walk", false);
            //Debug.Log("EXIT DEATH STATE");
        }

        public override void PlaySFX(int sfx)
        {
            //AudioManager.PlaySFX(sfx);
            base.PlaySFX(sfx);
        }

        public override void StopSFX(int sfx)
        {
            base.StopSFX(sfx);
        }

    }

}