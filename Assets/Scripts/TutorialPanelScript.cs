using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialPanelScript : MonoBehaviour
{
    public Button exitButton;
    public Button simulateButton;
    public Button tutorialButton;
    public Button tutorial_1;
    public Button exitToMainMenuButton;
    public GameObject tutorialPanel;

    // public GameObject loadingPanel;

    private void Start()
    {
        tutorialPanel.SetActive(false);
        //loadingPanel.SetActive(false);
    }

    private void Update()
    {
        exitButton.onClick.AddListener(DisablePanel);
        tutorialButton.onClick.AddListener(enablePanel);
        simulateButton.onClick.AddListener(LoadScene);
        tutorial_1.onClick.AddListener(OpenTutorial1);
        exitToMainMenuButton.onClick.AddListener(ExitToMainMenu);
    }

    private void DisablePanel()
    {
        tutorialPanel.SetActive(false);
    }

    private void enablePanel()
    {
        tutorialPanel.SetActive(true);
    }

    private void OpenTutorial1()
    {
        SceneManager.LoadScene("LoadingScene2");
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void ExitToMainMenu()
    {
        // Load the "MainMenu" scene.
        SceneManager.LoadScene("MainMenu");
    }

    // private IEnumerator LoadSceneAsync(string v)
    // {
    //     //yield return new WaitForSeconds(10f);
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(v);

    //     while (!asyncLoad.isDone)
    //     {
    //         loadingPanel.SetActive(true);
    //         yield return null;
    //     }
    // }
}