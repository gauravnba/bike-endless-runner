using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    float MaxSpeed = 5.0f;                  // Maximum speed the player is allowed to reach
    [SerializeField]
    float Acceleration = 10.0f;             // The acceleration that the player will experience on start and stop.
    [SerializeField]
    float TiltRotation = 15.0f;             // The angle in degrees of rotation, used to tilt the bike when strafing.
    public float Fuel = 100;                // The amount of fuel held by player.

    CharacterController mController;        // Reference to the CharacterController of the player character.
    Vector3 mMovementVector = Vector3.zero; // The movement value that will be updated on the Character Controller every frame.

    const float FUEL_REDUCE_FACTOR = 0.01f; // The factor to be multiplied to the 'velocity vector', to apply fuel reduction.
    const float GRAVITY = 9.8f;             // Value of gravity, used to simulate falling, if a bridge type platform is spawned.

	// Use this for initialization
	void Start () {
        mController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Fuel >= 0)
        {
            UpdateMovement();
        }
        UpdateFuel();
    }

    void UpdateMovement()
    {
        // Update gravity
        if (mController.isGrounded)
        {
            mMovementVector.y = -0.5f;
        }
        else
        {
            mMovementVector.y -= GRAVITY * Time.smoothDeltaTime;
        }

        // Forward and Backward Movement
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            StartCoroutine(Accelerate(true));
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            StartCoroutine(Accelerate(false));
        }
        // Horizontal movement and tilt the bike towards movement.
        mMovementVector.x = Input.GetAxisRaw("Horizontal") * MaxSpeed / 2;
        transform.rotation = Quaternion.AngleAxis(Input.GetAxisRaw("Horizontal") * -TiltRotation, Vector3.forward);

        mController.Move(mMovementVector * Time.deltaTime);
    }

    void UpdateFuel()
    {
        Fuel -= mMovementVector.z * FUEL_REDUCE_FACTOR;

        if (Fuel <= 0)
        {
            mMovementVector.z = 0.0f;
        }
    }

    IEnumerator Accelerate(bool towardsFacing)
    {
        if (towardsFacing)
        {
            while (mMovementVector.z <= MaxSpeed)
            {
                mMovementVector.z = mMovementVector.z + Acceleration * Time.deltaTime;
                yield return null;
            }
            mMovementVector.z = MaxSpeed; //Manually clamp to MaxSpeed
        }
        else
        {
            while (mMovementVector.z >= 0.0f)
            {
                mMovementVector.z = mMovementVector.z - Acceleration * Time.deltaTime;
                yield return null;
            }
            mMovementVector.z = 0.0f; //Manually clamp to 0
        }
    }
}