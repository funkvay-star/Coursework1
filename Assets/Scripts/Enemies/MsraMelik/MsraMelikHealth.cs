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

	private Color originalColor;

	private void Awake()
	{
		_instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		// Save the original color
		originalColor = spriteRenderer.color;
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

		// Start a new ChangeColor coroutine
		//StartCoroutine(ChangeColor());

		MsraMelikAttack._instance.CheckPlayerDistanceAndAttack();

		MakeUnkillable();
	}

	private IEnumerator ChangeColor()
	{
		// Get the SpriteRenderer component
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		// Wait for 0.5 seconds before changing the color
		yield return new WaitForSeconds(0.5f);

		// Set the color to translucent red
		spriteRenderer.color = new Color(1, 0, 0, 0.5f);

		// Wait for 0.5 seconds while the object is red
		yield return new WaitForSeconds(0.5f);

		// Gradually return to the original color over 0.5 seconds
		for (float t = 0; t < 0.5f; t += Time.deltaTime)
		{
			spriteRenderer.color = Color.Lerp(new Color(1, 0, 0, 0.5f), originalColor, t / 0.5f);
			yield return null;
		}

		// Ensure the color is set to the original color at the end
		spriteRenderer.color = originalColor;
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
