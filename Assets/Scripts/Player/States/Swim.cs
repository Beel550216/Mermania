using Player;
using UnityEngine;

namespace Player
{
    public class SwimState : State
    {
        private float speed = 15;
        float turnSmoothVelocity;
        public float turnSmoothTime = 0.1f;

        public SwimState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {

            player.anim.Play("SWIM");
            //currentPlayerModel = playerModelPrefab[2];  //2 = 3

             Debug.Log("ENTERED SWIM STATE");
        }
        public override void LogicUpdate()
        {
            //check float
            if (player.CheckForFloat() == false)
            {
                if (player.CheckForMovement() == false)
                {
                    player.gravity = -9.81f;
                    player.anim.SetBool("idle", true);
                    player.sm.ChangeState(player.idleState);

                }
                else
                {
                    player.anim.SetBool("walk", true);
                    sm.ChangeState(player.walkState);
                }
            }
            if (player.CheckForFloat() == true)
            {
                if (player.CheckForMovement() == false)
                {
                    player.sm.ChangeState(player.floatState);

                }
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + player.cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            player.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            player.controller.Move(moveDir.normalized * speed * Time.deltaTime);
            //Debug.Log("UPDATE DEATH STATE");
        }

        public override void PhysicsUpdate()
        {
            player.gravity = 2f;  //edit this too
            //player.rb.AddForce(player.transform.up * 50f); //edit this
        }

        public override void Exit()
        {
            //Debug.Log("EXIT DEATH STATE");
        }
    }

}