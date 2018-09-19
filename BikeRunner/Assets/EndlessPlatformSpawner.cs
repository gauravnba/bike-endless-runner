using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPlatformSpawner : MonoBehaviour {
    [SerializeField]
    GameObject[] Platforms;                 // Array of spawnable platforms to pick from.

    [SerializeField]
    int MaxNumOfPlatforms = 10;              // The maximum number of platforms that we want to spawn and hold simultaneously.

    float mSpawnLocationZ = 8.0f;           // The z coordinate of the spawn location for the current platform.

    Transform mPlayerTransform;             // The transform of the player character.
    List<GameObject> mPlatformsPool;        // An object pool for the platforms.
    int mLastPlatformIndex = 0;

    const float PLATFORM_LENGTH = 20.0f;    // Length of the each of the platforms in metres.

	// Use this for initialization
	void Start () {
        mPlatformsPool = new List<GameObject>();
        mPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < MaxNumOfPlatforms; ++i)
        {
            // Spawn maximum number of platforms. The first one should always be of index 0.
            SpawnPlatform((i==0) ? 0 : RandomPlatformIndex());
        }
    }
	
	// Update is called once per frame
	void Update () {
		if ((mPlayerTransform.position.z - PLATFORM_LENGTH) > (mSpawnLocationZ - MaxNumOfPlatforms * PLATFORM_LENGTH))
        {
            SpawnPlatform(RandomPlatformIndex());
            RemovePlatform();
        }
	}

    void SpawnPlatform(int platformIndex) {
        GameObject platform = Instantiate(Platforms[platformIndex]) as GameObject;
        platform.transform.SetParent(transform);
        platform.transform.position = Vector3.forward * mSpawnLocationZ;

        mSpawnLocationZ += PLATFORM_LENGTH;
        mPlatformsPool.Add(platform);
    }

    void RemovePlatform()
    {
        Destroy(mPlatformsPool[0]);
        mPlatformsPool.RemoveAt(0);
    }

    int RandomPlatformIndex()
    {
        if (Platforms.Length <= 1)
        {
            return 0;
        }

        int randomIndex = mLastPlatformIndex;
        while (randomIndex == mLastPlatformIndex)
        {
            randomIndex = Random.Range(0, Platforms.Length);
        }
        mLastPlatformIndex = randomIndex;

        return randomIndex;
    }
}
