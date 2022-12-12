using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int _moveSpeed;

    [SerializeField] private Transform _leftPoint, _rightPoint;

    [SerializeField] bool _lookOtherWay; // if your sprites looks right, then _lookOtherWay = false, and if sprite looks left, then _lookOtherWay = true

    //[SerializeField] private int _lookOtherWay; // if your sprites looks right, then _lookOtherWay = -1, and if sprite looks left, then _lookOtherWay = 1

	private Animator _animator;

    private bool _movingRight;

    private Rigidbody2D _theRB;
    public SpriteRenderer _theSR;

    public float _waitTime, _moveTime;
    private float _waitCount, _moveCount;

    // Start is called before the first frame update
    void Start()
    {
        _theRB = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _leftPoint.parent = null;
        _rightPoint.parent = null;

        _movingRight = true;
        _moveCount = Random.Range(_moveTime * 0.5f, _moveTime * 1.25f);

        //_theSR.flipX = _lookOtherWay;
        transform.localScale = new Vector3(ShouldLookOtherWay(_lookOtherWay), 1, 1);
	}

    // Update is called once per frame
    void Update()
    {
        if(_moveCount > 0)
        {
			_moveCount -= Time.deltaTime;

			if (transform.position.x > _rightPoint.position.x)
			{
				_movingRight = false;
				//_theSR.flipX = !_lookOtherWay;
				transform.localScale = new Vector3(ShouldLookOtherWay(!_lookOtherWay), 1, 1);
			}
			else if (transform.position.x < _leftPoint.position.x)
			{
				_movingRight = true;
				//_theSR.flipX = _lookOtherWay;
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

            if(_moveCount <= 0)
            {
                _waitCount = Random.Range(_waitTime * 0.75f, _waitTime * 1.25f);
            }

            _animator.SetBool("isMoving", true);
		}
        else if(_waitCount > 0)
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

    private int ShouldLookOtherWay(bool checker)
    {
        return checker == true ? -1 : 1;
	}
}
