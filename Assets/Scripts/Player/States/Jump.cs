using Player;
using UnityEngine;

namespace Player
{
    public class JumpState : State
    {

    //variables and methods specific to this state only

    public JumpState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }


    public override void Enter()
    {
        Debug.Log("ENTERED JUMP STATE");
    }
    public override void LogicUpdate()
    {
        player.velocity.y = Mathf.Sqrt(5 * -2 * player.gravity); //player.jumpHeight

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

        if (player.CheckForMovement() == true)
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

        else
        {
            sm.ChangeState(player.idleState);
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
