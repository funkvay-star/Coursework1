using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private int _moveSpeed;

	[SerializeField] private Transform _leftPoint, _rightPoint;

	[SerializeField] private bool _lookOtherWay;

	[SerializeField] private float playerDetectRange = 5.0f;
	[SerializeField] private float playerStopChasingRange = 5.0f;

	private Animator _animator;
	private bool _movingRight;
	private Rigidbody2D _theRB;
	public SpriteRenderer _theSR;

	public float _waitTime, _moveTime;
	private float _waitCount, _moveCount;

	private Transform playerTransform;

	private enum EnemyState { Patrol, ChasePlayer }
	private EnemyState currentState;

	void Start()
	{
		_theRB = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();

		_leftPoint.parent = null;
		_rightPoint.parent = null;

		_movingRight = true;
		_moveCount = Random.Range(_moveTime * 0.5f, _moveTime * 1.25f);

		transform.localScale = new Vector3(ShouldLookOtherWay(_lookOtherWay), 1, 1);

		currentState = EnemyState.Patrol;

		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{
		if (currentState == EnemyState.Patrol)
		{
			Patrol();
		}
		else if (currentState == EnemyState.ChasePlayer)
		{
			ChasePlayer();
		}

		DetectPlayer();
	}

	private void Patrol()
	{
		if (_moveCount > 0)
		{
			_moveCount -= Time.deltaTime;

			if (transform.position.x > _rightPoint.position.x)
			{
				_movingRight = false;
				transform.localScale = new Vector3(ShouldLookOtherWay(!_lookOtherWay), 1, 1);
			}
			else if (transform.position.x < _leftPoint.position.x)
			{
				_movingRight = true;
				transform.localScale = new Vector3(ShouldLookOtherWay(_lookOtherWay), 1, 1);
			}

			if (_movingRight)
			{
				_theRB.velocity = new Vector2(_moveSpeed, _theRB.velocity.y);
			}
			else if (!_movingRight)
			{
				_theRB.velocity = new Vector2(-_moveSpeed, _theRB.velocity.y);
			}

			if (_moveCount <= 0)
			{
				_waitCount = Random.Range(_waitTime * 0.75f, _waitTime * 1.25f);
			}

			_animator.SetBool("isMoving", true);
		}
		else if (_waitCount > 0)
		{
			_waitCount -= Time.deltaTime;
			_theRB.velocity = new Vector2(0f, _theRB.velocity.y);

			if (_waitCount <= 0)
			{
				_moveCount = Random.Range(_moveTime * 0.5f, _moveTime * 1.25f);
			}

			_animator.SetBool("isMoving", false);
		}
	}

	private void ChasePlayer()
	{
		Vector3 direction = (playerTransform.position - transform.position).normalized;
		float newXPosition = transform.position.x + (direction.x * _moveSpeed * Time.deltaTime);

		if (newXPosition >= _leftPoint.position.x && newXPosition <= _rightPoint.position.x)
		{
			_theRB.velocity = new Vector2(direction.x * _moveSpeed, _theRB.velocity.y);

			if (direction.x > 0)
			{
				transform.localScale = new Vector3(ShouldLookOtherWay(_lookOtherWay), 1, 1);
			}
			else
			{
				transform.localScale = new Vector3(ShouldLookOtherWay(!_lookOtherWay), 1, 1);
			}

			_animator.SetBool("isMoving", true);
		}
		else
		{
			float horizontalDistance = playerTransform.position.x - transform.position.x;
			if ((horizontalDistance > 0 && newXPosition < _leftPoint.position.x) || (horizontalDistance < 0 && newXPosition > _rightPoint.position.x))
			{
				newXPosition = Mathf.Clamp(newXPosition, _leftPoint.position.x, _rightPoint.position.x);
				_theRB.velocity = new Vector2(Mathf.Sign(horizontalDistance) * _moveSpeed, _theRB.velocity.y);

				if (Mathf.Sign(horizontalDistance) > 0)
				{
					transform.localScale = new Vector3(ShouldLookOtherWay(_lookOtherWay), 1, 1);
				}
				else
				{
					transform.localScale = new Vector3(ShouldLookOtherWay(!_lookOtherWay), 1, 1);
				}

				_animator.SetBool("isMoving", true);
			}
			else
			{
				_theRB.velocity = new Vector2(0f, _theRB.velocity.y);
				_animator.SetBool("isMoving", false);
			}
		}
	}


	private void DetectPlayer()
	{
		float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

		if (distanceToPlayer <= playerDetectRange)
		{
			currentState = EnemyState.ChasePlayer;
		}
		else if (distanceToPlayer > playerStopChasingRange && currentState == EnemyState.ChasePlayer)
		{
			currentState = EnemyState.Patrol;
		}
	}



	private int ShouldLookOtherWay(bool checker)
    {
        return checker == true ? -1 : 1;
	}
}
