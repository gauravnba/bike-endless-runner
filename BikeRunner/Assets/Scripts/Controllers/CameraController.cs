using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Transform mLookAt;                          // The transform of the object the camera is supposed to be looking at. In this case, the player.
    Vector3 mStartingOffset = Vector3.zero;     // The offset at which the camera will remain, from the starting of the game.
    Vector3 mMovementVector = Vector3.zero;     // The position that the camera is supposed to be in at the time.

    // Use this for initialization
    void Start() {
        mLookAt = GameObject.FindGameObjectWithTag("Player").transform;
        mStartingOffset = transform.position - mLookAt.position;
        mMovementVector.x = transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        mMovementVector = mLookAt.position + mStartingOffset;
        mMovementVector.x = 0;
        transform.position = mMovementVector;
	}
}
