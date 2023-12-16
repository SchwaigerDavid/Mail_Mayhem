using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class damageLogic : MonoBehaviour
{
public double durability; // how much damage can the Object take, before its destroyed
public double damage_resistance; // how much force can the Object absorb before it gets damaged
public string relevant_colliding_game_tag = "Cussioning"; // if object collides with object with this tag, it takes less damage 

// also relevant: mass (how much mass does the Object have --> more mass --> more force --> more damage)
public Rigidbody2D myRigidbody2D;

    private void OnCollisionEnter2D (Collision2D collision){
        GameObject object_collided_with = collision.collider.GameObject();
        
        // float impactForce = collision.relativeVelocity.magnitude * myRigidbody2D.mass; // previous version; problem: if cussion falls on object, both take damage
        float impactForce = myRigidbody2D.velocity.magnitude * myRigidbody2D.mass;

        
        if (object_collided_with.tag == relevant_colliding_game_tag ) {
            // collision with obsorbant object
            // let it absorb some force, before calculating damage
            forceReductionLogic script = object_collided_with.GetComponent<forceReductionLogic>();
            double reduced_force = script.reduce_force(impactForce, transform.position);
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
    }   
}