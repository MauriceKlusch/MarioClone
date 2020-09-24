using UnityEngine;
using UnityEngine.SceneManagement;

namespace mariofake {

    public class LevelComplete : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D collision) {   
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextLevel > 2) {
                SceneManager.LoadScene(0);
            }
            else {
                SceneManager.LoadScene(nextLevel);
            }
        }

    }
}
