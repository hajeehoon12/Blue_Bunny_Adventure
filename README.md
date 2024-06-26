# [B-12조] "Blue_Bunny_Adventure"
Chapter 3-3 유니티 게임개발 심화

## 목차
1. [시연 영상](#시연-영상)
2. [게임 소개](#게임-소개)
3. [게임 조작](#게임-조작)
4. [개발 환경](#개발-환경)
5. [팀원](#팀원)
6. [구현 기능](#구현-기능)
7. [회의록](#회의록)
8. [PPT](#PPT)

## 시연 영상
https://youtu.be/NUNNsoQKPNc

## 게임 소개

![image](https://github.com/hajeehoon12/Blue_Bunny_Adventure/assets/107660181/37c0be33-46ac-4c74-acaf-1d38e45b23dc)

- `게임 명`
  - 블루 버니 어드벤처 🐰

- `게임 설명`
  - 파란 토끼가 2D 플랫폼을 탐험해 나가는 게임입니다.

## 게임 조작
- `플레이어 이동`
  - 화살표 or WASD

- `플레이어 점프`
  - Space Bar

- `플레이어 대시`
  - 왼쪽 Shift

- `플레이어 공격`
  - 마우스 왼쪽 클릭 or Z or C

- `인벤토리`
  - Tab

- `메뉴`
  - ESC

## 개발 환경
- `게임 엔진`
  - Unity(2022.3.17f1)

- `플러그인`
  - DoTween(Asset)

- `그래픽 소스`
  - Kenney 사이트에서 무료 에셋을 이용

- `기타`
  - IDE: Visual Studio Community 2022
  - Language: C#
  - Version Control: GitHub
  - Sound:  NealK

## 팀원
- 팀장 `하지훈`: 플레이어 이동, 공격, 사운드, 오브젝트 풀, 보스 몬스터

- 팀원 `이서영`: 몬스터(FSM), 몬스터HPBar, 저장 시스템(JSON)

- 팀원 `박도현`: Map (타일맵 형식), Item (프리팹), 카메라 세부 로직 조정, SpawnManager, 게임 매니저

- 팀원 `박관엽`: UI (체력바, 인벤토리 프레임, 일시정지 창 프레임), 오디오 볼륨 조정

## 구현 기능
- `플레이어`
  - 플레이어 이동(InputSystem)
  - 스킬 구현 (대시)
  - 탄환 구현
  - 스탯 구현 ( 체력, 마나, 공격력, 이동속도, 공격속도 )

- `몬스터`
  - 유한 상태 기계(Finite-State Machine, FSM)을 활용한 몬스터의 행동 구현
  - 보스 몬스터의 공격 패턴 구현

- `아이템`
  - 소모 아이템과 효과 아이템 구현
  - 아이템 적용 시, 플레이어의 스탯에 영향이 가도록 설정
  - 효과 아이템 획득 시, 인벤토리에 들어가게 되는 로직 구현
 
- `저장, 불러오기`
  - 직렬화(JSON)을 통해 플레이어 스탯, 골드, 아이템 등을 저장과 불러오기 구현

- `사운드`
  - BGM, SFX 삽입
  - 오디오 믹서를 이용한 볼륨 조절 기능

- `그 외 자세한 구현 기능들`
  - https://teamsparta.notion.site/a0df1b562237462d838b342786a4a8ec

## 회의록
https://teamsparta.notion.site/18f96f41bcb64153b177ed9da93b319f

## PPT
[TEAM B12조_블루 버니 어드벤처.pdf](https://github.com/user-attachments/files/15981906/TEAM.B12._.pdf)
