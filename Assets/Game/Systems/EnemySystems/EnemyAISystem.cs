using Client;
using Game.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Systems.EnemySystems
{
    public class EnemyAISystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<UnitComponent, PlayerTag>> _playerFilter;
        private EcsFilterInject<Inc<UnitComponent, EnemyTag>> _enemyFilter;

        private readonly float _stoppingDistance = 2f; // Adjust this value as needed

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter.Value)
            {
                ref var playerComponent = ref _playerFilter.Pools.Inc1.Get(playerEntity);
                foreach (var enemyEntity in _enemyFilter.Value)
                {
                    ref var enemyComponent = ref _enemyFilter.Pools.Inc1.Get(enemyEntity);

                    // Calculate direction from enemy to player
                    Vector3 direction = playerComponent.GameObject.transform.position -
                                        enemyComponent.GameObject.transform.position;
                    float distanceToPlayer = direction.magnitude;

                    // Check if the enemy is within the stopping distance
                    if (distanceToPlayer > _stoppingDistance)
                    {
                        direction.Normalize();

                        // Move the enemy towards the player
                        MoveEnemy(enemyComponent, direction);
                    }
                    else
                    {
                        // Stop the enemy
                        StopEnemy(enemyComponent);
                    }
                }
            }
        }

        private void MoveEnemy(UnitComponent enemyComponent, Vector3 direction)
        {
            // Move the enemy using Rigidbody if available
            if (enemyComponent.Rigidbody != null)
            {
                enemyComponent.Rigidbody.velocity = direction * enemyComponent.Speed;
            }
            else // Move the enemy using Transform if Rigidbody is not available
            {
                enemyComponent.GameObject.transform.position += direction * enemyComponent.Speed * Time.deltaTime;
            }
        }

        private void StopEnemy(UnitComponent enemyComponent)
        {
            // Stop the enemy's movement by setting velocity to zero if Rigidbody is available
            if (enemyComponent.Rigidbody != null)
            {
                enemyComponent.Rigidbody.velocity = Vector3.zero;
            }
        }
    }
}