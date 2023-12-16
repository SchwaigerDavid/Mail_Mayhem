using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPackage : MonoBehaviour
{
    private PackageSaver packageSaver;
    [SerializeField]
    private List<GameObject> packageObjects = new List<GameObject>();

    public static LoadPackage instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
      
        CameraMovement.instance.target=PackageSaver.instance.packageHolder.transform;
    }

    public void setPackageObjects(List<GameObject> go) {


        packageObjects = go;
    }

  
}
