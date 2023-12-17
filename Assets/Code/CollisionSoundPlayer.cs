using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSoundPlayer : MonoBehaviour
{
    private AudioSource sound;

    private AudioClip[][] box_v_box;
    private AudioClip[][] box_v_floor;
    private AudioClip[][] glass_ceramic;
    private AudioClip[][] object_v_box;
    private AudioClip[][] packaging_paper;
    private AudioClip[][] seal_plush;
    private AudioClip[][] styrofoam;

    void Start() {
        sound = GetComponent<AudioSource>();

        box_v_box = get_sound_paths("box_v_box");
        box_v_floor = get_sound_paths("box_v_floor");
        glass_ceramic = get_sound_paths("glass_ceramic");
        object_v_box = get_sound_paths("object_v_box");
        packaging_paper = get_sound_paths("packaging_paper");
        seal_plush = get_sound_paths("seal_plush");
        styrofoam = get_sound_paths("styrofoam");
    }

    public void play_sound(string otherObjName, double impactForce) {
        AudioClip[][] coll_sounds = null;

        int intensity;
        Debug.Log(impactForce);
        if(impactForce < 0.15) {
            intensity = 0;
        }
        else if(impactForce < 0.5) {
            intensity = 1;
        }
        else {
            intensity = 2;
        }

        bool dampened = false;

        // Collision sounds with specific object
        if(otherObjName.StartsWith("CartonBox")) {
            coll_sounds = object_v_box;
        }
        else if(otherObjName.StartsWith("newspaper")) {
            coll_sounds = packaging_paper;
            dampened = true;
        }
        else if(otherObjName.StartsWith("Styrofoam")) {
            coll_sounds = styrofoam;
            dampened = true;
        }

        if(coll_sounds != null) {
                sound.PlayOneShot(choose_random_sound(coll_sounds, intensity));
        
            }

        // Object's own noise
        if(dampened == false) {
            if(name.StartsWith("bigvase")) {
                coll_sounds = glass_ceramic;
            }
            else if(name.StartsWith("sealplush")) {
                coll_sounds = seal_plush;
            }
            
            if(coll_sounds != null) {
                sound.PlayOneShot(choose_random_sound(coll_sounds, intensity));
        
            }
        }
    }

    private AudioClip[][] get_sound_paths(string path) {
        string[] intensities = {"light", "medium", "heavy"};
        AudioClip[][] result = new AudioClip[intensities.Length][];

        for(int i = 0; i < intensities.Length; i++) {
            result[i] = Resources.LoadAll<AudioClip>("Sound/"+path+"/"+intensities[i]);
        }

        return result;
    }

    private AudioClip choose_random_sound(AudioClip[][] sound_files, int intensity) {
        int random_choice = Random.Range(0, sound_files[intensity].Length);
        AudioClip clip = sound_files[intensity][random_choice];

        return clip;
    }

}