using Client;
using Game.Components;
using Game.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Systems.PlayerSystems {
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorldInject _world;
        private EcsCustomInject<SceneService> _sceneService;
        private EcsPoolInject<InputComponent> _inputComponentPool;
        private EcsPoolInject<UnitComponent> _unitComponentPool;
        private EcsPoolInject<PlayerTag> _playerTagPool;
        
        public void Init (IEcsSystems systems)
        {
            var playerEntity = systems.GetWorld().NewEntity();
            _inputComponentPool.Value.Add(playerEntity);
            ref var playerTag = ref _playerTagPool.Value.Add(playerEntity);
            playerTag.FollowOffset = _sceneService.Value.cameraFollowOffset;
            
            var playerGameObject = Object
                .Instantiate(_sceneService.Value.playerPrefab, _sceneService.Value.playerSpawnPoint).GameObject();

            ref var unitComponent=ref _unitComponentPool.Value.Add(playerEntity);
            unitComponent.GameObject = playerGameObject;
            unitComponent.Transform = playerGameObject.transform;
            unitComponent.Speed = _sceneService.Value.playerMoveSpeed;
            unitComponent.Rigidbody = playerGameObject.GetComponent<Rigidbody>();
            unitComponent.HealthPoints = 100;
        }
    }
}