using UnityEngine;
//using static Unity.Cinemachine.InputAxisControllerBase<T>;

public class RunState : BaseState<PlayerStateMachine.PlayerState>
{
    public RunState(PlayerStateMachine.PlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("ENTERED RUN STATE");
    }
    public override void UpdateState()
    {
        Debug.Log("UPDATE RUN STATE");

        /*float horizontal = Input.GetAxisRaw("Horizontal");
         float vertical = Input.GetAxisRaw("Vertical");
         Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

         float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
         float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
         transform.rotation = Quaternion.Euler(0f, angle, 0f);

         Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
         controller.Move(moveDir.normalized * speed * Time.deltaTime); */
    }
    public override void ExitState()
    {
        Debug.Log("EXIT RUN STATE");
        //GetNextState();
    }

    public override PlayerStateMachine.PlayerState GetNextState()
    {
        Debug.Log("Getting next state");
        Debug.Log(StateKey);
        return StateKey;
    }

    public override void CheckForRun()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {

    }
    public override void OnTriggerStay(Collider other)
    {

    }
    public override void OnTriggerExit(Collider other)
    {

    }
}

