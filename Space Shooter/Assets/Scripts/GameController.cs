using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    IEnumerator SpawnWaves()
    {
        // wait before starting
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            // loop for each asteroids
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                // [0,1] determines how much asteroids we want to show by seconds
                yield return new WaitForSeconds(spawnWait);
            }
            // wait before next wave
            yield return new WaitForSeconds(waveWait);
        }
    }

    private void Start()
    {
        // to not block the main process
        StartCoroutine(SpawnWaves());
    }

}

