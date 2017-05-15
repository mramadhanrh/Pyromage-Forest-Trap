using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour {

	// Use this for initialization

    public float range;
    public Ray[] ray = new Ray[9];
    public GameObject bulletObject;
    public Button buttonAction;
    public float mpAbsorb;
    public float mpDecrement;
    public GameObject shattered;
    public float jumpPower;
    public GameObject NoMana;
    public Sprite[] butAction;

    [Range(0, 10)]
    public float speedhealing;

    bool isJumping;
    RaycastHit hit;
    bool isAbsorb;
    bool mpHeal, hpHeal;
    public static float mp, hp;

    public GameObject Explosion;
    public Material mtr;
    public Color[] colors;

    public AudioClip musicExplosion;

	void Start () {
        mp = GameManager.mp;
        hp = GameManager.hp;
        mtr.color = colors[1];
	}
	
	// Update is called once per frame
	void Update () {
        //foreach (Ray rays in ray)
        //{
        //    Debug.DrawRay(rays.origin, rays.direction * range, Color.black);
        //}

        HpMpManager();
        ButtonManager();
        HpMpHealing();
        if (mp <= mpDecrement)
        {
            buttonAction.GetComponent<Image>().sprite = butAction[0];
        }
        else
        {
            buttonAction.GetComponent<Image>().sprite = butAction[1];
        }
	}

    public void infiniteMana()
    {
        mpDecrement = 0;
    }

    void ButtonManager()
    {
        if (isAbsorb)
        {
            buttonAction.enabled = false;
        }
        else
        {
            buttonAction.enabled = true;
        }
    }

    void HpMpHealing()
    {
        if (hpHeal)
            hp += Time.deltaTime * speedhealing;

        if (mpHeal)
            mp += Time.deltaTime * speedhealing;
    }

    void HpMpManager()
    {
        if (mp > GameManager.mpMax)
        {
            mp = GameManager.mpMax;
        }
        else if (mp < 0)
        {
            mp = 0;
        }

        if (hp > GameManager.hpMax)
        {
            hp = GameManager.hpMax;
        }
        else if (hp < 0)
        {
            hp = 0;
            Instantiate(Explosion, transform.position, Quaternion.identity);
            GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
            gm.GetComponent<GameManager>().GameOver();
            Destroy(gameObject);
        }
    }

    public void DoAction()
    {
        if (mp > mpDecrement)
        {
            Shoot();
            GetComponent<AudioSource>().PlayOneShot(musicExplosion);
            buttonAction.interactable = false;
            StartCoroutine(DelayShoot());
        }
        else
        {
            StartAbsorb();
        }
    }

    IEnumerator DelayShoot()
    {
        yield return new WaitForSeconds(0.7f);
        buttonAction.interactable = true;
    }

    void StartAbsorb()
    {
        int x = 9;
        ray[0] = new Ray(transform.position + transform.TransformVector(-1, 1f, 0f) * x, transform.forward);
        ray[1] = new Ray(transform.position + transform.TransformVector(0f, 1f, 0f) * x, transform.forward);
        ray[2] = new Ray(transform.position + transform.TransformVector(1f, 1f, 0f) * x, transform.forward);
        ray[3] = new Ray(transform.position + transform.TransformVector(-0.5f, 0.5f, 0f) * x, transform.forward);
        ray[4] = new Ray(transform.position + transform.TransformVector(0f, 0.5f, 0f) * x, transform.forward);
        ray[5] = new Ray(transform.position + transform.TransformVector(0.5f, 0.5f, 0f) * x, transform.forward);
        ray[6] = new Ray(transform.position + transform.TransformVector(-0.5f, 0f, 0f) * x, transform.forward);
        ray[7] = new Ray(transform.position + transform.TransformVector(0f, 0f, 0f) * x, transform.forward);
        ray[8] = new Ray(transform.position + transform.TransformVector(0.5f, 0f, 0f) * x, transform.forward);


        foreach (Ray rays in ray)
        {
            if (Physics.Raycast(rays, out hit, range))
            {
                if (hit.transform.tag == "Absorbable")
                {
                    Destroy(hit.transform.gameObject);
                    Absorb(hit);
                    break;
                }
                Debug.Log(hit.transform.name);
            }
            else
            {
                Debug.Log("Gk Kena");
                GameObject nomana = (GameObject)Instantiate(NoMana, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity);
                nomana.transform.position = gameObject.transform.position;
            }
        }

        //if (Physics.Raycast(ray, out hit, range))
        //{
        //    if (hit.transform.tag == "Absorbable")
        //        Absorb(hit);
        //    Debug.Log(hit.transform.name);
        //}
        //else
        //{
        //    Debug.Log("Gk kena tuh");
        //}
    }

    void Absorb(RaycastHit hit)
    {
        //hit.transform.gameObject.GetComponent<Animator>().Play("SunctionRotation");
        isAbsorb = true;
        hit.transform.GetComponent<BoxCollider>().isTrigger = true;
        HasAbsorb();
        AbsorbHit();
    }

    void AbsorbHit()
    {
        //remove rigid
        //hit.transform.GetComponent<Rigidbody>().useGravity = false;

        ////position
        //Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        //hit.transform.position = Vector3.Lerp(hit.transform.position, playerPos, Time.deltaTime * 2);

        ////scale
        //hit.transform.localScale = Vector3.Lerp(hit.transform.localScale, Vector3.zero, Time.deltaTime * 2);

        //animation
        //GetComponent<Animator>().Play("Player_Absorb");

        Instantiate(shattered, hit.transform.position, Quaternion.identity);

    }

    void HasAbsorb()
    {
        GetComponent<Animator>().Play("Player_HasAbsorb");
        Debug.Log("HasAbsorb");
        mp += mpAbsorb;
    }

    void Shoot()
    {
        if (mp > mpDecrement)
        {
            mp -= mpDecrement;
            Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            GameObject obj = (GameObject)Instantiate(
                bulletObject,
                playerPos + transform.forward,
                Quaternion.identity);

            Vector3 a = transform.position + (transform.forward * 2);
            Vector3 b = transform.forward;
            GetComponent<Animator>().Play("Player_Shoot");
            obj.transform.LookAt(transform.forward * 400);
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 400);
        }
    }

    public void Jump()
    {
        if (!isJumping)
        {
            //Debug.Log("Masuk");
            GetComponent<Rigidbody>().AddForce(0, jumpPower, 0, ForceMode.Impulse);
        }

    }

    IEnumerator GotHit()
    {
        mtr.color = colors[0];
        yield return new WaitForSeconds(0.3f);
        mtr.color = colors[1];
    }

    void OnCollisionEnter(Collision col)
    {
        isJumping = false;

        if (col.gameObject.tag == "Enemy")
        {
            hp -= 10;
            GetComponent<Rigidbody>().AddForce(col.transform.GetChild(0).forward * 2, ForceMode.Impulse);
            StartCoroutine(GotHit());
            StopCoroutine(GotHit());
        }

        if (col.gameObject.tag == "HealingThrone")
            hpHeal = true;

        if (col.gameObject.tag == "ManaThrone")
            mpHeal = true;
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            hp -= Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(col.transform.GetChild(0).forward * 2, ForceMode.Impulse);
        }
    }

    void OnCollisionExit(Collision col)
    {
        isJumping = true;

        if (col.gameObject.tag == "HealingThrone")
            hpHeal = false;

        if (col.gameObject.tag == "ManaThrone")
            mpHeal = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Absorbable")
        {
            Destroy(col.gameObject);
            isAbsorb = false;
        }
    }
}
