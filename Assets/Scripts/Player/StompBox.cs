using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EasyEnemy")
        {
            //other.transform.parent.gameObject.SetActive(false);

            //Instantiate(_deathEffect.transform, other.transform.position, other.transform.rotation);

            //PlayerMovement._instance.Bounce();

            //float _dropSelect = Random.Range(0, 100f);

            //if(_dropSelect <= _chanceOfDrop)
            //{
            //    Instantiate(_collectible, other.transform.position, other.transform.rotation);
            //}
			other.GetComponent<EnemyHealthControll>().GetDamage(other.GetComponent<EnemyHealthControll>().Health); // enemy dies because he takes damage equal to the size of his life
		}
    }
}
