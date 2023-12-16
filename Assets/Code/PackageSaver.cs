using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSaver : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> package;

    public static PackageSaver instance;
    public GameObject packageHolder;
    // Start is called before the first frame update
    void Start()
    {
        package = new List<GameObject>();
        instance = this; 
        DontDestroyOnLoad(packageHolder);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag.Equals("moveable")) { 
            package.Add(other.gameObject);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (package.Contains(other.gameObject)) {
            package.Remove(other.gameObject);
            Debug.Log("Exit");
        }
    }
    public List<GameObject> getPackage() {
        return package;
    }

}
