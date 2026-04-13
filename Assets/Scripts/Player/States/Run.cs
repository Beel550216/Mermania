using Player;
using UnityEngine;
//using static Unity.Cinemachine.InputAxisControllerBase<T>;

public class RunState : State
{
    private float speed = 22;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    //variables just for this state here

    public RunState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }


    public override void Enter()
    {
        player.anim.SetBool("Sprint", true);

        Debug.Log("ENTERED RUN STATE");

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

        if (player.CheckForMovement() == true)
        {
            if (Input.GetKey(KeyCode.LeftShift) == false)
            {
                sm.ChangeState(player.walkState);
            }
            else
            {

                //player.vfx.SetFloat("spawnRate", 0f);


                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + player.cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                player.transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                player.controller.Move(moveDir.normalized * 20f * Time.deltaTime);
            }

        }
    }
    public override void Exit()
    {
        player.anim.SetBool("Sprint", false);

        Debug.Log("EXIT RUN STATE");
        //GetNextState();
    }

    

   
}

