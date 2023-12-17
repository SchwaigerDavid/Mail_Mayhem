using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float timer = 2;
    public float minTimer = .3f;
    public float maxTimer = 3;
    //public float cooldownTimer = .2f;
    public Vector3 dir;
    public float force = 2;
    public float minForce = .2f;
    public float maxForce = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            force = Random.Range(minForce, maxForce);
            timer = Random.RandomRange(minTimer, maxTimer);
            transform.GetComponent<Rigidbody>().AddForce(dir * force);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
