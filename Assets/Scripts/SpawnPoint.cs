using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

    [SerializeField]
    private GameObject enemyPrefab;

	void Update () {	
	}

    public GameObject Spawn()
    {
        GameObject enemy = Instantiate(enemyPrefab) as GameObject;

        enemy.transform.position = transform.position;
        enemy.transform.eulerAngles = transform.eulerAngles;

        return enemy;
    }
}
