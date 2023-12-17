using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageObject : MonoBehaviour
{

    public Vector3 position;
    public Vector3 rotation;
    public GameObject prefabGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        rotation = transform.rotation.eulerAngles; 
    }
}
