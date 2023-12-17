using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CollisionSoundPlayer : MonoBehaviour
{
    private AudioSource sound;
    private AudioClip clip;
    public string selfName;

    private string[][] box_v_box;
    private string[][] box_v_floor;
    private string[][] glass_ceramic;
    private string[][] object_v_box;
    private string[][] packaging_paper;
    private string[][] seal_plush;
    private string[][] styrofoam;

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

    public void play_sound(double impactForce) {
        string[][] coll_sounds = null;

        switch(selfName) {
            case "glass_ceramic":
                coll_sounds = glass_ceramic; break;
            case "seal_plush":
                coll_sounds = seal_plush; break;
        }
        if(coll_sounds != null) {
            int intensity;
            Debug.Log(impactForce);
            if(impactForce < 0.05) {
                intensity = 0;
            }
            else if(impactForce < 0.2) {
                intensity = 1;
            }
            else {
                intensity = 2;
            }

            int random_choice = Random.Range(0, coll_sounds[intensity].Length);
            string clip_name = coll_sounds[intensity][random_choice];
            clip_name = clip_name.Replace("Assets/Resources/", "");
            clip_name = clip_name.Replace(".wav", "");
            

            clip = Resources.Load<AudioClip>(clip_name);
            sound.PlayOneShot(clip);
        }
    }

    private string[][] get_sound_paths(string path) {
        string[][] result = new string[3][];
        string[] intensities = {"light", "medium", "heavy"};

        for(int i = 0; i < 3; i++) {
            result[i] = AssetDatabase.FindAssets("", new[] {"Assets/Resources/Sound/"+path+"/"+intensities[i]});
            for(int j = 0; j < result[i].GetLength(0); j++) {
                result[i][j] = AssetDatabase.GUIDToAssetPath(result[i][j]);
            }
        }

        return result;
    }
}