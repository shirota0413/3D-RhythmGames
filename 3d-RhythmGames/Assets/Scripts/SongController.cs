using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour {
    AudioSource Music;
    public string ClipPath;

    //Start is called before the first frame update
    void Start() {
        Music = this.GetComponent<AudioSource>();
        Music.clip = (AudioClip)Resources.Load(ClipPath);
        Music.Play();
    }
}
