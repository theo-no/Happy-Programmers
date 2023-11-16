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

        InitializeQuest();
    }


    // ����Ʈ ������ ��� ��ųʸ�
    private Dictionary<int, Quest> allQuest = new Dictionary<int, Quest>();

    // �Ϸ��� ����Ʈ�� ��� ����Ʈ
    private List<int> completedQuestIds = new List<int>();

    // ���� ����Ʈ�� ��� ����Ʈ
    private List<int> accpetedQuestIds = new List<int>();

    private void InitializeQuest()
    {
        AddQuest(new Quest(0, "[��� ù��]", "����ȣ �����ڴ��� �����ϽŴ�� ���������͸� Ÿ�� �ö󰡺���!"));
        AddQuest(new Quest(1, "[Ŀ�� �ɺθ�]", "������ ������ ��ǰ�ǿ� �ִ� Ŀ�Ǹ� ��������� ���״�."));
        AddQuest(new Quest(2, "[���� ��������]", "����� �븮�� �μ�ǿ� �ִ� ������ ��������� ���״�."));
        AddQuest(new Quest(3, "[������ ��Ź]", "���ع� ������ ȸ�� �⹫�� ��Ź�Ͽ���. �ù踦 ���ο��� ����������."));
        AddQuest(new Quest(4, "[�ù� ���� �������ֱ�]", "�ù� ���� ��Ұ� B11�� �Ǿ��ִ�. �����ο��� ����������."));

    }

    // ����Ʈ �Ϸ�
    public void CompleteQuest(int questId)
    {
        // ���� �Ϸ����� ���� ����Ʈ��� ����Ʈ�� �߰�
        if (!completedQuestIds.Contains(questId))
        {
            accpetedQuestIds.Remove(questId);
            completedQuestIds.Add(questId);
        }
    }

    // ����Ʈ ������ ����Ʈ �߰�
    public void AddQuest(Quest quest)
    {
        // �� ����Ʈ�� ��ųʸ��� �߰�
        if (!allQuest.ContainsKey(quest.Id))
        {
            allQuest.Add(quest.Id, quest);
        }
    }

    // ����Ʈ�� �����Ͽ� �߰�
    public void acceptQuest(int questId)
    {
        accpetedQuestIds.Add(questId);
    }
    
    public Quest GetQuest(int questId)
    {
        return allQuest[questId];
    }

    // ������ ����Ʈ Ȯ���ϱ�
    public List<Quest> GetCurrentQuests()
    {
        List<Quest> currentQuests = new List<Quest>();
        foreach (int id in accpetedQuestIds)
        {
            if (!completedQuestIds.Contains(id))
            {
                currentQuests.Add(GetQuest(id));
            }
        }
        return currentQuests;
    }
}
