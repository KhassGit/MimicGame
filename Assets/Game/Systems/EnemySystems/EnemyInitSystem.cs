using Client;
using Game.Components;
using Game.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Systems.EnemySystems
{
    public sealed class EnemyInitSystem: IEcsInitSystem
    {
        private EcsWorldInject _world;
        private EcsCustomInject<SceneService> _sceneService;
        private EcsPoolInject<UnitComponent> _unitComponentPool;
        private EcsPoolInject<EnemyTag> _enemyTagPool;
        
        public void Init (IEcsSystems systems)
        {
            var enemyEntity = systems.GetWorld().NewEntity();
            ref var unitComponent=ref _unitComponentPool.Value.Add(enemyEntity);
            _enemyTagPool.Value.Add(enemyEntity);
            var enemyGameObject = Object
                .Instantiate(_sceneService.Value.enemyPrefab, _sceneService.Value.enemySpawnPoint).GameObject();

            unitComponent.GameObject = enemyGameObject;
            unitComponent.Transform = enemyGameObject.transform;
            unitComponent.Speed = _sceneService.Value.enemyMoveSpeed;
            unitComponent.Rigidbody = enemyGameObject.GetComponent<Rigidbody>();
            unitComponent.HealthPoints = 100;
        }
    }
}