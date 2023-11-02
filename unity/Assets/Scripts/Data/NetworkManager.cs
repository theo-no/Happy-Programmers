using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UnityWebRequestGet());
        StartCoroutine(UnityWebRequestPost());
    }

    IEnumerator UnityWebRequestGet()
    {
        string id = "wlsdnr214";
        string name = "������";
        
        string url = "http://k9d210.p.ssafy.io:8081";
        string urlWithParams = $"http://k9d210.p.ssafy.io:8081/{id}?name={name}";


        // url�� Get ��û ������
        UnityWebRequest www = UnityWebRequest.Get(url);

        // ������ �� ������ ��ٸ���
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            // ���信 ������ ���ٸ� ���䳻�� �ؽ�Ʈ ����غ���
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }

    IEnumerator UnityWebRequestPost()
    {
        string url = "http://k9d210.p.ssafy.io:8081";
        WWWForm form = new WWWForm();
        string id = "���̵�";
        string pw = "��й�ȣ";
        form.AddField("Username", id);
        form.AddField("Password", pw);
        // ���� �ּҿ� ������ �Է�
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if(www.error == null)
        {
            // ������ ���
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("error");
        }
    }
}
