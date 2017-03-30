using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

    private GameObject enemy;
    private SpawnPoint[] spawnPoints;

    void Start()
    {
        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    void Update ()
    {
	    if(enemy == null)
        {
            int i = Random.Range(0, spawnPoints.Length);
            enemy = spawnPoints[i].Spawn();
        }
	}    
}
