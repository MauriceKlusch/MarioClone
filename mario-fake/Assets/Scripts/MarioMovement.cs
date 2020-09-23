using UnityEngine;

namespace mariofake {

	public class MarioMovement : MonoBehaviour {

		[Header("Horizontal Movement")]
		[SerializeField] private float moveSpeed = default;
		[Header("Jumping")]
		[SerializeField] private float jumpForce = default;
		[SerializeField] private float jumpingCheckOffset = default;

		[SerializeField] private float fallMultiplier = default;
		[SerializeField] private float lowJumpMultiplier = default;
		[Header("Ground detection")]
		[SerializeField] private float groundCheckRadius = default;
		[SerializeField] private LayerMask groundLayer = default;
		[SerializeField] private Transform marioBottom = default;

		private Rigidbody2D rb2D;
		private Animator animator;

		private enum HorizontalDirection { None, Left, Right }
		private HorizontalDirection horizontalDirection;

		private bool isGrounded;

		private bool isJumping;
		private bool readyToJump;
		private bool isHoldingJumpButton;
		private float timeToWaitBeforeJumpingCheck;

		private bool isSneaking;

		private void Start() {
			rb2D = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
		}

		void Update() {			

			Collider2D collider = Physics2D.OverlapCircle(marioBottom.position, groundCheckRadius, groundLayer);
			isGrounded = collider ? true : false;

			if (timeToWaitBeforeJumpingCheck > 0) timeToWaitBeforeJumpingCheck -= Time.deltaTime;

			if (isJumping && isGrounded && timeToWaitBeforeJumpingCheck <= 0) {
				isJumping = false;
				animator.SetBool("Jumping", false);
			}
			
			if (Input.GetKey(KeyCode.A) && !isSneaking) {
				horizontalDirection = HorizontalDirection.Left;
				animator.SetBool("Walking", true);
				transform.localScale = new Vector3(-1f, 1f, 1f);
			}
			else if (Input.GetKey(KeyCode.D) && !isSneaking) {
				horizontalDirection = HorizontalDirection.Right;
				animator.SetBool("Walking", true);
				transform.localScale = Vector3.one;
			}
			else {
				horizontalDirection = HorizontalDirection.None;
				animator.SetBool("Walking", false);
			}

			if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) {
				isHoldingJumpButton = true;
            }
			else {
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
				horizontalDirection = HorizontalDirection.None;
            }
			else {
				isSneaking = false;
				animator.SetBool("Sneaking", false);
			}						
		}

		private void LateUpdate() {

			switch (horizontalDirection) {
				case HorizontalDirection.Left:
					rb2D.velocity = new Vector2(-1f * moveSpeed, rb2D.velocity.y);
					break;
				case HorizontalDirection.Right:
					rb2D.velocity = new Vector2(1f * moveSpeed, rb2D.velocity.y);
					break;
				default:
					rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
					break;
			}

			if (readyToJump) {				
				rb2D.velocity += new Vector2(0f, jumpForce);
				readyToJump = false;
				isJumping = true;
			}

			// Let mario fall faster if he's falling
			if (rb2D.velocity.y < 0) {
				rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // Unity applies 1 per default
			}
			// Let mario jump higher depending on how long the jump button is pressed
			else if (rb2D.velocity.y > 0 && !isHoldingJumpButton) {
				Debug.Log("Called");
				rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; // Unity applies 1 per default
			}
		}

	}

}
