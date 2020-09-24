using UnityEngine;

namespace mariofake {

    public class Cloud : MonoBehaviour {

        [SerializeField] private float minMoveSpeed = default;
        [SerializeField] private float maxMoveSpeed = default;
        [SerializeField] private float minScale = default;
        [SerializeField] private float maxScale = default;

        private float moveSpeed;
        private float scale;

        private void Start() {
            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            scale = Random.Range(minScale, maxScale);

            transform.localScale = new Vector3(scale, scale, scale);
        }

        private void Update() {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        }

    }

}
