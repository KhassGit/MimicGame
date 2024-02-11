using Client;
using Game.Components;
using Game.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Systems.PlayerSystems {
    sealed class CameraFollowSystem : IEcsRunSystem
    {
        private EcsCustomInject<SceneService> _sceneService;

        private EcsFilterInject<Inc<PlayerTag, UnitComponent>> _playerFilter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter.Value)
            {
                ref var playerComponent = ref _playerFilter.Pools.Inc2.Get(entity);
                ref var playerTag = ref _playerFilter.Pools.Inc1.Get(entity);

                var cameraTransform = _sceneService.Value.mainCamera.transform;
                var playerPosition = playerComponent.GameObject.transform.position;

                cameraTransform.position = playerPosition + playerTag.FollowOffset;
                cameraTransform.LookAt(playerPosition);
            }
        }
    }
}