using UnityEngine;
using System.Collections.Generic;

namespace Enemy
{
    public abstract class State
    {
        protected EnemyScript enemy;
        protected StateMachine sm;

        protected AudioManager audioManager;

        //AudioManager theInstance = FindObjectOfType<AudioManager>();


        // base constructor
        protected State(EnemyScript enemy, StateMachine sm)
        {
            this.enemy = enemy;
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