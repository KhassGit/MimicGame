using Client;
using Game.Components;
using Game.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Systems.PlayerSystems
{
    public class HealthUpdateSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<UnitComponent, PlayerTag>> _playerFilter;
        private EcsCustomInject<UIController> _uiController;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter.Value)
            {
                ref var playerComponent = ref _playerFilter.Pools.Inc1.Get(playerEntity);
                _uiController.Value.SetHealth(playerComponent.HealthPoints);
            }
        }
    }
}