using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsraMelikHealth : MonoBehaviour
{
	public static MsraMelikHealth _instance;

	[SerializeField] private int _health;
	public int Health
	{
		get { return _health; }
	}

	[SerializeField] private GameObject _deathEffect;
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private float _unkillableStartDuration = 5f;
	[SerializeField] private float _unkillableEndDuration = 10f;
	[SerializeField] private float _transparentAlpha = 0.5f;
	private bool _isUnkillable = false;

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

	public void GetDamage(int damage)
	{
		if (_isUnkillable) return;

		_health -= damage;

		Debug.Log(_health);

		if (_health <= 0)
		{
			transform.parent.gameObject.SetActive(false);

			Instantiate(_deathEffect.transform, transform.position, transform.rotation);
		}
	}

	public void MakeUnkillable()
	{
		StartCoroutine(UnkillableCoroutine());
	}

	private IEnumerator UnkillableCoroutine()
	{
		_isUnkillable = true;
		Color originalColor = _spriteRenderer.color;
		_spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, _transparentAlpha);

		float unkillableDuration = Random.Range(_unkillableStartDuration, _unkillableEndDuration);
		Debug.Log(unkillableDuration);
		yield return new WaitForSeconds(unkillableDuration);

		_spriteRenderer.color = originalColor;
		_isUnkillable = false;
	}
}
