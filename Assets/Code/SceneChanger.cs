using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Transform package;
    public bool loadPackageRun = false;
    public float timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (loadPackageRun)
        {
            if (timer <= 0)
            {
                foreach (GameObject item in PackageSaver.instance.getPackage())
                {
                    item.transform.parent = PackageSaver.instance.packageHolder.transform;
                }
                PackageSaver.instance.packageHolder.transform.position -= new Vector3(0, 0, PackageSaver.instance.packageHolder.transform.position.z);

                SceneManager.LoadScene("Delivery", LoadSceneMode.Single);
                loadPackageRun = false;
            }
            else { timer -= Time.deltaTime; }

        }
    }
    public void LoadPackageRun()
    {
        PackageAnimation.instance.open = true;
       
       loadPackageRun = true;
     
     
    }
}
