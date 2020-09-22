using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioMovement : MonoBehaviour {
	
	[SerializeField] private float moveSpeed = default;

    void Update() {
        if (Input.GetKey(KeyCode.A)) {
			transform.position = new Vector2(transform.position.x - (1f * moveSpeed * Time.deltaTime), 0f);
		}
		else if (Input.GetKey(KeyCode.D)) {
			transform.position = new Vector2(transform.position.x + (1f * moveSpeed * Time.deltaTime), 0f);
		}
    }
}
