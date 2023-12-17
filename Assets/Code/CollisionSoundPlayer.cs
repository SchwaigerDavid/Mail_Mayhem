using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSoundPlayer : MonoBehaviour
{
    private AudioSource sound;
    public string selfName;

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

    public void play_sound(bool soft, double impactForce) {
        AudioClip[][] coll_sounds = null;
        if(soft) {
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

                sound.PlayOneShot(choose_random_sound(packaging_paper, intensity));
        }
        else {
            switch(selfName) {
                case "glass_ceramic":
                    coll_sounds = glass_ceramic; break;
                case "seal_plush":
                    coll_sounds = seal_plush; break;
            }
            if(coll_sounds != null) {
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

                sound.PlayOneShot(choose_random_sound(coll_sounds, intensity));
                sound.PlayOneShot(choose_random_sound(object_v_box, intensity));
            }
        }
    }

    private AudioClip[][] get_sound_paths(string path) {
        AudioClip[][] result = new AudioClip[3][];
        string[] intensities = {"light", "medium", "heavy"};

        for(int i = 0; i < 3; i++) {
            AudioClip[] resources = Resources.LoadAll<AudioClip>("Sound/"+path+"/"+intensities[i]);
            int j = 0;
            result[i] = new AudioClip[resources.Length];
            foreach(var r in resources) {
                result[i][j] = r;
                j++;
            }
        }

        return result;
    }

    private AudioClip choose_random_sound(AudioClip[][] sound_files, int intensity) {
        int random_choice = Random.Range(0, sound_files[intensity].Length);
        AudioClip clip = sound_files[intensity][random_choice];
        return clip;
    }
}