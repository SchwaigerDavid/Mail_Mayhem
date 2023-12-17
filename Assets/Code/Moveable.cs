using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moveable : MonoBehaviour
{

    private Vector3 mousePos;
    private Vector3 worldPosition; 
    private RaycastHit hit;
    private Transform lastHit;

    private bool clicking = false;
    private bool moving = false;

    public LayerMask layermaskIgnored;
    public GameObject[] levels;
    // Start is called before the first frame update
    void Start()
    {
        

            switch (GameManager.instance.level)
            {
                case 0:
                levels[0].SetActive(true);
                break;
                case 1:
                    levels[1].SetActive(true);
                    break;
                case 2:
                    levels[2].SetActive(true);
                    break;

            }
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        // TODO Wenn maus zu schnell dann soll objekt weiter hinterher bis Mausknopf wieder losgelassen wird
        // Möglichkeit das zu machen wäre einen Boolean zu verwenden der true gesetzt wird wenn spieler objekt anklickt und false wenn spieler die Maustaste loslässt. 
        if (Input.GetMouseButton(0))
        {

            clicking = true;
            //DontDestroyOnLoad(gameObject);

        }
        else if (clicking)
        {
            clicking = false;
        }
        if (Physics.Raycast(ray, out hit, 100,layermaskIgnored) && !moving) {
            lastHit = hit.transform;
            moving = true;
        }

        if ((moving&&clicking)&& lastHit != null )
        {
            mousePos.z = 1.15f;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
           // Debug.Log(mousePos); 
            lastHit.GetComponent<Rigidbody>().useGravity = false;
            lastHit.GetComponent<Rigidbody>().freezeRotation = true;
            lastHit.position = worldPosition;
        }
        else if(lastHit != null) { 
            lastHit.GetComponent<Rigidbody>().useGravity = true;
            lastHit.transform.GetComponent<Rigidbody>().freezeRotation = false;
            lastHit = null;
            moving = false;
        }
       
    }

}
