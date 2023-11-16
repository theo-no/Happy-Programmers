using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class GameQuestManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI content;

    void Update()
    {
        DisplayCurrentQuests();
    }

    public void DisplayCurrentQuests()
    {
        List<Quest> currentQuests = QuestManager.instance.GetCurrentQuests();

        if (currentQuests.Count > 0)
        {
            Quest quest = currentQuests[0];
            title.text = quest.Name;
            content.text = currentQuests[0].Description;
        }
        else
        {
            // ���� ���� ���� ����Ʈ�� ���� ������ �ش� UI�� ����ݴϴ�.
            title.text = "���� �������� ����Ʈ�� �����ϴ�.";
            content.text = "���ο� ����Ʈ�� NPC�� ��ȣ�ۿ��Ͽ� �޾��ּ���";
        }
    }
}
