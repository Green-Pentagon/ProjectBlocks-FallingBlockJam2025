using UnityEngine;

public class CameraBahaviour : MonoBehaviour
{
    public Transform attachedPlayer;
    public Vector2 CameraYLowerUpperBounds;
    Camera thisCamera;
    bool deathTrigger = false;
    // Use this for initialization
    void Start()
    {
        thisCamera = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!attachedPlayer.GetComponent<PlayerController>().IsPlayerDead())
        {
            Vector3 player = attachedPlayer.transform.position;
            float deltaY = transform.position.y - player.y;
            Vector3 newCamPos = transform.position;

            //NOTE: THIS BEHAVIOUR WILL LIKELY BREAK WITH NEGATIVE Y VALUES!
            if (deltaY > CameraYLowerUpperBounds.y)//if higher than allowed upper bound, start moving camera up
            {
                newCamPos = new Vector3(transform.position.x, player.y + CameraYLowerUpperBounds.y, transform.position.z);
            }
            if (deltaY < CameraYLowerUpperBounds.x)//if higher than allowed lower bounds, start moving camera up
            {
                newCamPos = new Vector3(transform.position.x, player.y + CameraYLowerUpperBounds.x, transform.position.z);
            }

            //Vector3 newCamPos = new Vector3(transform.position.x, player.y + yCameraOffsetRange.y, transform.position.z);
            transform.position = newCamPos;
        }
        else if (!deathTrigger) {
            deathTrigger = true;
            GetComponentInChildren<DropperBehaviour>().enabled = false;
            //GetComponent<AudioListener>().enabled = false;
        }
        
    }
}
