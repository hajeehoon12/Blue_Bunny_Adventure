using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public TextMeshPro conversationText;
    private SpriteRenderer npcsprite;
    public Transform moveTr;

    bool isFirstConversationEnd = false;
    bool isSecondConversationStart = false;
    bool isPlayerArrive = false;

    List<string> conversations = new List<string>();
    Vector2 playerPos => CharacterManager.Instance.Player.transform.position;

    private void Start()
    {
        npcsprite = GetComponentInChildren<SpriteRenderer>();
        DummyData_1();
        StartCoroutine(ReadText());
    }

    private void Update()
    {
        npcsprite.flipX = CharacterManager.Instance.Player.transform.position.x < this.transform.position.x;

        // 첫번째 대화가 끝나고 토끼가 이동한 다음 플레이어가 도착했을 때 나와야한다.
        if(isFirstConversationEnd && playerPos.x > 0 && !isSecondConversationStart)
        {
            isSecondConversationStart = true;
            DummyData_2();
            StartCoroutine(ReadText());
        }
    }

    private void DummyData_1()
    {
        conversations.Add("안녕!");
        conversations.Add("토끼들을 구해주러 왔구나!");
        conversations.Add("막 도착해서 정신이 없을 테지만..");
        conversations.Add("기본적인 조작법을 알려줄게!");
        conversations.Add("방향키 혹은 AD로 이동할 수 있어!");
        conversations.Add("점프는 스페이스바!");
        conversations.Add("공격은 z키나 마우스 왼쪽 버튼을 눌러봐!");
        conversations.Add("마지막으로 상호 작용 키는 윗 방향키를 누르면 돼! ");
    }

    private void DummyData_2()
    {
        conversations.Clear();
        conversations.Add("어떻게 벌써 여기까지 왔냐고?");
        conversations.Add("쉬프트 키를 누르면 대쉬가 된다는 건 알지?");
        conversations.Add("토끼라면 다 할 수 있을거야!");
        conversations.Add("몸 조심히 다녀와!");
    }


    IEnumerator ReadText()
    {
        foreach(string conversation in conversations)
        {
            StartCoroutine(ReadTextEach(conversation));
            yield return new WaitForSeconds(3f);
            conversationText.text = string.Empty;
        }

        isFirstConversationEnd = true;
        transform.position = moveTr.position;
    }

    IEnumerator ReadTextEach(string conversation)
    {
        char[] conversationSplit = conversation.ToCharArray();
        foreach(char c in conversationSplit)
        {
            conversationText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(Define.PLAYER_TAG))
        {           
            StopAllCoroutines();
            conversationText.text = string.Empty;
            StartCoroutine(ReadTextEach("말하는데 공격하다니... 설명 안해줄 테다.."));
        }
    }
}
