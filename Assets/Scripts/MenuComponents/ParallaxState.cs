using FiniteStateMachine;
using Input;
using Skybox;
using UnityEngine;

namespace MenuComponents
{
    public abstract class ParallaxState : IState
    {
        public IPlayerInput Input;
        public Transform Target;
        public SkyboxSettings SkyboxSettings;
        public ParallaxStateMachine StateMachine;
        public Transform Text;

        public virtual void Enter()
        {
            
        }
        
        public virtual void Update() { }
        
        public virtual void FixedUpdate() { }

        public virtual void Exit() { }
    }
}