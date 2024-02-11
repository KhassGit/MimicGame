using Client;
using Game.Components;
using Game.Services;
using Game.Systems.EnemySystems;
using Game.Systems.PlayerSystems;
using Game.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.UnityEditor;
using UnityEngine;

namespace Game
{
    sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;
        [SerializeField] private SceneService sceneService;
        [SerializeField] private UIController uiController;

        private void Start()
        {
            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            _updateSystems
                .Add(new PlayerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new EnemyInitSystem())
                .Add(new EnemyAISystem())
                .Add(new HealthUpdateSystem())
                .DelHere<DamageEvent>()
                .Add(new PlayerRotationSystem())
                .Add(new CameraZoomSystem())

#if UNITY_EDITOR
                .Add(new EcsWorldDebugSystem())
#endif
                .Inject(sceneService)
                .Inject(uiController)
                .Init();

            _fixedUpdateSystems
                .Add(new PlayerMovementSystem())
                .Add(new CameraFollowSystem())
                .Inject(sceneService)
                .Inject(uiController)
                .Init();
        }

        private void Update()
        {
            _updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        private void OnDestroy()
        {
            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;
            }

            if (_fixedUpdateSystems != null)
            {
                _fixedUpdateSystems.Destroy();
                _fixedUpdateSystems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}