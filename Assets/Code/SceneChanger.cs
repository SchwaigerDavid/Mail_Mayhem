using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LoadPackageRun()
    {
        PackageAnimation.instance.open = true; 
        foreach (GameObject item in PackageSaver.instance.getPackage())
        {
            item.transform.parent = PackageSaver.instance.packageHolder.transform;
        }

        SceneManager.LoadScene("Delivery", LoadSceneMode.Single);
     
     
    }
}
