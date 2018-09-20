using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPlatformSpawner : MonoBehaviour {
    [SerializeField]
    GameObject[] Platforms;                 // Array of spawnable platforms to pick from.

    [SerializeField]
    int MaxNumOfPlatforms = 10;             // The maximum number of platforms that we want to spawn and hold simultaneously.

    [SerializeField]
    GameObject FuelRefillPrototype;         // A prototypical object of the FuelRefill prefab, to supply to the Unity Factory pattern.

    float mSpawnLocationZ = 3.0f;           // The z coordinate of the spawn location for the current platform.

    Transform mPlayerTransform;             // The transform of the player character.
    List<GameObject>[] mPlatformsPool;      // An object pool for the platforms.
    int mLastPlatformIndex = 0;             // The type of platform that was used last. Used for pseudo random generation of endless platform.
    List<GameObject> mPlatformsActive;      // The list of platforms, currently in use.

    const float PLATFORM_LENGTH = 10.0f;    // Length of the each of the platforms in metres.
    const int TYPES_OF_PLATFORMS = 5;       // The types of platforms available to us.
    const int POOL_SIZE = 3;                // The initial size of the object pool.

	// Use this for initialization
	void Start () {
        mPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        mPlatformsActive = new List<GameObject>();
        mPlatformsPool = new List<GameObject>[TYPES_OF_PLATFORMS];

        // Populate ObjectPool
        for (int i = 0; i < TYPES_OF_PLATFORMS; ++i)
        {
            List<GameObject> poolOfSingleKind = new List<GameObject>();
            for (int j = 0; j < POOL_SIZE; ++j)
            {
                poolOfSingleKind.Add(Instantiate(Platforms[i]) as GameObject);
                poolOfSingleKind[j].SetActive(false);
            }
            mPlatformsPool[i] = poolOfSingleKind;
        }

        // Spawn maximum number of platforms. The first one should always be of index 0.
        for (int i = 0; i < MaxNumOfPlatforms; ++i)
        {
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
        GameObject platform = GetPooledPlatform(platformIndex);
        mPlatformsActive.Add(platform);
        platform.SetActive(true);
        platform.transform.SetParent(transform);
        platform.transform.position = Vector3.forward * mSpawnLocationZ;

        // Spawn Fuel refill tank, if possible.
        Transform fuelRefillSpawn = platform.transform.Find("FuelSpawnLocation");
        if (fuelRefillSpawn != null)
        {
            GameObject fuel = Instantiate(FuelRefillPrototype) as GameObject;
            fuel.transform.SetParent(platform.transform);
            fuel.transform.position = fuelRefillSpawn.position;
        }
        
        mSpawnLocationZ += PLATFORM_LENGTH;
    }

    GameObject GetPooledPlatform(int platformIndex)
    {
        foreach(GameObject platform in mPlatformsPool[platformIndex])
        {
            if(!platform.activeInHierarchy)
            {
                return platform;
            }
        }

        GameObject newPlatform = Instantiate(Platforms[platformIndex]) as GameObject;
        newPlatform.SetActive(false);
        mPlatformsPool[platformIndex].Add(newPlatform);
        return newPlatform;
    }

    void RemovePlatform()
    {
        // Set the earliest inserted platform as inactive and remove from list.
        mPlatformsActive[0].SetActive(false);
        mPlatformsActive.RemoveAt(0);
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
