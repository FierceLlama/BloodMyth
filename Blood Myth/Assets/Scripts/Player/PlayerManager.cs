using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    private Player _player;

    private PlayerStates _currentPlayerState;
    private PlayerStates _normalMovement;
    private PlayerStates _tiredMovement;
    private PlayerStates _exhaustedMovement;
    private PlayerStates _sprinting;
    private PlayerStates _jumping;

    private Animator _animController;
    private bool _facingRight = true;
    private Rigidbody2D _rb2D;
    private SpriteRenderer _flipSprite;
    private float _move;
    private float _currentMoveRate;

    // Checking for ground below me
    private bool _isGrounded;
    public Transform groundCheck;
    private float _groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public static PlayerManager instance;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            instance.Initialize();
        }
        else if (instance != this)
        {
            DestroyImmediate(this);
        }
    }

    void Initialize()
    {
        this._normalMovement = new PlayerNormal();
        this._tiredMovement = new PlayerTiredMovement();
        this._exhaustedMovement = new PlayerExhaustedMovement();
        this._sprinting = new PlayerSprinting();
        this._jumping = new PlayerJumping();
        this.PlayerIsNormal();

        this._player = GetComponent<Player>();
        this._currentMoveRate = _player.maxMovement;
        this._animController = this.gameObject.GetComponent<Animator>();
        this._rb2D = GetComponent<Rigidbody2D>();
        this._flipSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        this._isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this._groundRadius, this.whatIsGround);
        this._animController.SetBool("grounded", this._isGrounded);
        this._currentPlayerState.Update();
    }

    public void SwitchPlayerState(PlayerStates newPlayerState)
    {
        this._currentPlayerState.Exit();
        this._currentPlayerState = newPlayerState;
        this._currentPlayerState.Enter();
    }

    public void PlayerIsNormal()
    {
        this.SwitchPlayerState(this._normalMovement);
    }

    public void PlayerIsSprinting()
    {
        this.SwitchPlayerState(this._sprinting);
    }

    public void NormalMovement()
    {
        if (!this._isGrounded)
        {
            this._currentMoveRate = this._player.maxMovement / 2.0f;
        }
        this.PlayerMovement();        
    }

    public void SprintingMovement()
    {
        if (!this._isGrounded)
        {
            this._currentMoveRate = this._player.sprintMaxMovement / 2.0f;
        }
        this.PlayerMovement();
    }

    void PlayerMovement()
    {
        this._move = Input.GetAxis("Horizontal");
        this._animController.SetFloat("speed", Mathf.Abs(this._move));

        this.VerifySpriteDirection();

        // Movement is velocity in the x direction up to current move rate
        this._rb2D.velocity = new Vector2(this._move * this._currentMoveRate, this._rb2D.velocity.y);
    }

    void VerifySpriteDirection()
    {
        if (this._move < 0 && this._facingRight)
        {
            this._flipSprite.flipX = true;
            this._facingRight = false;
        }
        else if (this._move > 0 && !this._facingRight)
        {
            this._flipSprite.flipX = false;
            this._facingRight = true;
        }
    }

    public void SetNormalMovementValues()
    {
        this._currentMoveRate = this._player.maxMovement;
    }

    public void SetTiredMovementValues()
    {
    }

    public void SetExhaustedMovementValues()
    {
    }

    public void SetSprintingMovementValues()
    {
        this._currentMoveRate = this._player.sprintMaxMovement;
    }
}
