using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoBackToMainMenuScript : MonoBehaviour
{
    private void Start() {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(GoBackToMainMenu);
    }

    private void GoBackToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}