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

	[SerializeField] private Vector2 cameraWindow = new Vector2(2, 2); // Size of the camera window

	[SerializeField] private float smoothTime = 0.3f; // Smoothness factor for interpolation

	private PlayerMovement _playerMov;

	private float _lastXPosition;
	private Vector3 velocity = Vector3.zero; // Velocity used for interpolation

	// Start is called before the first frame update
	void Start()
	{
		_lastXPosition = transform.position.x;
	}

	// FixedUpdate is called at a fixed interval and is independent of frame rate
	void FixedUpdate()
	{
		FollowTarget();
	}

	// Update is called once per frame
	void Update()
	{
		ChangeBackground();
	}

	void FollowTarget()
	{
		Vector3 targetPosition = new Vector3(_target.position.x, Mathf.Clamp(_target.position.y, _minHeight, _maxHeight), transform.position.z);

		// Check if the player is outside the camera window
		if (Mathf.Abs(targetPosition.x - transform.position.x) > cameraWindow.x / 2 || Mathf.Abs(targetPosition.y - transform.position.y) > cameraWindow.y / 2)
		{
			// Smoothly interpolate the camera position
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		}
	}

	void ChangeBackground()
	{
		float amountToMoveX = transform.position.x - _lastXPosition;

		_farBackBackground.position = new Vector3(transform.position.x, transform.position.y, 0);
		_middleBackground.position = 0.75f * new Vector3(transform.position.x, transform.position.y, 0);

		_lastXPosition = transform.position.x;
	}
}
