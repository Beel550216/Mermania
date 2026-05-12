using Player;
using UnityEngine;

namespace Player
{ 
    public class DeathState: State
    {
        public LevelManager lm;

        public DeathState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

       
        public override void Enter()
        {
            /*if(lm.killedPlayer)
            {
                player.anim.Play("Dead2");
            }
            else
            {*/
                player.anim.Play("Dead");
            //}

            //currentPlayerModel = playerModelPrefab[2];  //2 = 3

            Debug.Log("ENTERED DEATH STATE");
        }
        public override void LogicUpdate()
        {
            Debug.Log("UPDATE DEATH STATE");
        }
        public override void Exit()
        {
            Debug.Log("EXIT DEATH STATE");
        }
    }

}