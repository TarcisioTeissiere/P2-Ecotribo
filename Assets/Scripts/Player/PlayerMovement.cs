using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector2 screenBounds;
    private float spriteWidth;
    private float spriteHeight;
    private float originalSpeed;
    private PlayerAnimation playerAnimation;
    private SpriteRenderer spriteRenderer;
    private Coroutine activeSpeedBoost; 

    private void Start()
    {
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;

        playerAnimation = GetComponent<PlayerAnimation>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Inicializa o SpriteRenderer para aplicar o flip
        originalSpeed = moveSpeed;
    }

    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
            moveY = 1f;
        if (Input.GetKey(KeyCode.S))
            moveY = -1f;
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x + spriteWidth / 2, screenBounds.x - spriteWidth / 2);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y + spriteHeight / 2, screenBounds.y - spriteHeight / 2);
        transform.position = clampedPosition;
        playerAnimation.SetMovementAnimation(moveDirection);

        if (moveX != 0)
        {
            spriteRenderer.flipX = moveX < 0; // Flip no eixo X se movendo para a esquerda
        }
    }

    public IEnumerator SpeedBoost(float duration, float boostAmount)
    {
        moveSpeed += boostAmount;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
    }

    public void ActivateSpeedBoost(float duration, float boostAmount)
    {
        if (activeSpeedBoost != null)
        {
            StopCoroutine(activeSpeedBoost); 
            moveSpeed = originalSpeed; 
        }
        
        activeSpeedBoost = StartCoroutine(SpeedBoost(duration, boostAmount));
    }
}
