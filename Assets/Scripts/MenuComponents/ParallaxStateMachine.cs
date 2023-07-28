using System;
using FiniteStateMachine;
using Input;
using Skybox;
using Unity.VisualScripting;
using UnityEngine;

namespace MenuComponents
{
    public class ParallaxStateMachine : StateMachine<ParallaxState>
    {
        private IPlayerInput _playerInput;
        private Transform _target;
        private Transform _text;
        private SkyboxSettings _skyboxSettings;

        public Action GameStartAction;

        public ParallaxStateMachine(IPlayerInput playerInput, Transform target, Transform text,
            SkyboxSettings skyboxSettings)
        {
            _playerInput = playerInput;
            _target = target;
            _text = text;
            _skyboxSettings = skyboxSettings;
            InitializeStates();
        }
        
        public void Update()
        {
            _currentState?.Update();
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }

        protected override void ConfigureState(ParallaxState state)
        {
            state.StateMachine = this;
            state.Input = _playerInput;
            state.Target = _target;
            state.Text = _text;
            state.SkyboxSettings = _skyboxSettings;
        }
    }
}