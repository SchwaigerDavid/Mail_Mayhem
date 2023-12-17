using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SquishNewspaper : MonoBehaviour
{
    public string relevant_colliding_game_tag = "player object"; // if object collides with object with this tag, it takes less damage 
    public double threshold = 0.5; // how much force is needed to squish the newspaper

    public float reductionLimit = 0.5f; // how small can the object get before it starts growing again
    public Rigidbody myRigidbody;


    private Vector3 originalScale;
    private float reduce_scale = 0f;

    private bool squish_x; // direction to squish in

    public forceReductionLogic hp_logic;   

    void Update()
    {
        if (reduce_scale != 0){
            if (squish_x){
                // squish in x direction
                transform.localScale = new Vector3(transform.localScale.x - reduce_scale*Time.deltaTime, transform.localScale.y, transform.localScale.z);
                transform.position = new Vector3(transform.position.x - 2*reduce_scale*Time.deltaTime, transform.position.y, transform.position.z);
                // reset scale if it is too small or too big
                if (transform.localScale.x < reductionLimit){
                    reduce_scale = -Math.Abs(reduce_scale);
                    if (hp_logic.force_absorption <= 0){
                        // if all squishiness is gone, the newspaper doesn't grow back
                        reduce_scale = 0;
                    }
                } else if (transform.localScale.x >= originalScale.x){
                    reduce_scale = 0;
                }
            } else {
                // squish in y direction
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - reduce_scale*Time.deltaTime, transform.localScale.z);
                transform.position = new Vector3(transform.position.x, transform.position.y - 2*reduce_scale*Time.deltaTime, transform.position.z);
                // reset scale if it is too small or too big
                if (transform.localScale.y < reductionLimit){
                    reduce_scale = -Math.Abs(reduce_scale);
                    if (hp_logic.force_absorption <= 0){
                        // if all squishiness is gone, the newspaper doesn't grow back
                        reduce_scale = 0;
                    }
                } else if (transform.localScale.y >= originalScale.y){
                    reduce_scale = 0;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision){
        GameObject object_collided_with = collision.collider.GameObject();
        // float impactForce = collision.relativeVelocity.magnitude * myRigidbody2D.mass; // previous version; problem: if cussion falls on object, both take damage
        float impactForce = myRigidbody.velocity.magnitude * myRigidbody.mass;

        Debug.Log("impactForce: " + impactForce);

        if (impactForce > threshold && object_collided_with.tag == relevant_colliding_game_tag && reduce_scale == 0){
            // squish the newspaper
            originalScale = transform.localScale; // set original scale
            reduce_scale = 2f; // how much to reduce scale per second (time until finished = reduce_scale * 2)

            Vector2 impactDirection = object_collided_with.transform.position - transform.position;
            if (Mathf.Abs(impactDirection.x) > Mathf.Abs(impactDirection.y)) {
                // The impact is larger in the X direction
                squish_x = true; //direction to reduce scale = x
            } else {
                // The impact is larger in the Y direction or equal in both directions
                squish_x = false; // direction to reduce scale = y
            }   

        }

    }
}
