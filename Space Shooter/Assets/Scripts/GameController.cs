﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    //public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    private int score;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int currentTurn;


    private static string KeyNameSpaceShipSelected = "SpaceShip.Selected.Name";

    IEnumerator SpawnWaves()
    {
        // wait before starting
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            HideTurn();

            //int hazardCount = (int)Mathf.Log(currentTurn, 2f);
            int hazardCount = currentTurn;
            for (int i = 0; i < hazardCount; i++)
            {
                var hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                // [0,1] determines how much asteroids we want to show by seconds
                yield return new WaitForSeconds(spawnWait);
            }

            DisplayTurn();

            // wait before next wave
            yield return new WaitForSeconds(waveWait);
            
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
            currentTurn++;
        }
    }


    //IEnumerator SpawnWaves()
    //{
    //    // wait before starting
    //    yield return new WaitForSeconds(startWait);
    //    while (true)
    //    {
    //        for (int i = 0; i < hazardCount; i++)
    //        {
    //            var hazard = hazards[Random.Range(0, hazards.Length)];
    //            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
    //            Quaternion spawnRotation = Quaternion.identity;
    //            Instantiate(hazard, spawnPosition, spawnRotation);
    //            // [0,1] determines how much asteroids we want to show by seconds
    //            yield return new WaitForSeconds(spawnWait);
    //        }
    //        // wait before next wave
    //        yield return new WaitForSeconds(waveWait);

    //        if (gameOver)
    //        {
    //            restartText.text = "Press 'R' for Restart";
    //            restart = true;
    //            break;
    //        }
    //    }
    //}

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    private void Start()
    {
        score = 0;
        gameOver = false;
        restart = false;
        restartText.text = string.Empty;
        gameOverText.text = string.Empty;
        currentTurn = 1;

        UpdateScore();
        // Create the right space ship for the player
        Instantiate(Resources.Load(PlayerPrefs.GetString(KeyNameSpaceShipSelected), typeof(GameObject))); // TODO: bad and you should feel bad !
        // to not block the main process
        StartCoroutine(SpawnWaves());
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void DisplayTurn()
    {
        gameOverText.text = "Turn: " + currentTurn;
    }

    void HideTurn()
    {
        gameOverText.text = string.Empty;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}

