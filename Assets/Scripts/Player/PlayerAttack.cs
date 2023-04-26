using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public static PlayerAttack _instance;

	[SerializeField] Transform _attackAnimationObj;

	[SerializeField] private float obstacleRayDistance;
	[SerializeField] private LayerMask _layerMask;
	[SerializeField] private float _waitAfterAttack;
	private float _counterAfterAttack;

	[SerializeField] private int _hitStrength;

	private Animator _animator;
	public Animator Animator
	{
		get { return _animator; }
	}

	public GameObject obstacleRayObject;

	private float _characterDirection;
	public bool _isArmed;

	private void Awake()
	{
		_instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if(_counterAfterAttack > 0)
		{
			_counterAfterAttack -= Time.deltaTime;
		}

		if(Input.GetKeyDown(KeyCode.K) && _isArmed && _counterAfterAttack <= 0)
		{
			Attack();
			_counterAfterAttack = _waitAfterAttack;
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			//MsraMelikHealth._instance.MakeUnkillable();
			MsraMelikAttack._instance.SpawnWindEffect();
		}
	}

	private void Attack()
	{
		_animator.SetTrigger("attack");


		SpriteRenderer animationObjectRenderer = _attackAnimationObj.GetComponent<SpriteRenderer>();

		if (PlayerMovement._instance.TheSpriteRenderer.flipX)
		{
			_characterDirection = -1f;
			animationObjectRenderer.flipX = true;
		}
		else
		{
			_characterDirection = 1f;
			animationObjectRenderer.flipX = false;
		}

		RaycastHit2D hitObstacle = Physics2D.Raycast(obstacleRayObject.transform.position, Vector2.right * new Vector2(_characterDirection, 0f), obstacleRayDistance, _layerMask);

		if (hitObstacle.collider != null)
		{
			Debug.DrawRay(obstacleRayObject.transform.position, Vector2.right * hitObstacle.distance * new Vector2(_characterDirection, 0f), Color.red);
			Debug.Log("Enemy Detected");

			//hitObstacle.collider.gameObject.SetActive(false);
			Debug.Log(hitObstacle.collider.tag);
			hitObstacle.collider.GetComponent<EnemyHealthControll>().GetDamage(_hitStrength);
		}
		else
		{
			Debug.DrawRay(obstacleRayObject.transform.position, Vector2.right * obstacleRayDistance * new Vector2(_characterDirection, 0f), Color.green);
			//Debug.Log("No enemy");
		}
	}

	private void OnDrawGizmos()
	{
		RaycastHit2D hitObstacle = Physics2D.Raycast(obstacleRayObject.transform.position, Vector2.right * new Vector2(_characterDirection, 0f), obstacleRayDistance, _layerMask);
		Gizmos.DrawRay(obstacleRayObject.transform.position, Vector2.right * hitObstacle.distance * new Vector2(_characterDirection, 0f));
	}
}