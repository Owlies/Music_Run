using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour {

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

        if (gameObject.CompareTag("leftTap")) {
            other.SendMessage("enableLeftTapJump");
        } else if (gameObject.CompareTag("rightTap")) {
            other.SendMessage("enableRightTapJump");
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }

        if (gameObject.CompareTag("leftTap")) {
            other.SendMessage("disableLeftTapJump");
        }
        else if (gameObject.CompareTag("rightTap")) {
            other.SendMessage("disableRightTapJump");
        }
    }
}
