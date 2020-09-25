using UnityEngine;
using UnityEngine.Events;

namespace mariofake {

    public class MarioHealth : MonoBehaviour {

        [SerializeField] private uint maxHealth = default;
        [SerializeField] private uint shrinkSizeAtHealth = default;
        [SerializeField] private Vector3 smallScale = default;
        [SerializeField] private float invincibleTime = default;
        [SerializeField] [Range(0f, 1f)] private float alphaValue = default;
        [SerializeField] private SpriteRenderer skinRenderer = default;
        [SerializeField] private AudioSource deathSound = default;

        public static event UnityAction OnMarioDeath;

        public uint currentHealth;
        private float invinsibleCounter;

        public float InvincibleTime { get => invincibleTime; }
        public bool IsInvincible { get => invinsibleCounter > 0; }

        private void Start() {
            currentHealth = maxHealth;
        }

        private void Update() {
            if (invinsibleCounter > 0) {
                invinsibleCounter -= Time.deltaTime;

                if (invinsibleCounter <= 0) {
                    RemoveInvincibility();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "Enemy" && invinsibleCounter <= 0 && MarioMovement.Instance && MarioMovement.Instance.Velocity.y >= 0) {
                currentHealth--;

                if (currentHealth == shrinkSizeAtHealth) {
                    transform.localScale = smallScale;
                }

                if (currentHealth == 0) {
                    OnMarioDeath?.Invoke();
                    Destroy(gameObject);
                }

                MakeInvincible(invincibleTime);
            }
        }

        private void MakeInvincible(float seconds) {
            invinsibleCounter = seconds;
            skinRenderer.color = new Color(skinRenderer.color.r, skinRenderer.color.g, skinRenderer.color.b, alphaValue);
        }

        private void RemoveInvincibility() {
            skinRenderer.color = new Color(skinRenderer.color.r, skinRenderer.color.g, skinRenderer.color.b, 1f);
        }

        public void KillMario() {
            deathSound.Play();
            OnMarioDeath?.Invoke();
            currentHealth = 0;
            Destroy(gameObject);
        }
    }

}
