using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvManager : MonoBehaviour
{
    public GameObject[] lvPrefabs;
    public float zSpawn=0;
    public float roadLength=18;
    public int numberOfLevel=6;
    public Transform playerTransform;
    public List<GameObject> activeLv = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberOfLevel; i++)
        {
            if (i == 0)
            {
                SpawnLv(0);
            }
            else
            {
                SpawnLv(Random.Range(0, lvPrefabs.Length));
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z -25 > zSpawn - (numberOfLevel * roadLength))
        {
            SpawnLv(Random.Range(0, lvPrefabs.Length));
            DeleteLv();
        }
    }

    public void SpawnLv(int lvIndex)
    {
        GameObject go = Instantiate(lvPrefabs[lvIndex], transform.forward * zSpawn, transform.rotation);
        activeLv.Add(go);
        zSpawn += roadLength;
    }
    private void DeleteLv()
    {
        Destroy(activeLv[0]);
        activeLv.RemoveAt(0);
    }
}
