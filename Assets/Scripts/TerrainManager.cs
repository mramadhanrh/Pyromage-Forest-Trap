using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    public GameObject[] terrainObject;
    public GameObject objectChecker;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallTerrain(float x, float z)
    {
        int i = Random.Range(0, terrainObject.Length);
        Instantiate(terrainObject[i].gameObject, new Vector3(x, 0, z), Quaternion.identity);

    }

    public void CallCheker(float x, float z)
    {
        Instantiate(objectChecker, new Vector3(x, 0, z), Quaternion.identity);
    }
}
