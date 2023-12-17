using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private Vector3 mousePos;
    private RaycastHit hit;
    private Transform lastHit;

    private bool moving = false;
    public string[] levelNames;

    public LayerMask uiLayerMask;

    public bool clicked = false;
    public GameObject showLevel; 
    public GameObject showCredits;
    public GameObject lvltut;
    public GameObject lvl1;
    public GameObject lvl2;

    public bool hovering = false;
    public bool creditsspawned = false;
    public bool lvlselectspawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            clicked = true;
        }
        else if (clicked)
        {
            clicked = false;
        }

        //Scale if hover over
        /*
        if (hovering) { lastHit.transform.localScale = new Vector3(15, 15, 10); } 
        else if( lastHit!=null) {
            lastHit.transform.localScale = new Vector3(10, 10, 10);
        }
        */
        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out hit, 100, uiLayerMask))
        {
            lastHit = hit.transform;
            //hovering = true;

            if (clicked)
            {
                switch (hit.transform.gameObject.layer)
                {
                    case 15:
                        if (!lvlselectspawned)
                        {
                            showLevel.SetActive(true);
                            lvltut.GetComponent<Rigidbody>().useGravity = true;
                            lvl1.GetComponent<Rigidbody>().useGravity = true;
                            lvl2.GetComponent<Rigidbody>().useGravity = true;
                            //lvl3.GetComponent<Rigidbody>().useGravity = true;
                            lvlselectspawned = true;
                        }
                        break;
                    case 16:
                        Quit();
                        break;
                    case 17:
                        if (!creditsspawned)
                        {
                            showCredits.SetActive(true);
                            showCredits.GetComponent<Rigidbody>().useGravity = true;

                            creditsspawned = true;
                        }
                        break;
                    case 18:
                        Camera.main.GetComponent<SceneChanger>().startPacking(1);
                        break;
                    case 19:
                        Camera.main.GetComponent<SceneChanger>().startPacking(2);
                        break;
                    
                    case 21:
                        Camera.main.GetComponent<SceneChanger>().startPacking(0);
                        break;
                }
            }
            else if (lastHit.transform != null)
            {
                hovering = false;
            }

        }

    }
    public void Quit() { 
    
    Application.Quit();
    }
}
