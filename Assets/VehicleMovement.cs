using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{

    public float timer = 2;
    public float minTimer=.3f;
    public float maxTimer = 3;
    public float cooldownTimer = .2f;
    public Vector3 dir;
    public float force = 2;
    public float minForce = .2f;
    public float maxForce = 5;
    float gravity = .1f;

    public float forceTimer = .1f;
    public float forcePerCall = .1f;

    public bool forceAdded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            forceAdded = true;
            force=Random.Range(minForce,maxForce);
        }
        else {
            timer -= Time.deltaTime;
        }
        if (cooldownTimer <= 0)
        {
            if (transform.position.y >= 0.323)
            {
                transform.position -= new Vector3(0, gravity, 0);

            }
            cooldownTimer = 0.2f;
        }
        else { 
        cooldownTimer -= Time.deltaTime;
        }
        if (forceAdded) {
            if (forceTimer < 0)
            {
                if (force <= 0)
                {
                    forceAdded=false;
                   
                }
                else
                {
                    addForce();
                    force -= forcePerCall;

                }
                forceTimer = .1f;

            }
            else { forceTimer -= Time.deltaTime; }
        
        }
        
    }

    public void addForce() {
        timer = Random.RandomRange(minTimer, maxTimer);
        gameObject.transform.position += new Vector3(0, forcePerCall, 0);


    }
}
