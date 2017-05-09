using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderManager : MonoBehaviour {

    public List<GameObject> spaceShipsPlayer;
    public Button NextButton;
    public Button PreviousButton;
    public Button PlayButton;

    private static string KeyNameSpaceShipSelected = "SpaceShip.Selected.Name";
    private string      spaceShipName;
    private int         spaceShipIndex;
    private GameObject  currentSpaceShip;

    // Use this for initialization
    void Start () {
        Debug.Log("Starting...");
        if (spaceShipsPlayer.Count > 0)
        {
            selectSpaceShip(0);
        }else
        {
            Debug.LogError("No spaceships in the queue !");
        }
        NextButton.onClick.AddListener(GoNext);
        PreviousButton.onClick.AddListener(GoPrevious);
        PlayButton.onClick.AddListener(Play);
    }

    void GoNext()
    {
        spaceShipIndex = (spaceShipIndex == (spaceShipsPlayer.Count - 1)) ? 0 : spaceShipIndex + 1;
        selectSpaceShip(spaceShipIndex);
    }
    void GoPrevious()
    {
        spaceShipIndex = (spaceShipIndex == 0) ? (spaceShipsPlayer.Count - 1) : spaceShipIndex - 1;
        selectSpaceShip(spaceShipIndex);
    }

    void Play()
    {
        SceneManager.LoadScene("Main");
    }
    
    void selectSpaceShip(int newIndex)
    {
        if (currentSpaceShip != null)
        {
            Debug.Log("Destroy old spaceship: " + currentSpaceShip.name);
            Destroy(currentSpaceShip);
        }
        spaceShipName = spaceShipsPlayer[spaceShipIndex].name;
        currentSpaceShip = Instantiate(spaceShipsPlayer[spaceShipIndex], new Vector3(0, 0, -0.5f), Quaternion.identity);
        Debug.Log("Create new spaceship: " + currentSpaceShip.name);
        var keyStored = String.Format("Prefabs/SpaceShips/Players/Player_{0}", spaceShipName);
        PlayerPrefs.SetString(KeyNameSpaceShipSelected, keyStored);
        Debug.Log("Save name in PlayerPrefs: " + keyStored);
    }

}
