using Client;
using Game.Components;
using Game.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Systems.PlayerSystems
{
    sealed class PlayerRotationSystem : IEcsRunSystem
    {
        private EcsCustomInject<SceneService> _sceneService;

        private EcsFilterInject<Inc<UnitComponent, InputComponent, PlayerTag>> _unitMovementFilter;

        public void Run(IEcsSystems systems)
        {
            // if (_sceneService.Value.isPaused) return;

            foreach (var entity in _unitMovementFilter.Value)
            {
                ref var playerComponent = ref _unitMovementFilter.Pools.Inc1.Get(entity);

                var playerPlane = new Plane(Vector3.up, playerComponent.Transform.position);
                var ray = _sceneService.Value.mainCamera.ScreenPointToRay(Input.mousePosition);
                if (!playerPlane.Raycast(ray, out var hitDistance)) continue;

                playerComponent.Transform.forward = ray.GetPoint(hitDistance) - playerComponent.Transform.position;
            }
        }
    }
}