using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_SJB : MonoBehaviour
{
    private bool isFirstInteraction = true;

    public void Interact()
    {
        // ù ��ȣ�ۿ��� ��� ����Ʈ �߰�
        if (isFirstInteraction)
        {
            QuestManager.instance.AddQuest(new Quest(3, "[������ ��Ź]", "���ع� ������ ȸ�� �⹫�� ��Ź�Ͽ���. �ù踦 ���ο��� ����������."));
            isFirstInteraction = false;
        }
    }
}
