using TMPro;
using UnityEngine;

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
        var currentQuests = QuestManager.instance.GetCurrentQuests();

        if (currentQuests.Count > 0)
        {
            title.text = currentQuests[0].Name;
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
