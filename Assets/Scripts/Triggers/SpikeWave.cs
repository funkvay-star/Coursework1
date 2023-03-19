using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWave : MonoBehaviour
{
	[SerializeField] private GameObject spikePrefab;
	[SerializeField] private float waveSpeed;
	[SerializeField] private float waveFrequency;
	[SerializeField] private float waveAmplitude;
	[SerializeField] private float minDistanceBetweenSpikes;
	[SerializeField] private Transform player;

	[SerializeField] private List<GameObject> spikes;

	void Start()
	{
		
	}

	void Update()
	{
		for (int i = 0; i < spikes.Count; i++)
		{
			GameObject spike = spikes[i];
			float newY = Mathf.Sin(Time.time * waveFrequency + i) * waveAmplitude;
			spike.transform.position = new Vector3(spike.transform.position.x, newY, spike.transform.position.z);
		}
	}
}
