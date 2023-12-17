using System.Collections;
using System.Collections.Generic;
//using System.Numerics; not using this, but if i delete it, it wats to reimport it so i just leave it here commented out to trick vs code
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class forceReductionLogic : MonoBehaviour
{
    public double force_absorption; // how much force is absorbed from the object that crashed into the force absorber
    public double damage_resistance; // The minimum amount of force that can be absorbed before the object
    public double durability; // how much damage the absorber can take before it is destroyed

    public string type = "Styrofoam"; // what type of object is this? [Newspaper, Styrofoam, Beans]

    private bool isSplit = false; // for Styrofoam, if it did already split in half, it can't split again

    public Collider myCollider;

    private double indestructible_time = 0; // how long is the object indestructible after it split in half

    public double reduce_force(double impactForce, Vector3 positionOfHit){
        // reduces the impactForce and takes damage if the force is too high
        if (indestructible_time > 0) {return 0;} // if the object is indestructible, it is a newly split styrofoam and it absorbs all force for one second
        double new_force = impactForce - force_absorption;
        handle_damage_depending_on_type(impactForce, positionOfHit);
        return new_force;
    }

    private void take_linear_damage(double impactForce){
        if (impactForce > damage_resistance) {
            if (durability < 0) {return;}
            // if to much force got absorbed, the absorbing_object itself takes damage
            durability -= impactForce-damage_resistance-force_absorption;
        }
    }

    private void split_in_half(Vector3 positionOfHit){
        if (isSplit) {
            Destroy(gameObject);
            return;
        }

        // splits the object in half, with reduced force absorption and durability
        float resize_factor = 0.4f;
        float strength_factor = 1/3;
        // creates two new objects, with half the durability and half the force_absorption
            // get size of object -> Fetch the Collider from the GameObject -> Fetch the size of the Collider volume
        Bounds bounds = myCollider.bounds;
        Vector3 size = bounds.size;
            // Center of new objects is collision position +- ((width/2 -+ distanceFromCenterToCollisionPosition)/2)
        Vector3 center1 = positionOfHit + size / 4; 
        Vector3 center2 = positionOfHit - size / 4;
            // generate them 
        GameObject subobject1 = Instantiate(gameObject, center1, transform.rotation);
        GameObject subobject2 = Instantiate(gameObject, center2, transform.rotation);
            // resize them
        subobject1.transform.localScale = new Vector3 (gameObject.transform.localScale.x, gameObject.transform.localScale.y * resize_factor, gameObject.transform.localScale.z);
        subobject2.transform.localScale = new Vector3 (gameObject.transform.localScale.x, gameObject.transform.localScale.y * resize_factor, gameObject.transform.localScale.z);
            // edit their mass
        Rigidbody ridg1 = subobject1.GetComponent<Rigidbody>();
        ridg1.mass = ridg1.mass*resize_factor;
        Rigidbody ridg2 = subobject2.GetComponent<Rigidbody>();
        ridg2.mass = ridg2.mass*resize_factor;
            // create two new objects with half the durability and half the force_absorption
        forceReductionLogic script1 = subobject1.GetComponent<forceReductionLogic>();   
        script1.force_absorption *= strength_factor;
        script1.damage_resistance += strength_factor;
        script1.isSplit = true;
        script1.indestructible_time = 1;
        forceReductionLogic script2 = subobject2.GetComponent<forceReductionLogic>();   
        script2.force_absorption *= strength_factor;
        script2.damage_resistance += strength_factor;
        script2.isSplit = true;
        script2.indestructible_time = 1;
            // and destroy the game object
        Destroy(gameObject);
    }

    private void handle_damage_depending_on_type(double impactForce, Vector3 positionOfHit){
        if (durability <= 0) {return;}
        // depenping on the type of object, it takes damage differently
        // a newspaper can take damage multiple times, without its force_absorption being reduced, if its durability drops below 0, there is no more force absorption
        // a styrofoam can take a small amount of damage, infinetly, but if the force is too high, it gets split in half with reduced absorption and health capacity
        // beans can take damage multiple times, but their absorption level is reduced every time they need to absorb force
        // bubblewrap can take a small amount of damage, infinetly, but if the force is too high, it simply pops and can't absorb any more force
        double vulnerable_force = impactForce-damage_resistance;
        if (type == "Newspaper"){
            take_linear_damage(impactForce);
            if (durability <= 0) {
                force_absorption = 0;
            }
        } else if (type == "Styrofoam"){
            // easy hits, nothing happens, but if the force is too high, the styrofoam splits in half
            if (vulnerable_force > damage_resistance){
                split_in_half(positionOfHit);
                //SplitGameObject(positionOfHit);
            }
        } else if (type == "Beans"){
            // force absorption is directly proportional to durability
            force_absorption -= vulnerable_force * (force_absorption/durability);
            take_linear_damage(impactForce);    
        } else {
            Debug.LogError("type not found");
        }
        
    }

    void Update() {
        if (indestructible_time > 0){
            indestructible_time -= Time.deltaTime;
        }
    }
    
}
