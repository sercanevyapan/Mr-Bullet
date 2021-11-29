using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int enemycount = 1;
    [HideInInspector]
    public bool gameOver;

    public int blackBullets = 3;
    public int goldenBullets = 1;

    public GameObject blackBullet, goldenBullet;

    private GameObject gameOverPanel;

    void Awake()
    {
        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);

        FindObjectOfType<PlayerController>().ammo = blackBullets + goldenBullets;

        for (int i = 0; i < blackBullets; i++)
        {
            GameObject bbTemp = Instantiate(blackBullet);
            bbTemp.transform.SetParent(GameObject.Find("Bullets").transform);
        }

        for (int i = 0; i < goldenBullets; i++)
        {
            GameObject gbTemp = Instantiate(goldenBullet);
            gbTemp.transform.SetParent(GameObject.Find("Bullets").transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver && FindObjectOfType<PlayerController>().ammo <=0 && enemycount > 0 && GameObject.FindGameObjectsWithTag("Bullet").Length <=0)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
        }

    }

    public void CheckBullets()
    {
        if (goldenBullets > 0)
        {
            goldenBullets--;
            GameObject.FindGameObjectWithTag("GoldenBullet").SetActive(false);
        }else if (blackBullets > 0)
        {
            blackBullets--;
            GameObject.FindGameObjectWithTag("BlackBullet").SetActive(false);
        }
    }

    public void CheckEnemyCount()
    {
        enemycount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(enemycount <= 0)
        {
             
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
