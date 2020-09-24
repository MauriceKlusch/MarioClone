using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    [SerializeField] private GameObject MainbMenu = default;
    [SerializeField] private GameObject LevelbSelect = default;

    public void Level1()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
    }
    public void Level3()
    {
        SceneManager.LoadScene(3);
    }
    public void BackButton()
    {
        LevelbSelect.SetActive(false);
        MainbMenu.SetActive(true);
    }
}
