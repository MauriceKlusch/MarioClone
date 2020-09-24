using UnityEngine;

namespace mariofake {

    public class GoombaBehavior : MonoBehaviour {

        [Header("Movement")]
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private HorizontalDirection startDirection = default;

        private enum HorizontalDirection { Left, Right }
        private HorizontalDirection horizontalDirection;

        private Rigidbody2D rb2D;

        private void Start() {
            rb2D = GetComponent<Rigidbody2D>();
            horizontalDirection = startDirection;
        }

        private void FixedUpdate() {
            switch (horizontalDirection) {
                case HorizontalDirection.Left:
                    rb2D.velocity = new Vector2(-1f * moveSpeed, rb2D.velocity.y);
                    break;
                case HorizontalDirection.Right:
                    rb2D.velocity = new Vector2(1f * moveSpeed, rb2D.velocity.y);
                    break;
            }           
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Ground")) {
                horizontalDirection = GetOppositeDirection(horizontalDirection);
            }
        }

        private HorizontalDirection GetOppositeDirection(HorizontalDirection dir) {
            if (dir == HorizontalDirection.Left) return HorizontalDirection.Right;
            else return HorizontalDirection.Left;
        }
    }

}