using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponentInParent<EnemyBehavior>().behavior = Behavior.chasing;
            GetComponent<SphereCollider>().radius = 10;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponentInParent<EnemyBehavior>().behavior = Behavior.patrol;
            GetComponent<SphereCollider>().radius = 5;
        }
    }
}
