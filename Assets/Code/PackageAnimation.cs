using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PackageAnimation : MonoBehaviour
{

    public GameObject o1;
    public GameObject o2;

    public Vector3 openRot1 = new Vector3(65, 0, 0);
    public Vector3 closedRot1 = new Vector3(270, 0, 0); 

  
    public float speed = 5;
    public float timer = .05f;
    public bool open = false;
    public bool closed = false;
    public float closeTimer = .05f;

    public static PackageAnimation instance;
  

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //closed = true;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (closed)
        {
            if (timer <= 0)
            {
                if (o1.transform.eulerAngles.x <= openRot1.x && o1.transform.eulerAngles.x > openRot1.x - 1)
                {
                    
                    closed = false;
                }
                else
                {
                    o1.transform.Rotate(speed, 0, 0);
                    o2.transform.Rotate(-speed, 0, 0);
                }
                timer = .05f;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        if (open) {
            if (closeTimer <= 0)
            {
                if (o2.transform.rotation.eulerAngles.x <= 271 && o2.transform.rotation.eulerAngles.x > 269)
                {
                    open = false;

                }
                else
                {
                    o1.transform.Rotate(-speed, 0, 0);
                    o2.transform.Rotate(speed, 0, 0);
                   
                }
                closeTimer = .05f;
            }
            else
            {
                closeTimer -= Time.deltaTime;
            }
        }
    }

  
    
}
