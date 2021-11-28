using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemycount = 1;
    [HideInInspector]
    public bool gameOver;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver && FindObjectOfType<PlayerController>().ammo <=0 && enemycount > 0)
        {
            gameOver = true;
        }

    }


    public void CheckEnemyCount()
    {
        enemycount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(enemycount <= 0)
        {

        }
    }
}
