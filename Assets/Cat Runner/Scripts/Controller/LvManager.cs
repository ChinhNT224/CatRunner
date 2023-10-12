using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvManager : MonoBehaviour
{
    public GameObject[] lvPrefabs1;
    public GameObject[] lvPrefabs2;
    public string playerTag = "Player";
    public float zSpawn = 0;
    public float roadLength = 120;
    public int numberOfLevel = 2;
    public Transform playerTransform;
    public List<GameObject> activeLv = new List<GameObject>();
    private GameObject[] activeLvPrefabs;
    private bool useLvPrefabs1 = true;
    private Vector3 lastPrefabPosition;

    void Start()
    {
        GameObject player = GameObject.FindWithTag(playerTag);
        playerTransform = player.transform;

        activeLvPrefabs = lvPrefabs1;

        for (int i = 0; i < numberOfLevel; i++)
        {
            if (i == 0)
            {
                SpawnLv(activeLvPrefabs[0]);
            }
            else
            {
                int randomIndex = Random.Range(0, activeLvPrefabs.Length);
                SpawnLv(activeLvPrefabs[randomIndex]);
            }
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 200 > zSpawn - (numberOfLevel * roadLength))
        {
            int randomIndex = Random.Range(0, activeLvPrefabs.Length);
            SpawnLv(activeLvPrefabs[randomIndex]);
            DeleteLv();
        }
    }

    public void SpawnLv(GameObject lvPrefab)
    {
        GameObject go = Instantiate(lvPrefab, transform.forward * zSpawn, transform.rotation);
        activeLv.Add(go);
        lastPrefabPosition = go.transform.position; 
        zSpawn += roadLength;
    }

    private void DeleteLv()
    {
        Destroy(activeLv[0]);
        activeLv.RemoveAt(0);
    }

    public void Portal1(Vector3 lvPosition)
    {
        useLvPrefabs1 = true;
        activeLvPrefabs = lvPrefabs1;

        DeleteAllLv();

        Vector3 startPosition = lvPosition; 

        for (int i = 0; i < numberOfLevel; i++)
        {
            int randomIndex = Random.Range(0, activeLvPrefabs.Length);
            SpawnLv(activeLvPrefabs[randomIndex]);
            activeLv[activeLv.Count - 1].transform.position = startPosition; // Đặt vị trí xuất phát
            startPosition.z += roadLength;
        }

        useLvPrefabs1 = true;
        activeLvPrefabs = lvPrefabs1;
        zSpawn = 0;
    }

    public void Portal2(Vector3 lvPosition)
    {
        useLvPrefabs1 = false;
        activeLvPrefabs = lvPrefabs2;

        DeleteAllLv();

        Vector3 startPosition = lvPosition;

        for (int i = 0; i < numberOfLevel; i++)
        {
            int randomIndex = Random.Range(0, activeLvPrefabs.Length);
            SpawnLv(activeLvPrefabs[randomIndex]);
            activeLv[activeLv.Count - 1].transform.position = startPosition;
            startPosition.z += roadLength;
        }

        useLvPrefabs1 = false;
        activeLvPrefabs = lvPrefabs2;
        zSpawn = 0;
    }

    private void DeleteAllLv()
    {
        foreach (var lv in activeLv)
        {
            Destroy(lv);
        }
        activeLv.Clear();

        lastPrefabPosition = new Vector3(transform.position.x, transform.position.y, zSpawn);
    }
}
