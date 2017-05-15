using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterToPlayer : MonoBehaviour {

    GameObject player;
    IEnumerator coroutine;
    bool status;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //coroutine = WaitSecond(0.2f);
        //StartCoroutine(coroutine);
	}
	
	// Update is called once per frame
	void Update () {
        //if (status)
        //{
        //    GetMove();
        //}   
        if (player != null)
        {
            GetMove();
        }
        else
        {
            Destroy(gameObject);
        }
	}

    void GetMove()
    {
        //triggered
        if (GetComponent<BoxCollider>() != null)
            transform.GetComponent<BoxCollider>().isTrigger = true;

        //remove rigid
        if (GetComponent<Rigidbody>() != null)
            transform.GetComponent<Rigidbody>().useGravity = false;

        //position
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * 2f);

        //scale
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 2f);

        //light
        if (GetComponent<Light>() != null)
            transform.GetComponent<Light>().intensity = Mathf.Lerp(transform.GetComponent<Light>().intensity, 0, Time.deltaTime * 2f);
    }

    IEnumerator WaitSecond(float time)
    {
        yield return new WaitForSeconds(time);
        status = true;
    }



    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Destroy(gameObject);
            if (GetComponentInParent<Transform>().childCount <= 2)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
