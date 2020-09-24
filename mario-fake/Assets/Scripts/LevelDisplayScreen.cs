using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace mariofake {

    public class LevelDisplayScreen : MonoBehaviour {

        [SerializeField] private Image image = default;
        [SerializeField] private Text text = default;
        [SerializeField] private float fadeSpeed = default;
        [SerializeField] private LevelTimer levelTimer = default;

        private int level;
        private float alphaValue;

        private bool fadeBack;
        private float counter = 3f;

        private void OnEnable() {
            Time.timeScale = 0f;

            levelTimer.gameObject.SetActive(false);

            level = SceneManager.GetActiveScene().buildIndex;

            alphaValue = 1f;

            image.color = new Color(image.color.r, image.color.g, image.color.b, alphaValue);

            text.text = "level " + level;
        }

        private void Update() {
            if (counter > 0) {
                counter -= Time.unscaledDeltaTime;
                if (counter <= 0) {
                    fadeBack = true;
                    Time.timeScale = 1f;
                    text.enabled = false;
                    levelTimer.gameObject.SetActive(true);
                }
            }

            if (fadeBack && !(alphaValue <= 0f)) {
                alphaValue -= fadeSpeed * Time.deltaTime;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alphaValue);
            }
        }
    }

}
