using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInfo : MonoBehaviour {

    void Start()
    {
        Destroy(gameObject.GetComponentsInParent<Transform>()[2].transform.gameObject);
    }
}
