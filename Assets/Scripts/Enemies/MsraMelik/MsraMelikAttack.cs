using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsraMelikAttack : MonoBehaviour
{
	public static MsraMelikAttack _instance;

	[SerializeField] private GameObject _windEffect;
	[SerializeField] private Transform _windSpawnPoint;
	[SerializeField] private LayerMask _playerLayer;
	[SerializeField] private float _raycastDistance = 5.0f;

	// ROCKS

	[SerializeField] private Transform _rockSpawnPoint_1, _rockSpawnPoint_2, _rockDestroyPoint;
	[SerializeField] private GameObject _rock1, _rock2, _mace;
	[SerializeField] private float _fallingSpeed = 5f;
	[SerializeField] private float _spawnRockInterval = 1.0f; // Interval between object spawns
	[SerializeField] private float _spawnRockDuration = 30f; // Duration of the spawning process

	[SerializeField] private Transform _playerTransform;
	[SerializeField] private float _aimAtPlayerProbability = 0.5f; // Probability of aiming at the player (0 to 1)


	// ARROWS

	[SerializeField] private GameObject _arrowPrefab;
	[SerializeField] private Transform _leftUp, _leftDown, _rightUp, _rightDown;
	[SerializeField] private float _arrowSpeed = 5f;
	[SerializeField] private float _spawnArrowInterval = 2.0f;
	[SerializeField] private float _durationArrows = 30f;


	// SPIKES

	[SerializeField] private SpikeWave _spikeWave;

	private void Awake()
	{
		_instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void ActivateSpikeWave()
	{
		Debug.Log("Attack");
		_spikeWave._activateWave = true;
	}

	public void FireArrows()
	{
		StartCoroutine(SpawnArrowsCoroutine());
	}

	private IEnumerator SpawnArrowsCoroutine()
	{
		float startTime = Time.time;

		while (Time.time - startTime < _durationArrows)
		{
			// Randomly choose to spawn either a left arrow, a right arrow, or both
			int arrowChoice = Random.Range(0, 6);

			if (arrowChoice == 0 || arrowChoice == 5)
			{
				float leftVerticalPosition = Random.Range(_leftDown.position.y, _leftUp.position.y);
				Vector3 leftArrowPosition = new Vector3(_leftUp.position.x, leftVerticalPosition, _leftUp.position.z);
				StartCoroutine(MoveArrow(leftArrowPosition, Vector3.right));
			}

			if (arrowChoice == 1 || arrowChoice == 5)
			{
				float rightVerticalPosition = Random.Range(_rightDown.position.y, _rightUp.position.y);
				Vector3 rightArrowPosition = new Vector3(_rightUp.position.x, rightVerticalPosition, _rightUp.position.z);
				StartCoroutine(MoveArrow(rightArrowPosition, Vector3.left));
			}

			yield return new WaitForSeconds(_spawnArrowInterval * 5); // Decrease the interval by a factor of 2 (10 / 2 = 5)
		}
	}


	private IEnumerator MoveArrow(Vector3 position, Vector3 direction)
	{
		GameObject arrowInstance = Instantiate(_arrowPrefab, position, Quaternion.identity);
		SpriteRenderer arrowSpriteRenderer = arrowInstance.GetComponent<SpriteRenderer>();

		if (direction == Vector3.left)
		{
			arrowSpriteRenderer.flipX = true;
		}
		else
		{
			arrowSpriteRenderer.flipX = false;
		}

		float destroyPositionX = direction == Vector3.right ? _rightUp.position.x : _leftUp.position.x;

		while (true)
		{
			arrowInstance.transform.position += direction * _arrowSpeed * Time.deltaTime;

			if ((direction == Vector3.right && arrowInstance.transform.position.x >= destroyPositionX) ||
				(direction == Vector3.left && arrowInstance.transform.position.x <= destroyPositionX))
			{
				Destroy(arrowInstance);
				break;
			}

			yield return null;
		}
	}








	public void ThrowRocks()
	{
		StartCoroutine(SpawnRocksCoroutine());
	}

	private IEnumerator SpawnRocksCoroutine()
	{
		float elapsedTime = 0f;
		int numSegmentsMultiplier = 2;
		int numSegments = Mathf.CeilToInt(_spawnRockDuration / (_spawnRockInterval * 4)) * numSegmentsMultiplier;

		while (elapsedTime < _spawnRockDuration)
		{
			GameObject[] prefabs = new GameObject[] { _rock1, _rock2, _mace };
			GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];

			float segmentWidth = (_rockSpawnPoint_2.position.x - _rockSpawnPoint_1.position.x) / numSegments;
			int randomSegment = Random.Range(0, numSegments);
			float minRandomX = _rockSpawnPoint_1.position.x + randomSegment * segmentWidth;
			float maxRandomX = minRandomX + segmentWidth;
			float randomX = Random.Range(minRandomX, maxRandomX);
			Vector3 spawnPosition = new Vector3(randomX, _rockSpawnPoint_1.position.y, _rockSpawnPoint_1.position.z);

			if (_playerTransform != null && Random.value <= _aimAtPlayerProbability)
			{
				spawnPosition.x = _playerTransform.position.x;
			}

			GameObject instance = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
			StartCoroutine(MoveAndDestroyObject(instance));

			yield return new WaitForSeconds(_spawnRockInterval * 4);

			elapsedTime += _spawnRockInterval * 4;
		}
	}

	private IEnumerator MoveAndDestroyObject(GameObject obj)
	{
		while (obj.transform.position.y > _rockDestroyPoint.position.y)
		{
			obj.transform.position -= new Vector3(0, _fallingSpeed * Time.deltaTime, 0);
			yield return null;
		}

		Destroy(obj);
	}


	public void SpawnWindEffect()
	{
		int spriteDirection = GetSpriteDirection();
		Vector3 windEffectRotation = new Vector3(0, 0, spriteDirection == 1 ? 90 : -90);
		Vector3 windEffectPosition = _windSpawnPoint.position + new Vector3(spriteDirection * 2.0f, 0, 0);
		GameObject windEffectInstance = Instantiate(_windEffect, windEffectPosition, Quaternion.Euler(windEffectRotation));
		StartCoroutine(DeleteWindEffectAfterDelay(windEffectInstance, 0.6f));

		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right * spriteDirection, _raycastDistance, _playerLayer);

		if (hitInfo.collider != null)
		{
			PlayerMovement._instance.WindKnockBack(spriteDirection == -1);
			PlayerHealthController._instance.TakeDamage(1, true);
		}
	}


	private IEnumerator DeleteWindEffectAfterDelay(GameObject windEffectInstance, float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy(windEffectInstance);
	}

	private int GetSpriteDirection()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		return spriteRenderer.flipX ? -1 : 1;
	}
}
