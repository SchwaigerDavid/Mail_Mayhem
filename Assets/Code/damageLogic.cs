using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class damageLogic : MonoBehaviour
{
public double durability; // how much damage can the Object take, before its destroyed
public double damage_resistance; // how much force can the Object absorb before it gets damaged
public string relevant_colliding_game_tag = "Cussioning"; // if object collides with object with this tag, it takes less damage 

public GameObject hotSwapObject; // if the object is destroyed, it is replaced with this object

// also relevant: mass (how much mass does the Object have --> more mass --> more force --> more damage)
public Rigidbody myRigidbody;

    private void OnCollisionEnter (Collision collision){
        GameObject object_collided_with = collision.collider.GameObject();
        
        // float impactForce = collision.relativeVelocity.magnitude * myRigidbody2D.mass; // previous version; problem: if cussion falls on object, both take damage
        float impactForce = myRigidbody.velocity.magnitude * myRigidbody.mass;
        
        
        if (object_collided_with.tag == relevant_colliding_game_tag ) {
            Debug.Log("relevant collision");
            // collision with obsorbant object
            // let it absorb some force, before calculating damage
            forceReductionLogic script = object_collided_with.GetComponent<forceReductionLogic>();
            double reduced_force = script.reduce_force(impactForce, transform.position);
            Debug.Log("reduced_force: " + reduced_force);
            calculate_if_takes_damage(reduced_force);
        } else {
            // collision with non-absorbant object
            // calculate damage to current object without absorbing any force
            calculate_if_takes_damage(impactForce);
        }    
    }

public void calculate_if_takes_damage(double impactForce){
    //Debug.Log("impactForce: " + impactForce);
        if (impactForce > damage_resistance) {
            durability -= impactForce-damage_resistance;
       }
       if (durability <= 0){
           // object is destroyed
           if (hotSwapObject != null){
               // if there is a hotSwapObject, it is spawned in the place of the destroyed object
               GameObject new_object = Instantiate(hotSwapObject, transform.position, transform.rotation);
           }
           Destroy(gameObject);
       }
    }   
}