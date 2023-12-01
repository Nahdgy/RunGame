using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, whatIsGround);
    }

    public void Jump()
    {
        if ( isGrounded && canRun)
        {
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
        if(isGrounded && canRun)
        {
            playerCollider.height = crouchHeight;
            StartCoroutine(CrouchDelay());
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

