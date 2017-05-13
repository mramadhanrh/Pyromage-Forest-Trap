using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image hpUI;
    public Image mpUI;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        CallUI();
	}

    public void CallUI()
    {
        HealthPoint();
        ManaPoint();
    }

    void HealthPoint()
    {
        float hpMax = GameManager.hpMax;
        float hp = PlayerAction.hp;
        hpUI.fillAmount = hp / hpMax;
    }

    void ManaPoint()
    {
        float mpMax = GameManager.mpMax;
        float mp = PlayerAction.mp;
        mpUI.fillAmount = mp / mpMax;
    }
}
