using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private bool _isGem, _isLittleHeal, _isMediumHeal, _isMaxHeal;
    private int _littleHeal = 1, _mediumHeal = 2;

    [SerializeField] private GameObject _pickupEffect;

    private bool _isCollected;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IsGem()
    {
		LevelManager._instance.Gems++;
		_isCollected = true;

		Destroy(gameObject); // this activated after OnTriggerEnter2D is done (at the end of the frame), so we still need to use _isCollected to not collect it twice if player has 2 collieders or if there are 2 players

        UIController._instance.UpdateGems();

		Instantiate(_pickupEffect, transform.position, transform.rotation);
	}

    private void Heal(int heal)
    {
        PlayerHealthController._instance.HealPlayer(heal);

		Destroy(gameObject);

        Instantiate(_pickupEffect, transform.position, transform.rotation);
	}

    private void IsHeal()
    {
		if (_isLittleHeal)
        {
            Heal(_littleHeal);
        }
        else if(_isMediumHeal)
        {
            Heal(_mediumHeal);
        }
        else
        {
            Heal(PlayerHealthController._instance.MaxHealth);
        }
	}

    private void WhatPickUp()
    {
        if (_isGem)
        {
            IsGem();
        }
        else if(_isLittleHeal || _isMediumHeal || _isMaxHeal)
        {
			if (PlayerHealthController._instance.Health == PlayerHealthController._instance.MaxHealth)
			{
				return;
			} 
            IsHeal();
		}
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !_isCollected)
        {
            WhatPickUp();
        }
    }
}
