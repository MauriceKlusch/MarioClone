using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace mariofake {
    public class LevelTimer : MonoBehaviour {
        [SerializeField] private float levelTime = 120f;
        [SerializeField] private Text timeText = default;
        [SerializeField] private MarioHealth marioHealth = default;

        // Start is called before the first frame update
        void Start() {

        }

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
