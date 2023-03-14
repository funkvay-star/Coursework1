using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Types;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform _target;

	[SerializeField] private Transform _farBackBackground, _middleBackground;

	[SerializeField] private float _maxHeight, _minHeight;

	private PlayerMovement _playerMov;

	private float _lastXPosition;

	// Start is called before the first frame update
	void Start()
	{
		_lastXPosition = transform.position.x;
	}

	// Update is called once per frame
	void Update()
	{
		FollowTarger();

		ChangeBackground();
	}

	void FollowTarger()
	{
		transform.position = new Vector3(_target.position.x, Mathf.Clamp(_target.position.y, _minHeight, _maxHeight), transform.position.z);
	}

	void ChangeBackground()
	{
		float amountTOMoveX = transform.position.x - _lastXPosition;

		//_farBackBackground.position += new Vector3(amountTOMoveX, 0, 0);
		//_middleBackground.position += new Vector3(amountTOMoveX * 0.5f, 0, 0);

		_farBackBackground.position = new Vector3(transform.position.x, transform.position.y, 0);
		_middleBackground.position = 0.75f * new Vector3(transform.position.x, transform.position.y, 0);
		//_closeBackground.position = new Vector3(0.75f * transform.position.x, 0.6f * transform.position.y, 0);

		_lastXPosition = transform.position.x;
	}
}