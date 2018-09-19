using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Transform mLookAt;
    Vector3 mStartingOffset = Vector3.zero;
    Vector3 mMovementVector = Vector3.zero;

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
