using UnityEngine;

namespace Game.Services
{
    public class SceneService : MonoBehaviour
    {
        public Transform playerSpawnPoint;
        public GameObject playerPrefab;
        public float playerMoveSpeed = 10;
        public Camera mainCamera;
        public Vector3 cameraFollowOffset = new(0, 20, -10);
        public Transform enemySpawnPoint;
        public GameObject enemyPrefab;
        public float enemyMoveSpeed = 8;
        public float zoomSpeed = 10f;
        public float minZoom = 3f;
        public float maxZoom = 20f;
    }
}
