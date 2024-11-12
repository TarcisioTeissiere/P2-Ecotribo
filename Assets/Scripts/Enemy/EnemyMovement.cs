using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Vector2 screenBounds;
    private float spriteWidth;
    private float spriteHeight;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        spriteWidth = spriteRenderer.bounds.size.x / 2;
        spriteHeight = spriteRenderer.bounds.size.y / 2;
    }

    private void Update()
    {
        MoveTowardsPlayer();
        LimitMovement();
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
            spriteRenderer.flipX = direction.x < 0;
        }
    }

    private void LimitMovement()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x + spriteWidth, screenBounds.x - spriteWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y + spriteHeight, screenBounds.y - spriteHeight);
        transform.position = clampedPosition;
    }

    public bool IsMoving()
    {
        return player != null && Vector2.Distance(transform.position, player.position) > 0.1f;
    }
}
