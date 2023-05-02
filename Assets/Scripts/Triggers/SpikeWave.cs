using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWave : MonoBehaviour
{
	[SerializeField] private float _waveSpeed;
	[SerializeField] private float waveFrequency;
	[SerializeField] private float waveAmplitude;
	[SerializeField] private Transform player;

	[SerializeField] private List<GameObject> spikes;

	private List<Vector3> initialPositions;

	public bool _activateWave;

	void Start()
	{
		initialPositions = new List<Vector3>();

		foreach (GameObject spike in spikes)
		{
			initialPositions.Add(spike.transform.position);
		}
	}

	void Update()
	{
		if (!_activateWave)
		{
			return;
		}

		for (int i = 0; i < spikes.Count; i++)
		{
			GameObject spike = spikes[i];
			float newY = initialPositions[i].y + Mathf.Sin((Time.time * _waveSpeed) * waveFrequency + i) * waveAmplitude;
			spike.transform.position = new Vector3(spike.transform.position.x, newY, spike.transform.position.z);
		}
	}
}
