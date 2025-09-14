using UnityEngine;

public class CameraBahaviour : MonoBehaviour
{
    public Transform attachedPlayer;
    public Vector2 cameraOffset;
    Camera thisCamera;
    // Use this for initialization
    void Start()
    {
        thisCamera = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player = attachedPlayer.transform.position;
        Vector3 newCamPos = new Vector3(player.x + cameraOffset.x, player.y + cameraOffset.y, transform.position.z);
        transform.position = newCamPos;
    }
}
