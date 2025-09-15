using System.Collections;
using UnityEngine;

public class FreezeBlock : MonoBehaviour
{
    Rigidbody2D rb;
    public const float freezeArmDelay = 1.0f;
    private bool armed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator ArmAfterDelay() {
        yield return new WaitForSeconds(freezeArmDelay);
        armed = true;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ArmAfterDelay());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (armed && collision.collider.tag != "Player")
        {
            Destroy(rb);
        }
    }

}
