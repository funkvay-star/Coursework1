using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite _checkpointOff, _checkpointOn; 

    private SpriteRenderer _theSR;

    // Start is called before the first frame update
    void Start()
    {
		_theSR = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            CheckpointController._instance.DeactivateCheckpoints();
            CheckpointController._instance.SetNewSpawnPoint(transform.position);

            _theSR.sprite = _checkpointOn;
        }
    }

    public void ResetCheckpoint()
    {
		_theSR.sprite = _checkpointOff;
	}
}
