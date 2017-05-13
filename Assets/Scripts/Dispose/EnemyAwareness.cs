using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            EnemyAI.status = EnemyAI.Status.chasing;
            EnemyAI.target = col.transform;
        }
        
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            EnemyAI.status = EnemyAI.Status.patrol;
        }
    }
}
