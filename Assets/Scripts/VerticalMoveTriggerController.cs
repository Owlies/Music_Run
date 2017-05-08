using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMoveTriggerController : MonoBehaviour {
	public bool enableMove = true;
	public GameObject nextTarget;
    public Camera mainCamera;
    public float offsetY = 0.0f;
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

        if(enableMove) {
            mainCamera.GetComponent<CameraController>().offsetY += offsetY;
            other.SendMessage("setVerticalSpeedTowardsTarget", nextTarget.transform.position);
		} else {
            mainCamera.GetComponent<CameraController>().offsetY -= offsetY;
            other.SendMessage("setVerticalSpeedToZero");
		}
    }
}
