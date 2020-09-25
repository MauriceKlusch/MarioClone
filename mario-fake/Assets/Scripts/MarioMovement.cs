using System;
using UnityEngine;

namespace mariofake {

    public class MarioMovement : MonoBehaviour {

        public static MarioMovement Instance;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                marioHealth = GetComponent<MarioHealth>();
            } else {
                Destroy(gameObject);
            }
        }

        private MarioHealth marioHealth;
        public bool IsInvincible { get => marioHealth.IsInvincible; }
        public bool IsJumping { get => isJumping; }

        [Header("Horizontal Movement")]
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private float accelerationSpeed = default;
        [SerializeField] private float looseMomentumSpeed = default;
        [Header("Jumping")]
        [SerializeField] private float jumpForce = default;
        [SerializeField] private float jumpingCheckOffset = default;
        [SerializeField] private float fallMultiplier = default;
        [SerializeField] private float lowJumpMultiplier = default;
        [SerializeField] private float stickBlockFallSpeed = default;
        [Header("Sneaking")]
        [SerializeField] private BoxCollider2D normalBoxCollider = default;
        [SerializeField] private BoxCollider2D normalBoxCollider2 = default;
        [SerializeField] private BoxCollider2D sneakingBoxCollider = default;
        [SerializeField] private BoxCollider2D sneakingBoxCollider2 = default;
        [Header("Ground detection")]
        [SerializeField] private Vector2 checkBoxSize = default;
        [SerializeField] private LayerMask groundLayer = default;
        [SerializeField] private Transform marioBottomLeft = default;
        [SerializeField] private Transform marioBottomRight = default;

        private Rigidbody2D rb2D;
        private Animator animator;

        private enum HorizontalDirection { None, Left, Right }
        private HorizontalDirection horizontalDirection;

        private bool isGrounded;

        private bool isJumping;
        private bool readyToJump;
        private bool isHoldingJumpButton;
        private float timeToWaitBeforeJumpingCheck;

        private bool stopVerticalVelocity;

        private bool isSneaking;

        private void Start() {
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            normalBoxCollider.enabled = true;
            normalBoxCollider2.enabled = true;
            sneakingBoxCollider.enabled = false;
            sneakingBoxCollider2.enabled = false;
        }

        void Update() {
            Collider2D collider = Physics2D.OverlapBox(marioBottomLeft.position, checkBoxSize, 0, groundLayer);
            Collider2D collider2 = Physics2D.OverlapBox(marioBottomRight.position, checkBoxSize, 0, groundLayer);

            isGrounded = collider || collider2 ? true : false;

            if (isGrounded) {
                stopVerticalVelocity = false;
            }

            if (timeToWaitBeforeJumpingCheck > 0) timeToWaitBeforeJumpingCheck -= Time.deltaTime;

            if (isJumping && isGrounded && timeToWaitBeforeJumpingCheck <= 0) {
                isJumping = false;
                stopVerticalVelocity = false;
                animator.SetBool("Jumping", false);
            }

            if (Input.GetKey(KeyCode.A) && !isSneaking) {
                horizontalDirection = HorizontalDirection.Left;
                animator.SetBool("Walking", true);
                if (transform.localScale.x > 0) {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            } else if (Input.GetKey(KeyCode.D) && !isSneaking) {
                horizontalDirection = HorizontalDirection.Right;
                animator.SetBool("Walking", true);
                if (transform.localScale.x < 0) {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            } else {
                horizontalDirection = HorizontalDirection.None;
                animator.SetBool("Walking", false);
            }

            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) {
                isHoldingJumpButton = true;
            } else {
                isHoldingJumpButton = false;
            }

            if (isHoldingJumpButton && isGrounded && !readyToJump && !isJumping) {
                timeToWaitBeforeJumpingCheck = jumpingCheckOffset;
                readyToJump = true;
                animator.SetBool("Jumping", true);
            }

            if (Input.GetKey(KeyCode.S) && !isJumping) {
                isSneaking = true;
                animator.SetBool("Sneaking", true);
                normalBoxCollider.enabled = false;
                normalBoxCollider2.enabled = false;
                sneakingBoxCollider.enabled = true;
                sneakingBoxCollider2.enabled = true;
                horizontalDirection = HorizontalDirection.None;
            } else {
                isSneaking = false;
                normalBoxCollider.enabled = true;
                normalBoxCollider2.enabled = true;
                sneakingBoxCollider.enabled = false;
                sneakingBoxCollider2.enabled = false;
                animator.SetBool("Sneaking", false);
            }
        }

        private void OnBecameInvisible() {
            marioHealth.KillMario();
        }

        private void LateUpdate() {

            switch (horizontalDirection) {

                case HorizontalDirection.Left:

                    if (rb2D.velocity.x > -moveSpeed) {
                        rb2D.velocity += new Vector2(-accelerationSpeed, 0f);
                    } else {
                        rb2D.velocity = new Vector2(-moveSpeed, rb2D.velocity.y);
                    }
                    break;

                case HorizontalDirection.Right:

                    if (rb2D.velocity.x < moveSpeed) {
                        rb2D.velocity += new Vector2(accelerationSpeed, 0f);
                    } else {
                        rb2D.velocity = new Vector2(moveSpeed, rb2D.velocity.y);
                    }
                    break;

                case HorizontalDirection.None:

                    if (rb2D.velocity.x < 0) {
                        rb2D.velocity += new Vector2((float)Math.Pow(10, -looseMomentumSpeed), 0f);
                    } else if (rb2D.velocity.x > 0) {
                        rb2D.velocity -= new Vector2((float)Math.Pow(10, -looseMomentumSpeed), 0f);
                    } else {
                        rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
                    }
                    break;
            }

            if (readyToJump) {
                rb2D.velocity += new Vector2(0f, jumpForce);
                readyToJump = false;
                isJumping = true;
            }

            if (stopVerticalVelocity) {
                rb2D.velocity = new Vector2(rb2D.velocity.x, -stickBlockFallSpeed);
            } else {
                // Let mario fall faster if he's falling
                if (rb2D.velocity.y < 0) {
                    rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // Unity applies 1 per default
                }
                // Let mario jump higher depending on how long the jump button is pressed
                else if (rb2D.velocity.y > 0 && !isHoldingJumpButton) {
                    rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; // Unity applies 1 per default
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Ground") && (isJumping || rb2D.velocity.y < -1)) {
                stopVerticalVelocity = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("Ground") && (isJumping || rb2D.velocity.y < -1)) {
                stopVerticalVelocity = false;
            }
        }
    }

}
