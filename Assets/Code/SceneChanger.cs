using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   
    public bool loadPackageRun = false;
    public float timer = 2;
    public int level;
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
                loadLevel(GameManager.instance.level);
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
    public void startPacking(int level) { 
            GameManager.instance.level = level;
            SceneManager.LoadScene("Packing", LoadSceneMode.Single);
    }

    public void loadLevel(int level) {

        switch (level) {
            case 0:
                //tutorial
          
                break;
            case 1:
                PackageSaver.instance.packageHolder.transform.position = new Vector3(-1.118f, 1.186f, 0);
                SceneManager.LoadScene("Delivery_Vehicle", LoadSceneMode.Single);
                break;
            case 2:
                PackageSaver.instance.packageHolder.transform.position -= new Vector3(0,0, PackageSaver.instance.packageHolder.transform.position.z);
                SceneManager.LoadScene("Delivery_Airport", LoadSceneMode.Single);
                break;
            case 3:
                //Level 3 
                break;
        }
       // SceneManager.LoadScene("Delivery", LoadSceneMode.Single);
    }
}
