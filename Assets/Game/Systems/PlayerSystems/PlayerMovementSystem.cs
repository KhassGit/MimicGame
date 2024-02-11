using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Systems.PlayerSystems {
    sealed class PlayerMovementSystem : IEcsRunSystem {    
        private EcsFilterInject<Inc<InputComponent, UnitComponent>> _filter;
        
        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var inputComponent = ref _filter.Pools.Inc1.Get(entity);
                ref var unitComponent = ref _filter.Pools.Inc2.Get(entity);
                var desiredVelocity = inputComponent.MoveInput.normalized * unitComponent.Speed;
                unitComponent.Rigidbody.velocity = desiredVelocity;
            }
        }
    }
}