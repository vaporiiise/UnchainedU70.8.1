using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;
    public List<GameObject> beamObjects; // List of beams corresponding to objects
    public Vector3 spawnPosition;
    public float beamDuration = 1f;
    public float despawnTime = 2f;
    public float intervalTime = 1f;
    public float loopDelay = 3f;
    public float speedIncreaseFactor = 0.9f; // Reduce times by 10% per success
    // Delay before restarting the loop

    public AudioSource audioSource;
    public AudioClip beamSpawnSFX;
    public AudioClip objectSpawnSFX;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            for (int i = 0; i < objectsToSpawn.Count; i++)
            {
                // Spawn beam
                GameObject beam = Instantiate(beamObjects[i], spawnPosition, Quaternion.identity);
                if (audioSource && beamSpawnSFX)
                {
                    audioSource.PlayOneShot(beamSpawnSFX);
                }
                yield return new WaitForSeconds(beamDuration);
                Destroy(beam);

                // Spawn object
                GameObject spawnedObj = Instantiate(objectsToSpawn[i], spawnPosition, Quaternion.identity);
                if (audioSource && objectSpawnSFX)
                {
                    audioSource.PlayOneShot(objectSpawnSFX);
                }
                yield return new WaitForSeconds(despawnTime);
                Destroy(spawnedObj);

                yield return new WaitForSeconds(intervalTime);
            }

            // Wait before restarting the loop
            yield return new WaitForSeconds(loopDelay);
        }
    }
    public void SpeedUp()
    {
        beamDuration *= speedIncreaseFactor;
        despawnTime *= speedIncreaseFactor;
        intervalTime *= speedIncreaseFactor;
        loopDelay *= speedIncreaseFactor;
    }
}