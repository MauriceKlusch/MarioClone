using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace mariofake {

    public class LevelComplete : MonoBehaviour {

        private bool triggered;

        [SerializeField] private AudioSource levelCompleteSound = default;
        [SerializeField] private AudioSource mapLevel = default;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!triggered)
                StartCoroutine(Func());
        }

        private IEnumerator Func() {
            triggered = true;
            mapLevel.Stop();
            levelCompleteSound.Play();

            yield return new WaitUntil(() => levelCompleteSound.isPlaying == false);

            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextLevel > 3) {
                SceneManager.LoadScene(0);
            } else {
                SceneManager.LoadScene(nextLevel);
            }
        }

    }
}
