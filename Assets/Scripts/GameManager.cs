using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static float hpMax, mpMax, damage, speed, goldMultiplier, hp, mp;
    public static int playerLevel, highScore, score;
    public static string playerName;
    public static bool[] rewards = new bool[3];

    public static float kelpMonsterHp, rageMonsterHp;
    public static int levelMaps;
    GameObject player;
    public float chunkRender;

    public GameObject scoreUI, GameOverObject;
    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Load();
        Set();
    }

    // Update is called once per frame
    void Update()
    {
        Optimization();
        scoreUI.GetComponent<TextMeshProUGUI>().SetText(score.ToString());
    }

    void Optimization()
    {
        if (player != null)
        {
            Vector3 playerPos = player.GetComponent<Transform>().position;
            float limitX, limitZ, minlimitX, minlimitZ;
            limitX = playerPos.x + chunkRender;
            limitZ = playerPos.z + chunkRender;
            minlimitX = playerPos.x - chunkRender;
            minlimitZ = playerPos.z - chunkRender;
            GameObject[] terrains = GameObject.FindGameObjectsWithTag("Terrain");
            foreach (GameObject terrain in terrains)
            {
                float x = terrain.GetComponent<Transform>().position.x;
                float z = terrain.GetComponent<Transform>().position.z;
                if (x > limitX || x < minlimitX || z > limitZ || z < minlimitZ)
                {
                    terrain.SetActive(false);
                }
                else
                {
                    terrain.SetActive(true);
                }
            }
        }
    }

    void Set()
    {
        if (levelMaps == 1)
        {
            kelpMonsterHp = 100;
            rageMonsterHp = 150;
        }
        else
        {
            kelpMonsterHp = 100;
            rageMonsterHp = 150;
        }
    }

    void Load()
    {
        {//Float Type 
            //hp
            if (PlayerPrefs.HasKey("hpMax"))
                hpMax = PlayerPrefs.GetFloat("hpMax");
            else
                hpMax = 100f;

            //mp
            if (PlayerPrefs.HasKey("mpMax"))
                mpMax = PlayerPrefs.GetFloat("mpMax");
            else
                mpMax = 100f;

            //damage
            if (PlayerPrefs.HasKey("damage"))
                damage = PlayerPrefs.GetFloat("damage");
            else
                damage = 50;

            //speed
            if (PlayerPrefs.HasKey("speed"))
                speed = PlayerPrefs.GetFloat("speed");
            else
                speed = 3;

            //goldMultiplier
            if (PlayerPrefs.HasKey("goldMultiplier"))
                goldMultiplier = PlayerPrefs.GetFloat("goldMultiplier");
            else
                goldMultiplier = 1;
        }

        {//int
            //playerLevel
            if (PlayerPrefs.HasKey("playerLevel"))
                playerLevel = PlayerPrefs.GetInt("playerLevel");
            else
                playerLevel = 1;

            //highscore
            if (PlayerPrefs.HasKey("highscore"))
                highScore = PlayerPrefs.GetInt("highscore");
            else
                highScore = 0;

            //score
            score = 0;
        }

        {//string
            if (PlayerPrefs.HasKey("playerName"))
                playerName = PlayerPrefs.GetString("playerName");
            else
                playerName = "NoName";
        }

        {//bool array
            string _rewards;
            for (int i = 0; i < rewards.Length; i++)
            {
                _rewards = null;
                if (PlayerPrefs.HasKey("rewards[" + i + "]"))
                {
                    _rewards = PlayerPrefs.GetString("rewards[" + i + "]");
                    if (_rewards == "true")
                    {
                        rewards[i] = true;
                        //Debug.Log("i = " + i);
                    }
                    else
                        rewards[i] = false;
                }
                else
                {
                    rewards[i] = false;
                }
            }//end For
        }

        {//Constant Value
            hp = hpMax;
            mp = mpMax;
        }
    }//End Load Method

    public void GameOver()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
        }
        GameOverObject.SetActive(true);
    }

    public void Sceneload(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
