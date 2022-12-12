using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
	//[SerializeField] private PlayerHealthController _playerHealth; 

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			//_playerHealth.TakeDamage();
			//FindObjectOfType<PlayerHealthController>().TakeDamage();

			PlayerHealthController._instance.TakeDamage();
		}
	}
}