using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    #region Movement Variables
    bool isMoving = false;
    PlayerControl controls;

    [SerializeField] 
    Rigidbody2D rb;
    
    [SerializeField]
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpForce = 1.0f;

    private Vector2 movementInput = Vector2.zero;
    private Vector2 lastInput = Vector2.zero;
    private bool jump = false;
    Vector3 velocity;
    float angle = 0;
    Vector3 resFacing;

    Vector3 velocityChange;
    bool grounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float groundDistance;

    [SerializeField] Vector2 boxSize;
    Vector3 facingDirection;

    #endregion

    #region Interaction Variables

    //TODO: refactor This

    [SerializeField]
    Collider2D col;

    [SerializeField]
    Transform spriteRef;
    public Transform SpriteTransformRef
    {
        get { return spriteRef; }
    }


    BehaviorState currentPlayerState;

    public BehaviorState CurrentState{
        get{ return currentPlayerState; }
    }

    public PlayerInput CurrentPlayerInput
    {
        get { return playerConfiguration.input; }
    }

    public Player HugRef
    {
        get; set;
    }

    
    #endregion

    #region PlayerReferences
    PlayerConfiguration playerConfiguration;

    public bool test = false;
    
    #endregion

    void OnDrawGizmosSelected()
    {
        //Gizmos.DrawSphere(groundCheck.position, groundDistance);
        //Gizmos.DrawSphere(groundCheck.position, groundDistance);
        //Vector3 direction = new Vector3( * 0.5f);
        //Gizmos.DrawRay(transform.position, direction);
    }

    private void Awake() {
        controls = new PlayerControl();

        //maybe get rid of this one
        
    }

    private void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        lastInput = movementInput;
        movementInput = context.ReadValue<Vector2>();
        if(movementInput.x != 0f && (movementInput.x > 0f && spriteRef.transform.localScale.x < 0f) || (movementInput.x < 0f && spriteRef.transform.localScale.x > 0f)){
            Flip();
        }
        isMoving = movementInput.x!=0?true:false;
    }

    public void Onjump(InputAction.CallbackContext context)
    {
        jump = context.action.triggered;
        if(IsPlayerGrounded())
        {
            rb.velocity = rb.velocity.x * Vector2.right;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //currentVisual.GetComponent<Animator>().SetTrigger("Jump");
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //Maybe rename to interact && 
        //
        if (context.action.triggered)
        {
            //ChangeState(BehaviorState.StartHug);
        }
    }

    void Input_onAccctionTriggered(CallbackContext context){
        
        if (context.action.name == controls.Player.Movement.name)
        {
            OnMove(context);    
        }

        if (context.action.name == controls.Player.Jump.name)
        {
            Onjump(context);    
        }

        /*if (context.action.name == controls.Player.Interact.name)
        {
            OnInteract(context);
        }*/
    }

    public void InitializePlayer(PlayerConfiguration playerConfig, LevelController levelController){
        //this should not have the pawor to see all the level methods
        //respawn = playerConfig.respawn;
        //input = playerConfiguration.input;
        playerConfiguration = playerConfig;
        playerConfiguration.input.onActionTriggered += Input_onAccctionTriggered;
        

        currentPlayerState = BehaviorState.Normal;
    }

    void FixedUpdate()
    {
        groundedPlayer = IsPlayerGrounded();
        if(isMoving)
        {
            rb.velocity = playerSpeed * ((groundedPlayer)? 1 : 0.5f) * movementInput.x * Vector2.right + rb.velocity.y * Vector2.up;
        }
        else
        {
            rb.velocity = Vector2.up * rb.velocity.y;
            //currentVisual.GetComponent<Animator>().SetFloat("Speed", 0);
        }
    }

    public void Flip()
    {
        spriteRef.transform.localScale = Vector3.up * spriteRef.transform.localScale.y + Vector3.forward * spriteRef.transform.localScale.z + Vector3.right * spriteRef.transform.localScale.x * -1;
    }

    public bool IsPlayerGrounded()
    {
        Color rayColor;
        RaycastHit2D raycastHit = Physics2D.BoxCast(groundCheck.position, boxSize, 0f, Vector2.down, groundDistance, whatIsGround);
        if (raycastHit.collider != null)
        {
        rayColor = Color.green;
        }
        else
        {
        rayColor = Color.red;
        }
        Debug.DrawRay(groundCheck.position + new Vector3(boxSize.x/2f, 0), Vector2.down * (groundDistance), rayColor);
        Debug.DrawRay(groundCheck.position - new Vector3(boxSize.x/2f, 0), Vector2.down * (groundDistance), rayColor);
        Debug.DrawRay(groundCheck.position - new Vector3(boxSize.x/2f, boxSize.y/2f + groundDistance, 1f), Vector2.right * (boxSize.x), rayColor);
        return (groundCheck)?Physics2D.BoxCast(groundCheck.position, boxSize, 0f, Vector2.down, groundDistance, whatIsGround):false; 
    }

    //find through the states if is a valid action
    public bool CanInteract(){
        return true;
        //return interactionDetector.CurrentTarget != null;
    }

    #region InteractionMethods


    public void Interact()
    {
        /*
        if (currentPlayerState == BehaviorState.StartHug && interactionDetector.CurrentTarget != null)
        {
            kawaiijuEventChannelSO.RaiseEvent(this, interactionDetector.CurrentTarget);
        }
        else if(currentPlayerState == BehaviorState.StartHug)
        {
            ReturnToNormal();
        }*/
        
    }

    #endregion

    #region Public methods
    public Vector3 FacingDirection()
    {
        angle = Mathf.Atan2(facingDirection.z, facingDirection.x) * Mathf.Rad2Deg;
        angle -= 90;
        if (angle < 0)
        {
            angle += 360;
        }
        //print(angle);
        // if we add more direction we devide the abgle secctions in four uinstead of 2
        if (angle > 0f && angle < 180f)
        {
            resFacing = -Vector3.right;
        }
        else
        {
            resFacing = Vector3.right;
        }
        return resFacing;
    }

    public void ReturnToNormal()
    {
        rb.isKinematic = false;
        //playerHug = -1;
        currentPlayerState = BehaviorState.Normal;
        //transform.parent = GameManager.instance.transform;
        transform.localScale = Vector3.one;
        Stop();
        col.enabled = true;
    }

    public void Stop()
    {
        //rigidbody.AddForce(Vector3.zero, ForceMode.VelocityChange);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }
    #endregion

}
