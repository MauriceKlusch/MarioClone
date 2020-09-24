using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace mariofake { 
public class DeathScreen : MonoBehaviour
{
    
        int currentscene = Convert.ToInt32(SceneManager.GetActiveScene());
       public void Respawn()
        {
            SceneManager.LoadScene(currentscene);
        }
        public void TitleScreen()
        {
            SceneManager.LoadScene(0);
        }
            
    }
}
