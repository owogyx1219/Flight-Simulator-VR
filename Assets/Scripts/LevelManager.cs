using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public Transform mainMenu, tutorialPage;
    public void loadScene(string name)
    {
        //SceneManager.LoadScene(name);
        Application.LoadLevel(name);
    }

    public void loadTutorial(string name)
    {
        Application.LoadLevel(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TutorialPage (bool clicked)
    {
        if(clicked == true)
        {
            tutorialPage.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
    }

    public void returnHomePage(bool clicked)
    {
        if(clicked == true)
        {
            tutorialPage.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
        }
    }
}
