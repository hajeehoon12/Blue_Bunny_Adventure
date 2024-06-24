using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //.. 프리팹을 저장할 변수 생성
    public GameObject[] prefabs;

    //.. 풀을 관리할 리스트 생성
    List<GameObject>[] pools;

    public static PoolManager Instance;

    void Awake()
    {
        // 일대일 대응 선언
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

        // ... 선택한 풀의 놀고 (비활성화 된) 게임오브젝트에 접근
        //... 만약 발견하면 select 변수에 할당하고
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ... 못발견하면
        if (!select)
        { // 발견하지 못하고 null 상태 그대로라면
            //... 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform); // instantiate 새롭게 생성하고 자기 자신 transform에 자식으로 생성
            pools[index].Add(select); // 이렇게 생성되면 pool에 집어넣음
        }
        return select;
    }


    public void DeleteAll()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
