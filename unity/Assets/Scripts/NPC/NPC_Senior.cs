using UnityEngine;

public class NPC_Senior : MonoBehaviour
{
    private bool hasCollided = false; // �浹�� �ѹ��� ó���ϱ� ���� ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;
            QuestManager.instance.acceptQuest(0);
        }
    }
}
