using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool packing = false;
    public int level;
    private bool startdelivering = false;
    public GameObject winner;
    public GameObject loser;
    public bool won = false;
    public float timer = 10; 


    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
       
        if (startdelivering&&!packing) {
            if (timer <= 0)
            {
                winner.SetActive(true);
                won = true;
            }
            else
            {
                timer-=Time.deltaTime;
            }
        }
    }

    public void youLose() {
        if (!won)
        {
            loser.SetActive(true);
            startdelivering = false;
        }
        
    }

    public void startDelivering() { 
        startdelivering = true;
    }

}
