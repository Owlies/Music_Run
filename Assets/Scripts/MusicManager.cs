using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(4.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
