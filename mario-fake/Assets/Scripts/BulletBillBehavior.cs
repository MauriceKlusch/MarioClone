using UnityEngine;

namespace mariofake {

    public class BulletBillBehavior : MonoBehaviour {

        [SerializeField] private float speed = default;
        //[SerializeField] private BoxCollider2D physicsCollider = default;

        private float activateColliderAfter;

        private void Update() {
            transform.position = new Vector3(transform.position.x + (-speed * Time.deltaTime), transform.position.y, transform.position.z);

            if (activateColliderAfter > 0) {
                activateColliderAfter -= Time.deltaTime;

                if (activateColliderAfter <= 0) {
                    //physicsCollider.enabled = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            //physicsCollider.enabled = false;

            activateColliderAfter = MarioMovement.Instance.InvincibleTime;
        }
    }

}
