using UnityEngine;

namespace mariofake {

    public class MapBorder : MonoBehaviour {

        [SerializeField] private MarioHealth marioHealth = default;

        private void OnTriggerEnter2D(Collider2D collision) {
            marioHealth.KillMario();
        }
    }

}