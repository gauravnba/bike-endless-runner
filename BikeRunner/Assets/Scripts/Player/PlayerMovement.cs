using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    float MaxSpeed = 5.0f;                  // Maximum speed the player is allowed to reach
    [SerializeField]
    float Acceleration = 10.0f;             // The acceleration that the player will experience on start and stop.

    CharacterController mController;        // Reference to the CharacterController of the player character.
    Vector3 mMovementVector = Vector3.zero; // The movement value that will be updated on the Character Controller every frame.
    float mGravity = 9.8f;

	// Use this for initialization
	void Start () {
        mController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        // Update gravity
        if (mController.isGrounded)
        {
            mMovementVector.y = -0.5f;
        }
        else
        {
            mMovementVector.y -= mGravity * Time.smoothDeltaTime;
        }

        // Forward and Backward Movement
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            StartCoroutine(Accelerate(true));
        }
        if(Input.GetAxisRaw("Vertical") < 0)
        {
            StartCoroutine(Accelerate(false));
        }
        // Horizontal movement.
        mMovementVector.x = Input.GetAxisRaw("Horizontal") * MaxSpeed / 2;

        mController.Move(mMovementVector * Time.deltaTime);
	}

    IEnumerator Accelerate(bool towardsFacing)
    {
        if (towardsFacing)
        {
            while (mMovementVector.z <= MaxSpeed)
            {
                mMovementVector.z = mMovementVector.z + Acceleration * Time.smoothDeltaTime;
                yield return null;
            }
            mMovementVector.z = MaxSpeed; //Manually clamp to MaxSpeed
        }
        else
        {
            while (mMovementVector.z >= 0.0f)
            {
                mMovementVector.z = mMovementVector.z - Acceleration * Time.smoothDeltaTime;
                yield return null;
            }
            mMovementVector.z = 0.0f; //Manually clamp to 0
        }
    }
}