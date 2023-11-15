using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_KSM : MonoBehaviour
{
    private bool isFirstInteraction = true;

    public void Interact()
    {
        // ù ��ȣ�ۿ��� ��� ����Ʈ �߰�
        if (isFirstInteraction)
        {
            QuestManager.instance.AddQuest(new Quest(2, "[���� ��������]", "����� �븮�� �μ�ǿ� �ִ� ������ ��������� ���״�."));
            isFirstInteraction = false;
        }
    }
}
