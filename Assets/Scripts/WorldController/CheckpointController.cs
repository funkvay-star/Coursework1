using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController _instance;

    private Checkpoint[] _checkpoints;
    public Vector3 _spawnPoint;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _checkpoints = FindObjectsOfType<Checkpoint>();

        _spawnPoint = PlayerMovement._instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void DeactivateCheckpoints()
    {
        for (int i = 0; i < _checkpoints.Length; ++i)
        {
            _checkpoints[i].ResetCheckpoint();
        }
    }

    public void SetNewSpawnPoint(Vector3 newSpawnPoint)
    {
        _spawnPoint = newSpawnPoint;
    }
}
