using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace mariofake { 
public class DeathScreen : MonoBehaviour
{
    
        
       public void Respawn()
        {

            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
        public void TitleScreen()
        {
            SceneManager.LoadScene(0);
        }
            
    }
}
