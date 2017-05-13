using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public GameObject[] enemy;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnEnemy", 4f, 4f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnEnemy()
    {
        float x = Random.Range(6, 15);
        float z = Random.Range(6, 15);
        int i = Random.Range(0, enemy.Length);
        Vector3 pos = new Vector3(transform.position.x + x, 6, transform.position.z + z);
        Instantiate(enemy[i], pos, Quaternion.identity);
    }
}
