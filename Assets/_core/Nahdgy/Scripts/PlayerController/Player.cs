using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    private AudioSource _SoundJump;
    
    [SerializeField]
    private AudioSource _SoundGlissade;

    [Header("Player Settings")]
    [SerializeField]
    public Rigidbody playerRb;

    [SerializeField]
    private float maxSpeed, jumpForce, moveMultiplier, playerHeight, horizontalInput, deplacementDistance, crouchHeight, crouchDelay;
    private float delay = 0.3f;
    public float speed;

    [SerializeField]
    public GameObject player;


    [SerializeField]
    private CapsuleCollider playerCollider;

    public Animator animator;

    [SerializeField]
    private float groundRange;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private bool isGrounded;
    public bool canRun, canMove;

    [SerializeField]
    private Transform playerOriantation;

    private Vector3 runDirection, velocity = Vector3.zero;
    [SerializeField]
    private Vector3 playerPosition;

    public void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
        playerCollider = GetComponent<CapsuleCollider>();
    }

    public void FixedUpdate()
    {
        Run();
        GroundCheck();
    }

    private IEnumerator DeplacementDelay()
    {
        canMove = false;
        yield return new WaitForSeconds(delay);
        canMove = true;
    }

    private IEnumerator LerpMoving()
    {
        float t = 0;
        float duration = 2;
        while (t < duration)
        {
            deplacementDistance = Mathf.Lerp(0, 3, t / duration);
            t += 0.5f * Time.deltaTime;

            yield return null;
        }

        deplacementDistance = 3;
    }
    public  void ChangeCoridore(InputAction.CallbackContext move)
    {
        
        player.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.0f, 3.0f), transform.position.y, transform.position.z);
        if(move.ReadValue<float>() == 1 && canMove)
        {
            Debug.Log("movingRigtht");
            //StartCoroutine(LerpMoving());
            playerPosition = new Vector3(transform.position.x + deplacementDistance, transform.position.y, transform.position.z);
            StartCoroutine(DeplacementDelay());
            playerPosition.x = Mathf.Clamp(playerPosition.x, -3.0f, 3.0f);
            player.transform.position = playerPosition;
        }
        if(move.ReadValue<float>() == -1 && canMove) 
        {
            Debug.Log("movingLeft");
            //StartCoroutine(LerpMoving());
            playerPosition = new Vector3(transform.position.x + -deplacementDistance, transform.position.y, transform.position.z);
            StartCoroutine(DeplacementDelay());
            playerPosition.x = Mathf.Clamp(playerPosition.x, -3.0f, 3.0f);
            player.transform.position = playerPosition;
        }

    }
    private void GroundCheck()
    {
        //get the radius of the players capsule collider, and make it a tiny bit smaller than that
        float radius = playerCollider.radius * 0.9f;
        //get the position (assuming its right at the bottom) and move it up by almost the whole radius
        Vector3 pos = transform.position + Vector3.up * (radius * 0.9f);
        //returns true if the sphere touches something on that layer
        isGrounded = Physics.CheckSphere(pos, radius, whatIsGround);
    }

    public void Jump()
    {
        Debug.Log("Jump");
        if ( isGrounded && canRun)
        {
            _SoundGlissade.Stop();
            _SoundJump.Play();
            animator.SetTrigger("Saut");
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
            playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private IEnumerator CrouchDelay()
    {
        yield return new WaitForSeconds(crouchDelay);
        playerCollider.height = playerHeight;
    }
    
    public void Crouch()
    {
        if (isGrounded && canRun)
        {
            _SoundJump.Stop();
            _SoundGlissade.Play();
            playerCollider.height = crouchHeight;
            StartCoroutine(CrouchDelay());
            animator.SetTrigger("Glisse");
        }        
    }


    private void Run()
    {
        if (canRun)
        {
            speed = Mathf.Clamp(speed, 0, maxSpeed);
         runDirection = playerOriantation.forward;
            if(isGrounded)
            {
                playerRb.AddForce(runDirection.normalized * speed, ForceMode.Force);
            }
            if(!isGrounded)
            {
                playerRb.AddForce(runDirection.normalized * speed * moveMultiplier, ForceMode.Force);
            }
        }
    }
}

