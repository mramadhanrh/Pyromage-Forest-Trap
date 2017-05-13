using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawn : MonoBehaviour {

    bool isSpawn = false;

    float xStart, xEnd, zStart, zEnd;
    TerrainManager tm;
    float length;
	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("TerrainManager");
        tm = go.GetComponent<TerrainManager>();
        length = 20;
	}

    IEnumerator SpawnCheck()
    {
            if (isSpawn == false)
            {
                xStart = GetComponentInParent<Transform>().position.x - length;
                zStart = GetComponentInParent<Transform>().position.z + length;

                xEnd = GetComponentInParent<Transform>().position.x + length;
                zEnd = GetComponentInParent<Transform>().position.z - length;

                for (float i = zStart; i > zEnd; i -= 5)
                {
                    for (float j = xStart; j < xEnd; j += 5)
                    {
                        //Debug.Log(j + " : " + i);
                        tm.CallCheker(j, i);
                    }
                }
                isSpawn = true;
            }
            yield return null;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(SpawnCheck());
        }
    }

}
