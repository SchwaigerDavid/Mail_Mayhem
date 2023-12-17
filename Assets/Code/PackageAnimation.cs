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
        closed = true;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (closed)
        {
            o1.transform.Rotate(new Vector3(155, 0, 0));
            o2.transform.Rotate(new Vector3(205, 0, 0));
            closed = false;
        }
        if (open) {
            o1.transform.Rotate(new Vector3(-155,0,0));
            o2.transform.Rotate(new Vector3(-205, 0, 0));
            open = false;
        }
    }

  
    
}
