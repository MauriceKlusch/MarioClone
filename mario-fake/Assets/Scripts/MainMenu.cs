using UnityEngine;

namespace mariofake {

    public class MainMenu : MonoBehaviour {
        [SerializeField] private GameObject MainaMenu = default;
        [SerializeField] private GameObject LevelaSelect = default;


        public void PlayGame() {
            LevelaSelect.SetActive(true);
            MainaMenu.SetActive(false);
        }
        public void QuitGame() {
            Application.Quit();
        }
    }
}
