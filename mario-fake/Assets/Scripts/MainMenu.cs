using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainaMenu = default;
    [SerializeField] private GameObject LevelaSelect = default;


    public void PlayGame()
    {
        LevelaSelect.SetActive(true);
        MainaMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
