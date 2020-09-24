using UnityEngine;

namespace mariofake {

    public class CameraController : MonoBehaviour {

        [SerializeField] private Transform targetToFollow = default;

        private void Update() {
            if (targetToFollow) {
                transform.position = new Vector3(targetToFollow.position.x, targetToFollow.position.y, transform.position.z);
            }           
        }

    }

}
