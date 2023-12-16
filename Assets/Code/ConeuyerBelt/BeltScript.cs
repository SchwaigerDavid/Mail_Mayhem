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
        if (!collision.transform.tag.Equals("moveable")&&!(onBelt.Contains(collision.gameObject)))
        {
            onBelt.Add(collision.gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.transform.tag.Equals("moveable"))
        {
            
            onBelt.Remove(collision.gameObject);
        }
    }
    
}
