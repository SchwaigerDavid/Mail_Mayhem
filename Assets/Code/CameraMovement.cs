using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public float dist = 4; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = target.position+ new Vector3(0,0,dist);
    
       
        transform.rotation = target.rotation * Quaternion.Euler(new Vector3(target.rotation.eulerAngles.x*-1,0,0)) * Quaternion.Euler(new Vector3(0, target.rotation.eulerAngles.y * -1, 0));
    
    }
}
