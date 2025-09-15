using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 boxExtents;                     //Used for ground detection
    SpriteRenderer spriteRenderer;
    //Animator animator;

    ////-Player physics-
    //float horizMaxSpeed = 15.0f;            //Ground movement speed cap (doewsn't prevent player being launched at higher speeds)
    //float groundControlSpeed = 1.0f;        //Dictates how fast force is applied to the player when they are moving on the ground
    //float airControlSpeed = 30.0f;          //Force responcible for amount of control allowed
    //float airControlMaxForce = 10.0f;       //Maximum force allowed in air, when the player is attempting to move mid-air
    //float sprintSpeedMultiplier = 0.5f;     //Multiplier for how much faster the player can move while sprinting
    //float terminalVelocity = 100.0f;

    //float jumpForce = 10.0f;                //Vertical force component = how much force to add upon a player's jump action.
    //private float fallingSpeed = 1.0f;      //How fast the player falls when holding down when mid-air

    //private int extraJumps = 0;              //How many more jumps the player can do in the air
    //private int extraJumpCount = 0;         //Keeps track of how many extra jumps the player has done

    //private bool dead = false;
    //private string finalLevel = "4";



    ////-Scene information-
    ////Stores name of current & next level (if there is no next level, "END" is stored instead)
    //string curLevel;
    //string nextLevel;

    ////-UI Elements-
    //public TextMeshProUGUI uiDeathText;
    //public TextMeshProUGUI uiLevelInfo;

    ////-Audio Sources-
    public AudioSource jumpSound;
    public AudioSource deathSound;
    //public AudioSource nextLevelSound;
    //public AudioSource crumbleSound;
    //public AudioSource WinSound;



    float moveX = 0.0f;
    float moveY = 0.0f;
    public bool jumped = false;
    public bool grounded;
    float velocityMult = 10.0f;
    Vector2 jumpImpulse = new Vector2(0.0f,10.0f);
    float YVelocityThreshold = -10.1f; //THIS VALUE MUST BE HIGHER THAN jumpImpulse! the vertical velocity something needs to fall to kill the player
    //an impulse of 10.0f vertical provides a maximum jump height of 6 units, allowing the clearing of 5 units if perfectly vertical (unaffected by slidy phys mat)

    IEnumerator DoDeath()
    {
        //dead = true;
        //animator.SetBool("dead", true);
        //deathSound.Play();
        //uiDeathText.enabled = true;
        spriteRenderer.color = Color.red;
        Debug.Log("Player died on impact!");
        yield return new WaitForSeconds(1.0f);// reload the level in 2 seconds
        spriteRenderer.color = Color.white;

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //uiDeathText.enabled = false;

        boxExtents = GetComponent<CapsuleCollider2D>().bounds.extents;// get the extent of the collision box
        //animator = GetComponent<Animator>();


        ////-Scene information-
        //curLevel = SceneManager.GetActiveScene().name;
        //uiLevelInfo.text = curLevel;
        //nextLevel = "Level ";
        //if (curLevel.Substring(curLevel.Length - 1) == finalLevel)
        //{
        //    nextLevel = "END";
        //}
        //else
        //{
        //    nextLevel += (int.Parse(curLevel.Substring(curLevel.Length - 1)) + 1);
        //}
    }

    private void FixedUpdate()
    {
        // check if we are on the ground
        Vector2 bottom = new Vector2(transform.position.x, transform.position.y - boxExtents.y);
        //Vector2 hitBoxSize = new Vector2(boxExtents.x * 1.9f, 0.05f);
        RaycastHit2D result = Physics2D.Raycast(bottom, Vector2.down,0.05f, 1 << LayerMask.NameToLayer("Ground"));//Physics2D.BoxCast(bottom, hitBoxSize, 0.0f, new Vector3(0.0f, -1.0f), 0.0f, 1 << LayerMask.NameToLayer("Ground"));
        grounded = result.collider != null && result.normal.y > 0.9f;

        moveX = Input.GetAxis("Horizontal") * velocityMult;
        moveY = Input.GetAxis("Jump");

        rb.linearVelocity = new Vector2(moveX, rb.linearVelocity.y);

        if (!jumped && moveY > 0.0f && grounded)
        {
            Debug.Log("Jump Triggered");
            rb.AddForce(jumpImpulse, ForceMode2D.Impulse);
            jumped = true;
        }
        else if (jumped && moveY == 0.0f && grounded)
        {
            Debug.Log("Jump Untriggered");
            jumped = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        //check if collision kills with a newly entered collider
        if (collision.relativeVelocity.y < YVelocityThreshold && collision.collider.tag == "Block")
        {
            StartCoroutine(DoDeath());
        }
        
    }
}
