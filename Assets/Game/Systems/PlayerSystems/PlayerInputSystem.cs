using Client;
using Game.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Systems.PlayerSystems {
    sealed class PlayerInputSystem : IEcsRunSystem {  
        private EcsFilterInject<Inc<InputComponent, UnitComponent, PlayerTag>> _filter;
        
        public void Run (IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var inputComponent = ref _filter.Pools.Inc1.Get(entity);
                Movement(ref inputComponent);
                Zoom(ref inputComponent);
            }
        }

        private static void Movement(ref InputComponent inputComponent)
        {
            inputComponent.MoveInput =
                new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
        private static void Zoom(ref InputComponent inputComponent)
        {
            inputComponent.ScrollInput = Input.GetAxis("Mouse ScrollWheel");
        }
    }
}