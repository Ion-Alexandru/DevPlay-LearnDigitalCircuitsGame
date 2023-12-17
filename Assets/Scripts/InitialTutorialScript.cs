using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialTutorialScript : MonoBehaviour
{
    public List<GameObject> tutorialList;
    private int currentTutorialIndex = 0;
    public Button nextButton;

    private const string FirstTimeKey = "FirstTime";

    private bool isFirstTime;

    private void Awake()
    {
        // Check if the flag has been set before
        isFirstTime = PlayerPrefs.GetInt(FirstTimeKey, 1) == 1;

        if (isFirstTime)
        {
            nextButton.gameObject.SetActive(true);

            foreach (GameObject tutorial in tutorialList)
            {
                tutorial.SetActive(false);
            }

            currentTutorialIndex = 0;
            tutorialList[currentTutorialIndex].SetActive(true);
            nextButton.onClick.AddListener(NextTutorial);

            isFirstTime = false;
            PlayerPrefs.SetInt(FirstTimeKey, 0);
            PlayerPrefs.Save();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        nextButton.gameObject.SetActive(true);

            foreach (GameObject tutorial in tutorialList)
            {
                tutorial.SetActive(false);
            }

            currentTutorialIndex = 0;
            tutorialList[currentTutorialIndex].SetActive(true);
            nextButton.onClick.AddListener(NextTutorial);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NextTutorial()
    {
        if (currentTutorialIndex < tutorialList.Count)
        {
            tutorialList[currentTutorialIndex].SetActive(false);
        }

        currentTutorialIndex++;

        if (currentTutorialIndex < tutorialList.Count)
        {
            tutorialList[currentTutorialIndex].SetActive(true);
        }
        else
        {
            nextButton.gameObject.SetActive(false);
        }
    }
}
