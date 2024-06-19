using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //.. �������� ������ ���� ����
    public GameObject[] prefabs;

    //.. Ǯ�� ������ ����Ʈ ����
    List<GameObject>[] pools;

    public static PoolManager Instance;

    void Awake()
    {
        // �ϴ��� ���� ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        pools = new List<GameObject>[prefabs.Length];
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ... ������ Ǯ�� ��� (��Ȱ��ȭ ��) ���ӿ�����Ʈ�� ����
        //... ���� �߰��ϸ� select ������ �Ҵ��ϰ�
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ... ���߰��ϸ�
        if (!select)
        { // �߰����� ���ϰ� null ���� �״�ζ��
            //... ���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform); // instantiate ���Ӱ� �����ϰ� �ڱ� �ڽ� transform�� �ڽ����� ����
            pools[index].Add(select); // �̷��� �����Ǹ� pool�� �������
        }
        return select;
    }
}
