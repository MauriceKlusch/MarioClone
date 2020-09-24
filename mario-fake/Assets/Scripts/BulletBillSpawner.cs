using UnityEngine;

namespace  mariofake {

    public class BulletBillSpawner : MonoBehaviour {

        [SerializeField] private float minSpawnInterval = default;
        [SerializeField] private float maxSpawnInterval = default;
        [SerializeField] private BulletBillBehavior bulletBill = default;

        private float spawnIntervalCounter;

        private void Start() {
            spawnIntervalCounter = Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        private void Update() {
            if (spawnIntervalCounter > 0) {
                spawnIntervalCounter -= Time.deltaTime;

                if (spawnIntervalCounter <= 0) {
                    Instantiate(bulletBill, transform.position, Quaternion.identity);
                    spawnIntervalCounter = Random.Range(minSpawnInterval, maxSpawnInterval);
                }
            }
        }

    }

}
