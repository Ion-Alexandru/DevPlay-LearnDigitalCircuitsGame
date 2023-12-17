using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialUIScript : MonoBehaviour
{
    public Button wireButton;
    public Button eraserButton;
    public Button exitButton;

    TilemapClick tilemapClick;
    public List<GameObject> pixList;
    public Button pixButton;
    public GameObject congratsScreen;
    public GameObject transition;

    public Button nextButton;

    private int currentPixIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        tilemapClick = FindObjectOfType<TilemapClick>();
        congratsScreen.SetActive(false);
        transition.SetActive(false);

        foreach (GameObject pix in pixList)
        {
            pix.SetActive(false);
        }

        currentPixIndex = 0;
        pixList[currentPixIndex].SetActive(true);

        nextButton.onClick.AddListener(NextPix);
        pixButton.onClick.AddListener(RestartTutorial);
    }

    // Update is called once per frame
    void Update()
    {
        if(!nextButton.isActiveAndEnabled)
        {
            pixButton.gameObject.SetActive(true);
        } else 
        {
            pixButton.gameObject.SetActive(false);
        }

        wireButton.onClick.AddListener(Draw);
        eraserButton.onClick.AddListener(Erase);
        exitButton.onClick.AddListener(Exit);
    }

    private void Draw(){
        tilemapClick.placingWires = true;

        exitButton.gameObject.SetActive(true);
    }

    private void Erase(){
        tilemapClick.deleteComponent = true;

        tilemapClick.placingWires = false;
        tilemapClick.pushButtonPlacement = false;
        tilemapClick.groundPlacement = false;

        exitButton.gameObject.SetActive(true);
    }
    private void Exit(){
        tilemapClick.pushButtonPlacement = false;
        tilemapClick.diodePlacement = false;
        tilemapClick.groundPlacement = false;
        tilemapClick.placingWires = false;
        tilemapClick.deleteComponent = false;

        exitButton.gameObject.SetActive(false);
    }

    private void NextPix()
    {
        if (currentPixIndex < pixList.Count)
        {
            pixList[currentPixIndex].SetActive(false);
        }

        currentPixIndex++;

        if (currentPixIndex < pixList.Count)
        {
            pixList[currentPixIndex].SetActive(true);
        }
        else
        {
            nextButton.gameObject.SetActive(false);
        }
    }

    public void RestartTutorial()
    {
        currentPixIndex = 0;
        pixList[currentPixIndex].SetActive(true);
        nextButton.gameObject.SetActive(true);
    }

    public void playCongrats()
    {
        congratsScreen.SetActive(true);
        Invoke("PlayTransition", 5f);
    }

    private void PlayTransition()
    {
        transition.SetActive(true);
        Invoke("goToMenu", 0.75f);
    }

    private void goToMenu()
    {
        SceneManager.LoadScene("GameplayMenu");
    }
}
