using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class DropperBehaviour : MonoBehaviour
{
    public AudioSource spawnSound;
    public GameObject[] shapePrefabs;
    public float startDelay = 3.0f;
    public float endDelay = 0.5f;
    public float delayDelta = 0.05f;
    
    Vector3 offset = new Vector3(0.5f, 0.0f, 0.0f);
    int[] spawnerRange = { 0, 10 };
    int[] rotations = {0,90,180,270};
    int cRot;
    float curSpawnDelay;
    bool triggerSpawn = true;

    IEnumerator SpawnDelay()
    {
        triggerSpawn = false;
        yield return new WaitForSeconds(curSpawnDelay);
        triggerSpawn = true;
        if (curSpawnDelay > endDelay)
        {
            curSpawnDelay -= delayDelta;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curSpawnDelay = startDelay;
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
            spawnSound.Play();
            transform.position = new Vector3((int)((Random.Range(spawnerRange[0],(spawnerRange[1] / 10.0f) )) * 10),transform.position.y,transform.position.z);
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
