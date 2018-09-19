using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField]
    float mMaxVelocity = 7.0f;
    [SerializeField]
    float mAcceleration = 10.0f;
    float mCurrentVelocity = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(Accelerate());
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(Decelerate());
        }
        Debug.Log(mCurrentVelocity);
    }

    IEnumerator Accelerate()
    {
        while (mCurrentVelocity <= mMaxVelocity)
        {
            //Apply linear acceleration to the player.
            mCurrentVelocity = mCurrentVelocity + mAcceleration * Time.smoothDeltaTime;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, mCurrentVelocity);
            yield return null;
        }
        // Manually clamp the velocity
        mCurrentVelocity = mMaxVelocity;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, mCurrentVelocity);
    }

    IEnumerator Decelerate()
    {
        while (mCurrentVelocity >= 0.0f)
        {
            //Apply linear decelaration to the player. We just use the mAcceleration variable to subtract from mCurrentVelocity.
            mCurrentVelocity = mCurrentVelocity - mAcceleration * Time.smoothDeltaTime;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, mCurrentVelocity);
            yield return null;
        }
        // Manually clamp the velocity
        mCurrentVelocity = 0.0f;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, mCurrentVelocity);
    }
}
