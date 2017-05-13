using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCheck : MonoBehaviour {

    Ray ray;
    float raycastRange;
	// Use this for initialization
	void Start () {
        raycastRange = 18;
	}
	
	// Update is called once per frame
	void Update () {
        ray = new Ray(transform.position, transform.up);
        //ray[1] = new Ray(child.position + child.TransformVector(1f, 0.5f, 0), child.forward);
        //ray[2] = new Ray(child.position + child.TransformVector(-1f, 0.5f, 0), child.forward);

        RaycastHit hit;
        bool val = false;
        //Debug.DrawRay(ray.origin, ray.direction * raycastRange, Color.black);
        //Debug.Break();
        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            Destroy(gameObject);
        }
        else
        {
            GameObject go = GameObject.FindGameObjectWithTag("TerrainManager");
            go.GetComponent<TerrainManager>().CallTerrain(transform.position.x, transform.position.z);
            Destroy(gameObject);
        }    
	}
}
