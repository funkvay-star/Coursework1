using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
	public static PlayerHealthController _instance;

	[SerializeField] private int _maxLifeCount;
	private int _lifeCount;
	[SerializeField] private int _maxHealth;
	private int _currentHealth;

	[SerializeField] private float _invincibleLength;
	private float _invincibleCounter;

	private SpriteRenderer _theSR;

	public int MaxLife
	{
		get { return _maxLifeCount; }
	}

	public int Life
	{
		get { return _lifeCount; }
		set
		{
			//PrintStackTrace();
			if (value > 0 && value <= _maxLifeCount)
			{
				_lifeCount = value;
			}
			else if (value <= 0)
			{
				LevelChanger._instance.EndGame();
			}
			else
			{
				_lifeCount = _maxLifeCount;
			}
		}
	}
	private void PrintStackTrace()
	{
		//System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
		//Debug.Log(stackTrace.ToString());
	}

	public int MaxHealth
	{
		get { return _maxHealth; }
	}

	public int Health
	{
		get { return _currentHealth; }
		set 
		{ 
			if(value >= 0 && value <= _maxHealth)
			{
				_currentHealth = value; 
			}
			else if (value < 0)
			{
				_currentHealth = 0;
			}
			else
			{
				_currentHealth = _maxHealth;
			}
		}
	}

	private void Awake()
	{
		_instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		_theSR = GetComponent<SpriteRenderer>();
		_currentHealth = _maxHealth;
		_lifeCount = _maxLifeCount;
	}

	// Update is called once per frame
	void Update()
	{
		if (_invincibleCounter > 0)
		{
			_invincibleCounter -= Time.deltaTime;

			if (_invincibleCounter <= 0)
			{
				_theSR.color = new Color(_theSR.color.r, _theSR.color.g, _theSR.color.b, 1f);
			}
		}
	}

	public void TakeDamage(int damage, bool _damageFromWind = false)
	{
		if (_invincibleCounter <= 0)
		{
			_currentHealth -= damage;

			if (_currentHealth <= 0)
			{
				_currentHealth = 0;

				LevelManager._instance.RespawnPlayer();
				_currentHealth = MaxHealth;
				LevelManager._instance._gamePaused = true;
				//_invincibleCounter = _invincibleLength;
			}
			else
			{
				_invincibleCounter = _invincibleLength;
				_theSR.color = new Color(_theSR.color.r, _theSR.color.g, _theSR.color.b, 0.5f);

				if (!_damageFromWind)
				{
					PlayerMovement._instance.KnockBack();
				}
			}

			UIController._instance.UpdateHealthDisplay();
		}
	}

	public void HealPlayer(int heal)
	{
		Health += heal;

		UIController._instance.UpdateHealthDisplay();
	}
}