using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace mariofake {
    public class DeathScreen : MonoBehaviour {

        [SerializeField] private GameObject deathScreen = default;

        private void Start() {
            MarioHealth.OnMarioDeath += ShowDeathScreen;
        }

        public void ShowDeathScreen() {
            Time.timeScale = 0;

            if (deathScreen != null)
                deathScreen.SetActive(true);
        }

        public void Respawn() {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void TitleScreen() {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

    }
}
