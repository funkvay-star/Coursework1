using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControll : MonoBehaviour
{
    [SerializeField] private int _health;
	public int Health
	{
		get { return _health; }
	}

	[SerializeField] private GameObject _deathEffect;
	[SerializeField] private GameObject _collectible;
	[SerializeField, Range(0, 100)] private float _chanceOfDrop;

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
        _health -= damage;

        Debug.Log(_health);

        if(_health <= 0)
        {
			transform.parent.gameObject.SetActive(false);

			Instantiate(_deathEffect.transform, transform.position, transform.rotation);

			PlayerMovement._instance.Bounce();

			float _dropSelect = Random.Range(0, 100f);

			if (_dropSelect <= _chanceOfDrop)
			{
				Instantiate(_collectible, transform.position, transform.rotation);
			}
		}
    }
}
