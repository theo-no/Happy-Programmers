using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    static public CharacterMovement instance;

    public string currentMapName; // transferMap 스크립트에 있는 transferMapName 변수의 값을 저장.

    public float moveSpeed = 5f; // 이동 속도 설정    
    public float runMultiplier = 2f; // 달리기 배율
    public InventoryUI inventoryUI; // 인벤토리

    private Rigidbody2D rb;
    public Vector2 movement;
    public Vector2 lastDirection; // 캐릭터가 마지막으로 움직인 방향

    private CharacterAnimation characterAnimation;

    private void Start() 
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            rb = GetComponent<Rigidbody2D>();
            characterAnimation = GetComponent<CharacterAnimation>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ProcessInput(float moveX, float moveY)
    {
        if (moveX != 0 && moveY != 0)
        {
            if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
                moveY = 0f;
            else
                moveX = 0f;
        }

        movement = new Vector2(moveX, moveY).normalized * moveSpeed;
        if (movement != Vector2.zero) // 캐릭터가 움직이는 경우에만 lastDirection을 업데이트합니다.
    {
        lastDirection = movement;
    }
    }

    public void SetRunning(bool isRunning)
    {
        if (isRunning)
            rb.velocity = movement * runMultiplier;
        else
            rb.velocity = movement;

        characterAnimation.SetMoveAnimation(movement, isRunning);
    }

    public void SetAttacking(bool isAttacking)
    {
        characterAnimation.SetAttacking(isAttacking);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 아이템과 충돌한 경우
        if (collision.gameObject.tag == "Item")
        {
            // 아이템을 인벤토리에 추가
            ItemPickup itemPickup = collision.gameObject.GetComponent<ItemPickup>();

            if (itemPickup != null)
            {
                inventoryUI.AcquireItem(itemPickup.item);
                // 아이템 오브젝트 삭제
                Destroy(collision.gameObject);
            }
        }
    }
}
