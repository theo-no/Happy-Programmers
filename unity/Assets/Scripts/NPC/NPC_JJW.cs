using UnityEngine;

public class NPC_JJW : MonoBehaviour
{
    private bool isFirstInteraction = true;

    public void Interact()
    {
        // ù ��ȣ�ۿ��� ��� ����Ʈ �߰�
        if (isFirstInteraction)
        {
            QuestManager.instance.AddQuest(new Quest(1, "[Ŀ�� �ɺθ�]", "������ ������ ��ǰ�ǿ� �ִ� Ŀ�Ǹ� ��������� ���״�."));
            isFirstInteraction = false;
        }
    }
}
