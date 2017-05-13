using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public enum Status
    {
        patrol,
        chasing
    }

    public static Status status;
    public Transform from;
    public Transform to;
    public float speed = 0.1F;
    [Range(0,10)]
    public float rayrange;
    public static Transform target;

    Ray[] ray = new Ray[4];

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        CheckCondition();
	}

    void CheckCondition()
    {
        if (status == Status.patrol)
        {
            Patrol();
        }
        else if (status == Status.chasing)
        {
            Chasing();
        }
    }

    void Chasing()
    {
        //transform.LookAt(target);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 8f * Time.deltaTime);
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        ray[0] = new Ray(transform.position + new Vector3(0, 0, 0.5f), Vector3.forward);
        ray[1] = new Ray(transform.position + new Vector3(0, 0, -0.5f), Vector3.back);
        ray[2] = new Ray(transform.position + new Vector3(1f, 0, 0), Vector3.left);
        ray[3]= new Ray(transform.position + new Vector3(-1f, 0, 0), Vector3.right);

        //debug ray depan belakang kanan kiri
        //Debug.DrawRay(rayfoward.origin, rayfoward.direction * rayrange, Color.black);
        //Debug.DrawRay(raybackward.origin, raybackward.direction * rayrange, Color.black);
        //Debug.DrawRay(rayleft.origin, rayleft.direction * rayrange, Color.black);
        //Debug.DrawRay(rayright.origin, rayright.direction * rayrange, Color.black);

        RaycastHit hit;
        foreach (Ray rays in ray)
        {
            Debug.DrawRay(rays.origin, rays.direction * rayrange, Color.black);
            if (Physics.Raycast(rays, out hit, rayrange))
            {
                if (hit.transform.tag == "Wall" || hit.transform.tag == "Enemy")
                {
                    DoRotate();
                }   
            }
        }
    }

    void DoRotate()
    {
        transform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
