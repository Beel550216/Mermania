using UnityEngine;

namespace Enemy
{

    public class SwimState : State
    {
        public SwimState(EnemyScript enemy, StateMachine sm) : base(enemy, sm)
        {
        }


        public override void Enter()
        {
            Debug.Log("ENTERED SWIM STATE");
        }
        public override void LogicUpdate()
        {

            //check death
            if (enemy.CheckForDeath() == true)
            {
                sm.ChangeState(enemy.deathState);
            }

            //check float
            if (enemy.CheckForIdle() == true)
            {
                sm.ChangeState(enemy.swimState);
            }

            if (enemy.CheckForAttack() == true)
            {
                sm.ChangeState(enemy.attackState);
            }

            /*if ( player.CheckForMovement() == true )
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    sm.ChangeState(player.runState);
                }
                else
                {
                    sm.ChangeState(player.walkState);
                }

                
            }*/
        }

        public override void PhysicsUpdate()
        {
            enemy.gravity = -9.81f;  //edit this too
            //player.rb.AddForce(player.transform.up * 100f); //edit this
        }


        public override void Exit()
        {
            //Debug.Log("EXIT IDLE STATE");
        }


    }

}