using UnityEngine;

namespace mariofake {

    public class CloudSpawner : MonoBehaviour {

        [Header("Cloud")]
        [SerializeField] private Cloud cloud = default;
        [Header("Settings")]
        [SerializeField] private Transform minYTransform = default;
        [SerializeField] private Transform maxYTransform = default;
        [SerializeField] private float minSpawningInterval = default;
        [SerializeField] private float maxSpawningInterval = default;

        private float yPos;
        private float timer;

        private void Start() {
            yPos = Random.Range(minYTransform.position.y, maxYTransform.position.y);
            timer = Random.Range(minSpawningInterval, maxSpawningInterval);
        }

        private void Update() {
            if (timer > 0) {

                timer -= Time.deltaTime;
                if (timer <= 0) {
                    Instantiate(cloud, new Vector3(transform.position.x, yPos, 0f), Quaternion.identity);

                    yPos = Random.Range(minYTransform.position.y, maxYTransform.position.y);
                    timer = Random.Range(minSpawningInterval, maxSpawningInterval);
                }
            } 
        }

    }


}