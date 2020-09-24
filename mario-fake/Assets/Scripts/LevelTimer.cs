using UnityEngine;
using UnityEngine.UI;

namespace mariofake {
    public class LevelTimer : MonoBehaviour {

        [SerializeField] private float levelTime = 120f;
        [SerializeField] private Text timeText = default;
        [SerializeField] private MarioHealth marioHealth = default;

        // Update is called once per frame
        void Update() {
            if (levelTime < 0) {
                timeText.text = "00:00";

                if (marioHealth != null)
                    marioHealth.KillMario();
            } else {
                levelTime -= Time.deltaTime;
                string minutes = ((int)levelTime / 60).ToString("00");
                string seconds = ((int)(levelTime % 60)).ToString("00");
                timeText.text = minutes + ":" + seconds;
            }
        }
    }
}
