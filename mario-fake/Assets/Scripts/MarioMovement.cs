using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioMovement : MonoBehaviour {
	
	[SerializeField] private float moveSpeed = default;
	
	private Animator animator;
	
	private void Start() {
		animator = GetComponent<Animator>();
	}

    void Update() {
        if (Input.GetKey(KeyCode.A)) {
			transform.position = new Vector2(transform.position.x - (1f * moveSpeed * Time.deltaTime), 0f);
			animator.SetBool("IsWalkingLeft", true);
			animator.SetBool("IsWalkingRight", false);
		}
		else if (Input.GetKey(KeyCode.D)) {
			transform.position = new Vector2(transform.position.x + (1f * moveSpeed * Time.deltaTime), 0f);
			animator.SetBool("IsWalkingLeft", false);
			animator.SetBool("IsWalkingRight", true);
		}
		else {
			animator.SetBool("IsWalkingRight", false);
			animator.SetBool("IsWalkingLeft", false);
		}
    }
}
