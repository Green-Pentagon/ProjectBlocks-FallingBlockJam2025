using System.Collections;
using UnityEngine;

public class DestroyerBehaviour : MonoBehaviour
{
    public float speed = 0.01f;
    public float startDelay = 3.0f;
    bool allowMovement = false;
    //public float endSpeed = 0.10f;
    //public float speedIncrement = 0.01f;
    //public float speedIncreaseDelay = 5.0f;
    //public float moveDelay = 0.01f;
    //float curSpeed = 0.01f;
    //bool triggerSpeedUp = true;
    //bool moving = false;

    //IEnumerator Move()
    //{
    //    moving = true;
    //    transform.position = transform.position + new Vector3 (0, curSpeed, 0);
    //    yield return new WaitForSeconds(moveDelay);
    //    moving = false;
    //}

    //IEnumerator SpeedUp()
    //{
    //    triggerSpeedUp = false;
    //    curSpeed += speedIncrement;
    //    yield return new WaitForSeconds(speedIncreaseDelay);
    //    triggerSpeedUp = true;
    //}

    IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(startDelay);
        allowMovement = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //curSpeed = startSpeed;
        StartCoroutine(DelayMovement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (allowMovement)
        {
            transform.position = transform.position + new Vector3(0, speed, 0);
        }
        //if (triggerSpeedUp && curSpeed < endSpeed)
        //{
        //    StartCoroutine(SpeedUp());
        //    Debug.Log("Destroyer sped up from:" + (curSpeed-speedIncrement) + " to:" + curSpeed);
        //}

        //if (!moving)
        //{
        //    StartCoroutine(Move());
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Destroy(collision.gameObject);
        }
    }
}
