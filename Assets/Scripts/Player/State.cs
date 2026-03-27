
using UnityEngine;
using System.Collections.Generic;

namespace Player
{
    public abstract class State
    {
        protected PlayerScript player;
        protected StateMachine sm;

        protected AudioManager audioManager;

        //AudioManager theInstance = FindObjectOfType<AudioManager>();


        // base constructor
        protected State(PlayerScript player, StateMachine sm)
        {
            this.player = player;
            this.sm = sm;
        }

        // These methods are common to all states
        public virtual void Enter()
        {
            //Debug.Log("This is base.enter");
        }

        public virtual void HandleInput()
        {
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void PlaySFX(int sfx)
        {
            //AudioManager audioManager = FindObjectOfType<AudioManager>();

            audioManager.PlaySFX(sfx);
        }

        public virtual void StopSFX(int sfx)
        {
            audioManager.StopSFX(sfx);
        }

    }
}