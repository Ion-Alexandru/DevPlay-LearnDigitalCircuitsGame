using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftMenuScript : MonoBehaviour
{
    public GameObject gatesShelf;
    public GameObject powerShelf;
    public GameObject switchesShelf;
    public GameObject passiveShelf;

    public Button gatesShelfButton;
    public Button powerShelfButton;
    public Button switchesShelfButton;
    public Button passiveShelfButton;

    public Button exitGatesButton;
    public Button exitPowerButton;
    public Button exitSwitchesButton;
    public Button exitPassivesButton;

    void Start()
    {
        gatesShelf.SetActive(false);
        powerShelf.SetActive(false);
        switchesShelf.SetActive(false);
        passiveShelf.SetActive(false);

        gatesShelfButton.onClick.AddListener(openGatesShelf);
        powerShelfButton.onClick.AddListener(openPowerShelf);
        switchesShelfButton.onClick.AddListener(openSwitchesShelf);
        passiveShelfButton.onClick.AddListener(openPassiveShelf);
        exitGatesButton.onClick.AddListener(exitGates);
        exitPowerButton.onClick.AddListener(exitPower);
        exitSwitchesButton.onClick.AddListener(exitSwitches);
        exitPassivesButton.onClick.AddListener(exitPassive);
    }

    private void openGatesShelf()
    {
        gatesShelf.SetActive(true);

        powerShelf.SetActive(false);
        switchesShelf.SetActive(false);
        passiveShelf.SetActive(false);
    }

    private void openPowerShelf()
    {
        powerShelf.SetActive(true);

        gatesShelf.SetActive(false);
        switchesShelf.SetActive(false);
        passiveShelf.SetActive(false);
    }

    private void openSwitchesShelf()
    {
        switchesShelf.SetActive(true);

        gatesShelf.SetActive(false);
        powerShelf.SetActive(false);
        passiveShelf.SetActive(false);
    }

    private void openPassiveShelf()
    {
        passiveShelf.SetActive(true);

        gatesShelf.SetActive(false);
        powerShelf.SetActive(false);
        switchesShelf.SetActive(false);
    }

    private void exitPower()
    {
        powerShelf.SetActive(false);
    }

    private void exitGates()
    {
        gatesShelf.SetActive(false);
    }

    private void exitSwitches()
    {
        switchesShelf.SetActive(false);
    }

    private void exitPassive()
    {
        passiveShelf.SetActive(false);
    }
}