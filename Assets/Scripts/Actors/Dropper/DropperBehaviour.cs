using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class DropperBehaviour : MonoBehaviour
{
    public GameObject[] shapePrefabs;
    private int[] rotations = {0,90,180,270};
    float spawnDelay = 3.0f;
    bool triggerSpawn = true;

    IEnumerator SpawnDelay()
    {
        triggerSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        triggerSpawn = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (shapePrefabs.Length == 0)
        {
            Debug.LogError("DropperBehaviour.cs: shapePrefabs array is empty!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerSpawn)
        {
            StartCoroutine(SpawnDelay());
            Debug.Log("Object Spawned!");
            GameObject obj = Instantiate(shapePrefabs[(int)((Random.Range(0.0f, shapePrefabs.Length / 10.0f)) * 10)]);
            obj.transform.position = transform.position;
            obj.transform.Rotate(new Vector3(0.0f,0.0f,rotations[(int)((Random.Range(0.0f, rotations.Length / 10.0f)) * 10)]));
            
        }
    }
}
