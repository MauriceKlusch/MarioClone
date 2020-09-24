using UnityEngine;

namespace mariofake
{

    public class BackgroundCloud : MonoBehaviour
    {
        [SerializeField] private float minSpeed = default;
        [SerializeField] private float maxSpeed = default;
        [SerializeField] private float minScale = default;
        [SerializeField] private float maxScale = default;

        private float movingSpeed;
        private float scale;

        private void Start()
        {
            movingSpeed = Random.Range(minSpeed, maxSpeed);
            scale = Random.Range(minScale, maxScale);

            transform.localScale = new Vector3(scale, scale, scale);
        }

        private void Update()
        {
            transform.position += new Vector3(movingSpeed * Time.deltaTime, 0f, 0f);
        }
    }
}