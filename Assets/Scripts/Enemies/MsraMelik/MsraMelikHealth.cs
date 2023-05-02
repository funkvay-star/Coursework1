using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsraMelikHealth : MonoBehaviour
{
	public static MsraMelikHealth _instance;

	[SerializeField] private int _currentHealth;
	[SerializeField] private int _maxHealth;


	public int CurrentHealth
	{
		get { return _currentHealth; }
	}

	public int MaxHealth
	{
		get { return _maxHealth; }
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

		_currentHealth -= damage;

		Debug.Log(_currentHealth);

		if (_currentHealth <= 0)
		{
			transform.parent.gameObject.SetActive(false);

			Instantiate(_deathEffect.transform, transform.position, transform.rotation);
		}

		if (_currentHealth <= _maxHealth / 2)
		{
			MsraMelikAttack._instance.ActivateSpikeWave();
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
