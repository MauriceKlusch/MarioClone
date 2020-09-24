using UnityEngine;

namespace mariofake {

    public class GoombaBehavior : MonoBehaviour {

        [Header("Movement")]
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private HorizontalDirection startDirection = default;
        [Header("On Goomba Death")]
        [SerializeField] private float waitBeforeDespawned = default;
        [Header("Sprites")]
        [SerializeField] private Sprite leftDeadSprite = default;
        [SerializeField] private Sprite rightDeadSprite = default;

        private enum HorizontalDirection { None, Left, Right }
        private HorizontalDirection horizontalDirection;

        private GameObject[] children;

        private Rigidbody2D rb2D;
        private Animator animator;
        private SpriteRenderer skinRenderer;
        private BoxCollider2D mainPhysicsCollider;

        private void Start() {
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            skinRenderer = GetComponent<SpriteRenderer>();
            mainPhysicsCollider = GetComponent<BoxCollider2D>();

            horizontalDirection = startDirection;

            children = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++) {
                children[i] = transform.GetChild(i).gameObject;
            }
              
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

            // Wenn mario goomba auf den Kopf springt (goomba dead)
            else if (collision.CompareTag("Player") && MarioMovement.Instance && !MarioMovement.Instance.IsInvincible && MarioMovement.Instance.IsJumping) {

                Destroy(rb2D);
                mainPhysicsCollider.enabled = false;

                Destroy(animator);

                foreach (GameObject g in children) {
                    g.SetActive(false);
                }              

                ChangeToDeadSprite();

                horizontalDirection = HorizontalDirection.None;

                Destroy(gameObject, waitBeforeDespawned);
            }
        }

        private HorizontalDirection GetOppositeDirection(HorizontalDirection dir) {
            if (dir == HorizontalDirection.Left) return HorizontalDirection.Right;
            else return HorizontalDirection.Left;
        }

        private void ChangeToDeadSprite() {
            if (horizontalDirection == HorizontalDirection.Left) skinRenderer.sprite = leftDeadSprite;
            else skinRenderer.sprite = rightDeadSprite;
        }
    }

}