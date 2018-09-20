using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillFuel : MonoBehaviour {
    [SerializeField]
    float RefillValue = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        PlayerControl playerScript = other.gameObject.GetComponent<PlayerControl>();
        if (playerScript != null)
        {
            playerScript.Fuel += RefillValue;
            Mathf.Clamp(playerScript.Fuel, 0.0f, 100.0f);
        }
        Destroy(gameObject);
    }
}
