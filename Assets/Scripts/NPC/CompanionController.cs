using UnityEngine;

public class CompanionController : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private float followSpeed = 2.0f;
	[SerializeField] private float followDistance = 1.0f;
	[SerializeField] private float playerIsTooFar = 5.0f;
	[SerializeField] private float teleportDistance = 2.0f;
	[SerializeField] SpriteRenderer _spriteRenderer;

	private Rigidbody2D _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		FollowPlayer();
	}

	private void FollowPlayer()
	{
		float distanceToPlayer = Vector2.Distance(transform.position, player.position);

		if (distanceToPlayer > playerIsTooFar)
		{
			TeleportNextToPlayer();
		}
		else if (distanceToPlayer > followDistance)
		{
			Vector2 direction = (player.position - transform.position).normalized;
			_rb.velocity = new Vector2(direction.x * followSpeed, _rb.velocity.y);

			// Flip the sprite based on the direction
			_spriteRenderer.flipX = direction.x > 0;
		}
		else
		{
			_rb.velocity = new Vector2(0, _rb.velocity.y);
		}
	}

	private void TeleportNextToPlayer()
	{
		Vector2 teleportPosition;

		if (PlayerMovement._instance.IsFacingRight())
		{
			teleportPosition = new Vector2(player.position.x - teleportDistance, player.position.y);
		}
		else
		{
			teleportPosition = new Vector2(player.position.x + teleportDistance, player.position.y);
		}

		transform.position = teleportPosition;
	}
}
