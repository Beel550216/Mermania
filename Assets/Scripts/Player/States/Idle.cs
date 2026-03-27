using UnityEngine;

namespace Player
{


    public class IdleState : State
    {

        //variables and methods specific to this state only

        public IdleState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }


        public override void Enter()
        {
            Debug.Log("ENTERED IDLE STATE");
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
                sm.ChangeState(player.swimState);
            }

            if (player.CheckForJump() == true)
            {
                sm.ChangeState(player.jumpState);
            }

            if ( player.CheckForMovement() == true )
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    sm.ChangeState(player.runState);
                }
                else
                {
                    sm.ChangeState(player.walkState);
                }

                
            }
        }
        public override void Exit()
        {
            //Debug.Log("EXIT IDLE STATE");
        }


        /*

      

        */


        /*
        public override void OnTriggerEnter(Collider other)
        {

        }
        public override void OnTriggerStay(Collider other)
        {

        }
        public override void OnTriggerExit(Collider other)
        {

        }
        */

    }

}