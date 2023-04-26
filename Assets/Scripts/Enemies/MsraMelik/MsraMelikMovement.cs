using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsraMelikMovement : MonoBehaviour
{
	[SerializeField] private GameObject point; // Reference to the GameObject point
	private bool shouldFall = false; // State to determine if MsraMelik should fall
	private Rigidbody2D msraMelikRigidbody; // Reference to MsraMelik's Rigidbody2D

	private void Start()
	{
		// Get the Rigidbody2D component
		msraMelikRigidbody = GetComponent<Rigidbody2D>();

		// Disable gravity for MsraMelik's Rigidbody2D
		msraMelikRigidbody.gravityScale = 0f;
	}

	private void Update()
	{
		// Check if PlayerMovement._instance is past the point and shouldFall is false
		if (PlayerMovement._instance.transform.position.x > point.transform.position.x && !shouldFall)
		{
			shouldFall = true;
			// Enable gravity for MsraMelik's Rigidbody2D
			msraMelikRigidbody.gravityScale = 1f;
		}
	}
}
