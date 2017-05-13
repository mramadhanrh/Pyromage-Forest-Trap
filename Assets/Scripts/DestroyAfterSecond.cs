using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSecond : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyAfter());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
