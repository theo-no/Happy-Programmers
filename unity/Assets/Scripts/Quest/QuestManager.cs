using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Quest(int id, string name, string description)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
    }
}

public class QuestManager : MonoBehaviour
{
    // �̱���
    public static QuestManager instance;

    private void Awake()
    {
        #region �̱���
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        #endregion

    }

    // ��� ����Ʈ�� �����ϴ� ��ųʸ�
    private Dictionary<int, Quest> quests = new Dictionary<int, Quest>();

    // �Ϸ��� ����Ʈ�� id���� ��� ����Ʈ
    private List<int> completedQuestIds = new List<int>();

    // ���� ����Ʈ�� id���� ��� ����Ʈ
    private List<int> receivedQuestIds = new List<int>();

    // ����Ʈ id������ ��������
    public Quest GetQuest(int questId)
    {
        // Ư�� ID�� ����Ʈ�� ��ȯ
        if (quests.TryGetValue(questId, out Quest quest))
        {
            return quest;
        }
        else
        {
            return null;
        }
    }

    // ����Ʈ �Ϸ�
    public void CompleteQuest(int questId)
    {
        // ���� �Ϸ����� ���� ����Ʈ��� ����Ʈ�� �߰�
        if (!completedQuestIds.Contains(questId))
        {
            completedQuestIds.Add(questId);
        }
    }

    // ����Ʈ �߰�
    public void AddQuest(Quest quest)
    {
        // �� ����Ʈ�� ��ųʸ��� �߰�
        if (!quests.ContainsKey(quest.Id))
        {
            quests.Add(quest.Id, quest);
        }
    }

    // ����Ʈ �޾Ҵ��� Ȯ��
    public bool IsQuestReceived(int questId)
    {
        // ���� ����Ʈ ����Ʈ�� �ִ��� Ȯ��
        if (receivedQuestIds.Contains(questId))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<Quest> GetCurrentQuests()
    {
        List<Quest> currentQuests = new List<Quest>();
        foreach (int id in receivedQuestIds)
        {
            if (!completedQuestIds.Contains(id))
            {
                currentQuests.Add(GetQuest(id));
            }
        }
        return currentQuests;
    }
}
