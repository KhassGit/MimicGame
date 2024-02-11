using Client;
using Game.Components;
using Game.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Systems.PlayerSystems
{
    sealed class CameraZoomSystem : IEcsRunSystem
    {
        private EcsCustomInject<SceneService> _sceneService;

        private EcsFilterInject<Inc<InputComponent, PlayerTag>> _inputFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _inputFilter.Value)
            {
                ref var inputComponent = ref _inputFilter.Pools.Inc1.Get(entity);
                ref var playerTag = ref _inputFilter.Pools.Inc2.Get(entity);

                var totalScrollDelta = inputComponent.ScrollInput;

                var zoomAmount = -totalScrollDelta * _sceneService.Value.zoomSpeed;

                var newFollowOffset = new Vector3
                {
                    x = playerTag.FollowOffset.x,
                    y = Mathf.Clamp(
                        playerTag.FollowOffset.y + zoomAmount,
                        _sceneService.Value.minZoom,
                        _sceneService.Value.maxZoom),
                    z = playerTag.FollowOffset.z,
                };

                playerTag.FollowOffset = newFollowOffset;
            }
        }
    }
}