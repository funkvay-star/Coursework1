using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;

    [SerializeField] private float _waitToRespawn;

    private int _collectedGems;

	public bool _gamePaused;

	public int Gems
	{
		get { return _collectedGems; }
        set 
        {
            if(value < 0)
            {
                _collectedGems = 0;
            }
            else
            {
				_collectedGems = value;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        //PlayerMovement._instance.gameObject.SetActive(false);
        //PlayerMovement._instance.RigidBody.velocity = new Vector3(0, 0, 0); MAYBE NEEDED TO COME BACK
		PlayerMovement._instance.Animator.SetTrigger("died");

		yield return new WaitForSeconds(_waitToRespawn);

		RestorePlayerAfterRespawn();

        --PlayerHealthController._instance.Life;
        UIController._instance.UpdateLifeCount();
	}

    private void RestorePlayerAfterRespawn()
    {
		PlayerMovement._instance.transform.position = CheckpointController._instance._spawnPoint;
		PlayerMovement._instance.gameObject.SetActive(true);
        PlayerHealthController._instance.Health = PlayerHealthController._instance.MaxHealth;
        UIController._instance.UpdateHealthDisplay();

		PlayerMovement._instance.Animator.Rebind();
	}
}
