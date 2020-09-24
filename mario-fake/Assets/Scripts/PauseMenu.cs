using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace mariofake {
    public class PauseMenu : MonoBehaviour {

        [SerializeField] private GameObject pauseMenu = default;

        public static bool isPaused = false;

        private void Start() {
            pauseMenu.SetActive(false);
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
                if (isPaused)
                    Resume();
                else
                    Pause();
        }

        void Pause() {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }

        public void Resume() {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }

        public void GoToMainMenu() {
            SceneManager.LoadScene(0);
        }
    }
}
