using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour {

    public GameObject voxelEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "EnemySphere")
        {
            Debug.Log(col.gameObject.name);
            Instantiate(voxelEffect, transform.position, Quaternion.identity);
            //Debug.Log(transform.position);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Fireball")
        {
            Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
