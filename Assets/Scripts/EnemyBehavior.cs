using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Behavior
{
    patrol,
    chasing,
    stunned
}

public enum Facing
{
    front,
    back,
    left,
    right
}

public enum EnemyType
{
    KelpMonster,
    RageMonster
}

public class EnemyBehavior : MonoBehaviour {

    //public static Behavior behavior;
    public Behavior behavior;
    GameObject target;
    public Image HpFill;
    public EnemyType enemyType;

    [Range(0,10)]
    public float speed;
    [Range(0, 10)]
    public float raycastRange;
    public GameObject voxelExplode;
    Quaternion qfront, qleft, qright, qback;
    IEnumerator coroutine;
    bool isTouching;
    [Range(0, 100)]
    float hp, hpMax;
    Facing facing;
    Transform child;

	// Use this for initialization
	void Start () {
        child = GetComponentsInChildren<Transform>()[1];
        qfront = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        qleft = new Quaternion(0.0f, -0.7f, 0.0f, 0.7f);
        qright = new Quaternion(0.0f, 0.7f, 0.0f, 0.7f);
        qback = new Quaternion(0.0f, 1.0f, 0.0f, 0.0f);
        InvokeRepeating("ChangeFacing", 0f, 2f);
        CheckType();
        hp = hpMax;
        //Debug.Log(name + " : " + hpMax);
        target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        BehaviorManager();
        if (hp <= 0f)
        {
            Instantiate(voxelExplode, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameManager.score++;
        }
        HpFill.fillAmount = hp / hpMax;
        //Debug.Log(GetComponentsInChildren<Transform>()[1].rotation);
        if (gameObject.transform.position.y < -70f)
        {
            Destroy(gameObject);
        }
	}

    void CheckType()
    {
        if(enemyType == EnemyType.KelpMonster)
            hpMax = GameManager.kelpMonsterHp;
        else if(enemyType == EnemyType.RageMonster)
            hpMax = GameManager.rageMonsterHp;
    }

    void BehaviorManager()
    {
        switch (behavior)
        {
            case Behavior.patrol:
                Patrol();
                break;
            case Behavior.chasing:
                Chasing();
                break;
            case Behavior.stunned:
                Stunned();
                break;
            default:
                Patrol();
                break;
        }
    }

    void ChangeFacing()
    {
        facing = (Facing)Random.Range(0, 4);
        //Debug.Log(facing);
    }

    IEnumerator ChangeFacingRaycast()
    {
        if (isTouching)
        {
            //Debug.Log("Coroutine");
            isTouching = false;
            ChangeFacing();
            yield return new WaitForSeconds(5f);
        }
    }

    bool Raycasting()
    {
        Ray[] ray = new Ray[1];
        ray[0] = new Ray(child.position + child.TransformVector(0, 0.5f, 0), child.forward);
        //ray[1] = new Ray(child.position + child.TransformVector(1f, 0.5f, 0), child.forward);
        //ray[2] = new Ray(child.position + child.TransformVector(-1f, 0.5f, 0), child.forward);

        RaycastHit hit;
        bool val = false;
        foreach (Ray rays in ray)
        {
            //Debug.DrawRay(rays.origin, rays.direction * raycastRange, Color.black);
            if (Physics.Raycast(rays, out hit, raycastRange))
            {
                if (hit.transform.tag == "Wall" || hit.transform.tag == "Enemy")
                {
                    val = true;
                }
                else
                {
                    val = false;
                }
            }
        }

        return val;
    }

    void Patrol()
    {
        if (facing == Facing.front)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //GetComponentsInChildren<Transform>()[1].LookAt(transform.position + Vector3.forward);
            GetComponentsInChildren<Transform>()[1].rotation = Quaternion.Slerp(GetComponentsInChildren<Transform>()[1].rotation, qfront, 0.1f);
        }
        else if (facing == Facing.back)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            //GetComponentsInChildren<Transform>()[1].LookAt(transform.position + Vector3.back);
            GetComponentsInChildren<Transform>()[1].rotation = Quaternion.Slerp(GetComponentsInChildren<Transform>()[1].rotation, qback, 0.1f);
        }
        else if (facing == Facing.left)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            //GetComponentsInChildren<Transform>()[1].LookAt(transform.position + Vector3.left);
            GetComponentsInChildren<Transform>()[1].rotation = Quaternion.Slerp(GetComponentsInChildren<Transform>()[1].rotation, qleft, 0.1f);
        }
        else if (facing == Facing.right)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            //GetComponentsInChildren<Transform>()[1].LookAt(transform.position + Vector3.right);
            GetComponentsInChildren<Transform>()[1].rotation = Quaternion.Slerp(GetComponentsInChildren<Transform>()[1].rotation, qright, 0.1f);
        }

        if (Raycasting())
        {
            isTouching = true;
            coroutine = ChangeFacingRaycast();
            StartCoroutine(coroutine);
        }
    }

    void Chasing()
    {
        if (target != null)
        {
            transform.Translate((target.transform.position - transform.position) * (speed * 0.2f) * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            //GetComponentsInChildren<Transform>()[1].rotation = Quaternion.Slerp(GetComponentsInChildren<Transform>()[1].rotation, targetRotation, 8f * Time.deltaTime);
            //transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
            GetComponentsInChildren<Transform>()[1].LookAt(target.transform);
        }
    }

    void Stunned()
    {

    }

    IEnumerator StopOnFire()
    {
        yield return new WaitForSeconds(3f);
        child.GetComponent<Animator>().SetBool("OnFire", false);
    }

    void OnCollisionEnter(Collision col)
    {
        float damage = GameManager.damage;
        if (col.gameObject.tag == "Fireball")
        {
            hp -= damage;
            child.GetComponent<Animator>().SetBool("OnFire", true);
            StartCoroutine(StopOnFire());
        }
    }
}
