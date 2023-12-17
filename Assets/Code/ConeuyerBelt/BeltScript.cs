using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltScript : MonoBehaviour
{
    [SerializeField]
    private float speed, beltSpeed;
    [SerializeField]
    private Vector3 dir;
    [SerializeField]
    private List<GameObject> onBelt = new List<GameObject>();

    public LayerMask ignoreMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < onBelt.Count; i++) { 
            onBelt[i].GetComponent<Rigidbody>().AddForce(dir * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer != 7&&!(onBelt.Contains(collision.gameObject)))

        {
            if (collision.transform.parent != null)
            {
                onBelt.Add(collision.gameObject.transform.parent.gameObject);
            }
            else {
                onBelt.Add(collision.gameObject);
            }
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {   
            onBelt.Remove(collision.gameObject);
    }
    
}
