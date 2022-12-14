using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _hitStrength;
	[SerializeField] private float _waitAfterAttack;
	private float _counterAfterAttack;

	private Animator _animator;

	// Start is called before the first frame update
	void Start()
    {
		_animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		if (_counterAfterAttack > 0)
		{
			_counterAfterAttack -= Time.deltaTime;
		}
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_counterAfterAttack <= 0 && other.tag == "Player")
        {
           // other.GetComponent<PlayerHealthController>().TakeDamage(_hitStrength);
			_counterAfterAttack = _waitAfterAttack;
			_animator.SetTrigger("attack");
		}
    }
}
