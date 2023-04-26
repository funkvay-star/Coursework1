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
			//Debug.Log("See player");
			PlayerMovement._instance.WindKnockBack(spriteDirection == -1);
			PlayerHealthController._instance.TakeDamage(1, true);
		}
		else
		{
			//Debug.Log("Don't see player");
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
