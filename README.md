<div align="center">
<h2>3D Defense 🎮</h2>
사방에서 몰려오는 적들을 처치하며 기지를 방어하고 목표 킬 수를 달성하는 3인칭 슈팅 게임, 3D Defense입니다!<br>
디자인보단, 기술적 실험 및 구조 설계에 집중한 프로젝트입니다.🍀<br>
해당 프로젝트는 2024년 비빔밥 유니티 세션 "Come Unity!"에 교육용 프로젝트로 사용되었습니다.
</div><br>

## 목차
  - [개요](#개요) 
  - [게임 설명](#게임-설명)
  - [사용 기술](#사용-기술)
  - [게임 플레이](#게임-플레이)
<br>

## 개요
| **프로젝트 명** | 3D Defense |
|:---:|:---:|
| **프로젝트 기간** | 2024.09 - 2024.12 |
| **기술 스택** | <img src="https://img.shields.io/badge/Unity-2022.3.48f1-000000?style=for-the-badge&logo=unity" height="25"> <img src="https://img.shields.io/badge/C%23-512BD4?style=for-the-badge&logo=csharp&logoColor=white" height="25"> <img src="https://img.shields.io/badge/Github-181717?style=for-the-badge&logo=github" height="25"> |
| **플랫폼 및 장르** | <img src="https://img.shields.io/badge/Platform-Windows-lightgrey?style=for-the-badge" height="25"> <img src="https://img.shields.io/badge/Genre-3D%20TPS-red?style=for-the-badge" height="25"> |
<br>


## 게임 설명
|![image](https://github.com/user-attachments/assets/779b6d5d-4d65-4f34-b9e0-f4476e0755d1)|![image](https://github.com/user-attachments/assets/6079ea2b-39bc-4428-a3e7-037e43e51ecd)|
|:---:|:---:|
|시작 화면|플레이 화면|

교내 Unity 팀프로젝트를 수행하면서, 기초 부재와 기술의 부족을 뼈저리게 느꼈습니다. 특히, 종료된 프로젝트의 코드를 볼때마다 "더 깨끗하게 짤 수 있었던 것 같은데" 라는 생각이 들었고, 기초를 다지면서 기술을 발전시키기 위한 프로젝트의 필요성을 느꼈습니다. <br>
당시 유행하던 사방에서 몰려오는 수많은 적을 쓰러뜨리며 성장하고 살아남는 뱀서라이크 장르를 채택했고, 기술 실험 및 클린 코드 설계 역량을 기르고자 3D Defense를 개발하게 되었습니다. <br><br>

#### 적들로부터 기지를 지켜라! 🛡️<br>
수많은 적들이 기지를 파괴하기 위해 몰려옵니다. 도중 플레이어를 발견시 플레이어를 공격하게 되고, 플레이어는 이러한 적들의 공격을 피해가며 기지를 지켜내야 합니다.
#### 다양한 종류의 적들을 막아내라! 👽<br>
슬라임, 거북 등 각기 다른 특성을 가진 적 유닛들이 등장합니다. 이들의 공격을 효과적으로 막아내야 합니다.

<br>

## 사용 기술
### 1. Generic Singleton
- 다수의 싱글톤 클래스에서 발생하는 코드 중복을 최소화하기 위해, 제네릭 싱글톤을 채용했습니다.
- 인스턴스 유일성 검사, DontDestroyOnLoad 구문, 프로퍼티 초기화 등의 코드 중복을 없앨 수 있었습니다.

<br>

### 2. Object Polling
- GO의 빈번한 생성 및 파괴에 발생하는 오버헤드를 없애기 위해, 오브젝트 풀링을 구현했습니다.
- UnityEngine.Pool 네임스페이스에서 제공하는 Pool 클래스를 이용하여 적과 탄막의 생성 및 파괴를 오브젝트 풀링으로 관리하게끔 했습니다.

<br>

### 3. MVP Pattern
- 인게임 로직과 UI 로직이 혼재하던 기존 코드 구조를 개선하고, 유지보수성을 향상시키기 위해 MVP 패턴을 사용했습니다.
- 스테이지 UI 시스템을 StageDataModel(M), UIStageView(V), StagePresenter(P)의 MVP 구조로 설계했습니다.
- UI와 인게임 로직이 분리되어 유지보수성과 가독성이 향상되었습니다.

<br>

### 4. FSM Based on State Pattern
- 다양한 적 상태(플레이어 추격, 플레이어 공격, 기지로 이동, 기지 공격)를 효과적으로 관리하기 위해, 상태 패턴 기반 FSM을 구현했습니다.
- 상태 인터페이스를 설계하고, 이를 구현하는 상태 클래스 및 상태 전환을 담당하는 컨텍스트 클래스를 설계했습니다.

<br>

### 5. PlayerPrefs
- PlayerPrefs를 이용한 환경설정 시스템을 구현했습니다.
- BaseSettings 추상 클래스를 구현하고, 이를 상속하는 4개의 설정 하위 카테고리(디스플레이, 게임플레이, 그래픽, 사운드) 클래스를 설계했습니다.




<br>

## 게임 플레이
### 조작법
| 구분 | 동작 | 입력 키 (Input) |
| :---: | :---: | :---: |
| **이동** | 플레이어 이동 | <kbd>W</kbd> <kbd>A</kbd> <kbd>S</kbd> <kbd>D</kbd> |
| **전투** | 공격 (탄막 발사) | <kbd>Mouse Left Click</kbd> |
| **카메라** | 시점 전환 | <kbd>Mouse Move</kbd> |

<br>

### 주요 화면
|로비|인게임 화면|게임 종료|설정|
|:---:|:---:|:---:|:---:|
|![image](https://github.com/user-attachments/assets/779b6d5d-4d65-4f34-b9e0-f4476e0755d1)|![image](https://github.com/user-attachments/assets/18269c39-3634-4c1e-b72e-93745ced567f)|![image](https://github.com/user-attachments/assets/6957b5b2-df9f-49ec-a82f-02ce0e550054)|![image](https://github.com/user-attachments/assets/15d2c9d1-f998-4a27-9213-14bf5775441c)|
|게임 시작 및 설정 변경|목표 킬 수를 달성해야하는 스테이지 시스템|승패 여부에 따른 플레이어 및 적 유닛의 애니메이션 차이|디스플레이, 그래픽, 게임플레이, 사운드|

