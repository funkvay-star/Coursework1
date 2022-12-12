using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement _instance;

	[SerializeField] private RuntimeAnimatorController _originalAnimation;

	[SerializeField] private float _jumpForce;
	[SerializeField] private float _runSpeed;
	[SerializeField] private Rigidbody2D _playersRigidBody;
	public Rigidbody2D RigidBody
	{
		get { return _playersRigidBody; }
		set { _playersRigidBody = value; }
	}

	[SerializeField] private bool _isGrounded;
	[SerializeField] private Transform _groundCheckPoint;
	[SerializeField] private LayerMask _isGround;

	[SerializeField] private bool _doubleJump;

	[SerializeField] private float _knockBackLength, _knockBackForce, _bounceForce;
	private float _knockBackCounter;

	private Animator _animator;
	public Animator Animator
	{
		get { return _animator; }
	}

	private SpriteRenderer _theSR;
	public SpriteRenderer TheSpriteRenderer
	{
		get { return _theSR; }
	}

	private void Awake()
	{
		_instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		_animator = GetComponent<Animator>();
		_theSR = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (PlayerHealthController._instance.Health <= 0 || LevelManager._instance._gamePaused)
		{
			_playersRigidBody.velocity = new Vector2(0, -5);
			_animator.SetFloat("moveSpeed", 0);
			return;
		}

		if (_knockBackCounter <= 0)
		{
			_playersRigidBody.velocity = new Vector2(_runSpeed * Input.GetAxisRaw("Horizontal"), _playersRigidBody.velocity.y);

			_isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, 0.2f, _isGround);

			if (Input.GetButtonDown("Jump"))
			{
				if (_isGrounded)
				{
					_playersRigidBody.velocity = new Vector2(_playersRigidBody.velocity.x, _jumpForce);
					_doubleJump = true;
				}
				else if (_doubleJump)
				{
					_playersRigidBody.velocity = new Vector2(_playersRigidBody.velocity.x, _jumpForce);
					_doubleJump = false;
				}
			}

			if (_playersRigidBody.velocity.x < 0)
			{
				_theSR.flipX = true;
			}
			else if (_playersRigidBody.velocity.x > 0)
			{
				_theSR.flipX = false;
			}
		}
		else
		{
			_knockBackCounter -= Time.deltaTime;

			if (_theSR.flipX)
			{
				_playersRigidBody.velocity = new Vector2(_knockBackForce, _playersRigidBody.velocity.y); // knock back to the left
			}
			else
			{
				_playersRigidBody.velocity = new Vector2(-_knockBackForce, _playersRigidBody.velocity.y); // knock back to the right
			}
		}

		_animator.SetBool("isGrounded", _isGrounded);
		_animator.SetFloat("moveSpeed", Mathf.Abs(_playersRigidBody.velocity.x));
	}

	public void KnockBack()
	{
		_knockBackCounter = _knockBackLength;
		_playersRigidBody.velocity = new Vector2(0, _knockBackForce);

		_animator.SetTrigger("hurt");
	}

	public void Bounce()
	{
		_playersRigidBody.velocity = new Vector2(_playersRigidBody.velocity.x, _bounceForce);
	}

	public void GiveSword()
	{
		PlayerAttack._instance._isArmed = true;
		_animator.runtimeAnimatorController = _originalAnimation;
	}
}