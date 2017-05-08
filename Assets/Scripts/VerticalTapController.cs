using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTapController : MonoBehaviour {
	public GameObject nextTarget;
    public KeyCode keyCode;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) {
            return;
        }

        if (nextTarget == null) {
            return;
        }

        if (Input.GetKeyDown(keyCode) || Input.GetKey(keyCode)) {
            collision.SendMessage("setVerticalSpeedTowardsTarget", nextTarget.transform.position);
        }
    }
}
