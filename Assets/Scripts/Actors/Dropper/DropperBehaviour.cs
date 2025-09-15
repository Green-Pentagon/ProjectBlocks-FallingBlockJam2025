using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class DropperBehaviour : MonoBehaviour
{

    public GameObject[] shapePrefabs;
    Vector3 offset = new Vector3(0.5f, 0.0f, 0.0f);
    int[] rotations = {0,90,180,270};
    int cRot;
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
            GameObject rndObj = shapePrefabs[(int)((Random.Range(0.0f, shapePrefabs.Length / 10.0f)) * 10)];
            GameObject obj = Instantiate(rndObj);
            
            if (rndObj.name == "piece_2x2")
            {
                cRot = 0;
            }
            else
            {
                cRot = rotations[(int)((Random.Range(0.0f, rotations.Length / 10.0f)) * 10)];
            }
            
            
            
            obj.transform.Rotate(new Vector3(0.0f, 0.0f, cRot));
            if (rndObj.name == "piece_2x2" || (rndObj.name == "piece_4x1" && (cRot == 90 || cRot == 270)) || (rndObj.name != "piece_4x1" && (cRot == 0 || cRot == 180)))
            {
                obj.transform.position = transform.position;
            }
            else
            {
                obj.transform.position = transform.position + offset;
            }
            
            
            
        }
    }
}
