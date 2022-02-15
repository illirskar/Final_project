using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    float verAxis;
    [Range(1f,5f)]
    public float jumpMult;
    [Range(1f, 10f)]
    public float moveSpeed;
    [SerializeField] 
    [Range(0f, 1f)]
    float LerpMod;

    bool grounded;
    bool onIce;
    bool doubleJump;

    bool wallJump;
    Vector3 direction;

    Rigidbody rigidbody;

    public AudioSource jump;
    public AudioSource swoosh;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Climbable"))
        {
            direction = collision.contacts[0].point - transform.position;
            direction = -direction.normalized;
        }

        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Ice"))
        {
            doubleJump = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // Wall check to avoid having player jump up every wall, unless they're marked accordingly.
        { grounded = false; }
        else { grounded = true; }

        if (collision.gameObject.CompareTag("Ice")) // Tag check for ice to add "slippery" movement.
        { onIce = true; }
        else { onIce = false; }

        if (collision.gameObject.CompareTag("Climbable")) // For walls the player should be able to jump away from.
        { wallJump = true; }
        else { wallJump = false; }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
        wallJump = false;
    }

    void Update()
    {
        //Jumping
        if (grounded && Input.GetKeyDown(KeyCode.Space) == true)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpMult * 10f); // Modifier is added to counteract gravity modifier.
            jump.Play();
        }
        else if (wallJump && Input.GetKeyDown(KeyCode.Space) == true)
        {
            WallJump(direction);
            jump.Play();
        }
        else if (!doubleJump && Input.GetKeyDown(KeyCode.Space) == true)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpMult * 10f); // Modifier is added to counteract gravity modifier.
            doubleJump = true;
            jump.Play();
        }
        
        else if (Input.GetKeyUp(KeyCode.Space))//Checks if key was released.
        {
            if (rigidbody.velocity.y > 0.0f)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y / 2); // Dividing Y by 2 makes the jump look shorter.
            }
        }

        //Horizontal movement
        float horAxis;
        horAxis = Input.GetAxis("Horizontal");

        if (onIce && rigidbody.velocity.magnitude < 15f) { // This check is implemented to avoid launching the object at faster-than-light speeds.
            Vector3 movement = new Vector3(horAxis * moveSpeed/2, 0); 
            rigidbody.AddForce(Vector3.Lerp(rigidbody.velocity, movement, LerpMod*2));
        }
        else {
            Vector3 movement = new Vector3(horAxis * moveSpeed, rigidbody.velocity.y);
            rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, movement, LerpMod);
        };
    }

    private void WallJump(Vector3 direction)
    {
        rigidbody.velocity = new Vector3(direction.x * 10f, jumpMult * 10f); //direction.x replaces velocity.x from normal jump formula, since velocity of gameobject in horizontal plane will be equal to 0.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Teleport"))
        {
            swoosh.Play();
        } 
    }
    
}
