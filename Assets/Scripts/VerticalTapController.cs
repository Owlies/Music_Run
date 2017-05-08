using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTapController : MonoBehaviour {
	public GameObject nextTarget;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }

		if(nextTarget == null) {
			return;
		}
        other.SendMessage("setVerticalSpeedTowardsTarget", nextTarget.transform.position);
    }
}
