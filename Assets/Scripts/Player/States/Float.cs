using Player;
using UnityEngine;

namespace Player
{
    public class FloatState : State
    {
        private float speed = 12;
        float turnSmoothVelocity;
        public float turnSmoothTime = 0.1f;

        public FloatState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }


        public override void Enter()
        {
            //currentPlayerModel = playerModelPrefab[2];  //2 = 3

             Debug.Log("ENTERED FLOAT STATE");
        }
        public override void LogicUpdate()
        {
            //check float
            if (player.CheckForFloat() == false)
            {
                if (player.CheckForMovement() == false)
                {
                    player.gravity = -9.81f;
                    player.sm.ChangeState(player.idleState);

                }
                else
                {
                    sm.ChangeState(player.walkState);
                }
            }

            if (player.CheckForFloat() == true)
            {
                if (player.CheckForSwim() == true)
                {
                    player.sm.ChangeState(player.swimState);

                }
            }

            if (Input.GetKey(KeyCode.Space) == true)
            {
                player.velocity.y = Mathf.Sqrt(5 * player.gravity);
                //Mathf.Sqrt(5 * -2 * player.gravity); //player.jumpHeight
            }
        }

        public override void PhysicsUpdate()
        {
            //player.gravity = 9f;  //edit this too
            //player.rb.AddForce(player.transform.up * 50f); //edit this
        }

        public override void Exit()
        {
            //Debug.Log("EXIT DEATH STATE");
        }
    }

}