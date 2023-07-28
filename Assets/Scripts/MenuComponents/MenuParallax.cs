using System;
using Input;
using Skybox;
using UnityEngine;
using Zenject;

namespace MenuComponents
{
    public class MenuParallax : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform text;
        
        
        private IPlayerInput _playerInput;
        private SkyboxSettings _skyboxSettings;
        
        private ParallaxStateMachine _stateMachine;
        
        [Inject]
        private void Construct(IPlayerInput playerInput, SkyboxSettings skyboxSettings)
        {
            _playerInput = playerInput;
            _skyboxSettings = skyboxSettings;
            _stateMachine = new ParallaxStateMachine(_playerInput, target, text,_skyboxSettings);
        }

        public void ToIdle()
        {
            _stateMachine.ChangeState<IdleParallaxState>();
        }

        public void ToGameTransition(Action starGame)
        {
            _stateMachine.GameStartAction = starGame;
            _stateMachine.ChangeState<ToGameState>();
        }

        private void Start()
        {
            _stateMachine.ChangeState<IdleParallaxState>();
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
    }
}