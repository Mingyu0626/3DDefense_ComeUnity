# 3D Defense ComeUnity

![Unity Version](https://img.shields.io/badge/Unity-2022.3.48f1-000000?logo=unity)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey)
![Genre](https://img.shields.io/badge/Genre-3D%20Defense-red)

> **한 줄 소개:** Unity 학습 목적으로 개발된 3D 타워 디펜스 게임으로, 적들의 침입을 막아내는 전략적 방어 게임입니다.

## 🎮 Game Play

플레이어는 3인칭 시점에서 캐릭터를 조작하여 몰려오는 적들을 격퇴해야 합니다. 적을 처치하거나 본진을 사수하면 승리하고, 본진이 파괴되면 패배합니다.

## 📝 About The Project

**3D Defense ComeUnity**는 2024년 2학기 비빔밥 세션 - 커뮤니티(Come Unity) 세션의 교육 목적으로 제작된 3D 디펜스 게임입니다. 이 프로젝트는 Unity 게임 개발의 핵심 개념들을 학습하고 실습하기 위한 교육용 리포지토리입니다.

### Key Features

* **전략적 방어 시스템**: 3인칭 시점에서 다가오는 적들을 효과적으로 방어
* **다양한 적 유형**: Slime, TurtleShell 등 각기 다른 특성을 가진 적 유닛
* **State Machine 기반 AI**: 적 캐릭터들의 지능적인 행동 패턴 구현
* **직관적인 UI/UX**: 게임 진행 상황을 명확하게 표시하는 유저 인터페이스

## 🛠 Tech Stack & Architecture

이 프로젝트는 **Unity**와 <strong>C#</strong>을 사용하여 개발되었습니다.

* **Engine:** Unity 2022.3.48f1
* **IDE:** Visual Studio / JetBrains Rider
* **Version Control:** Git

### Key Technical Implementations

* **Design Patterns**
  * Singleton 패턴: GameManager, UIManager, AudioManager 등 핵심 매니저 클래스에 적용
  * Observer 패턴: 씬 전환 및 게임 이벤트 시스템 구현
  * State 패턴: 적 캐릭터 AI의 FSM(Finite State Machine) 구현

* **Optimization**
  * Object Pooling: 총알, 적 오브젝트 등의 재사용을 통한 메모리 관리 및 가비지 컬렉션 최소화
  * PoolAble 인터페이스 기반의 통합 오브젝트 풀링 시스템

* **AI System**
  * Navigation Mesh 기반 적 길찾기
  * State Machine을 활용한 적 행동 패턴 구현 (이동, 공격, 사망 등)

* **Input System**
  * Unity의 New Input System 적용
  * InputManager를 통한 중앙집중식 입력 관리

* **UI/Animation**
  * DOTween을 활용한 부드러운 UI 애니메이션
  * TextMesh Pro를 활용한 고품질 텍스트 렌더링

## 📂 Directory Structure

주요 소스 코드는 아래 경로에서 확인할 수 있습니다.

```text
Assets/
├── Scripts/
│   ├── Audio/              # 오디오 매니저 및 사운드 관리
│   ├── Basement/           # 본진 관련 스크립트
│   ├── Camera/             # 카메라 제어 및 추적
│   ├── Core/               # 게임 매니저, 싱글톤, 오브젝트 풀링
│   │   ├── GameManager.cs
│   │   ├── InputManager.cs
│   │   ├── ObjectPoolManager.cs
│   │   └── Singleton.cs
│   ├── Enemy/              # 적 캐릭터 및 AI
│   │   ├── State/          # FSM 기반 적 상태 관리
│   │   ├── Spawner/        # 적 생성 시스템
│   │   ├── Slime/
│   │   └── TurtleShell/
│   ├── Player/             # 플레이어 컨트롤러
│   │   ├── PlayerAction.cs
│   │   ├── PlayerFire.cs
│   │   └── PlayerMove.cs
│   ├── Stage/              # 스테이지 관련 로직
│   └── UI & Animation/     # UI 매니저 및 애니메이션
│       ├── UIManager.cs
│       ├── UIAnimationManager.cs
│       └── Scene/          # 씬별 UI 관리
├── Scenes/
│   ├── LoadingScene
│   ├── LobbyScene
│   ├── GameScene
│   └── GameEndScene
├── Prefabs/
│   ├── Enemy/
│   ├── UI/
│   └── Player/
├── Resources/              # 런타임 로드 리소스
└── External Assets/        # 외부 에셋 (모델, 이펙트 등)
```

## 🎯 Core Systems

### Game Flow
게임은 4개의 주요 씬으로 구성됩니다.
1. **LoadingScene**: 게임 초기 로딩
2. **LobbyScene**: 메인 메뉴 및 게임 시작
3. **GameScene**: 실제 게임플레이
4. **GameEndScene**: 승리/패배 결과 화면

### Observer Pattern Implementation
씬 전환 시 Observer 패턴을 활용하여 각 오브젝트들이 씬 변경 이벤트를 구독하고 반응합니다.

```csharp
// ISceneObserver 인터페이스를 통한 씬 변경 감지
public interface ISceneObserver
{
    void OnSceneChanged(string sceneName);
    void OnSceneClosed(string sceneName);
}
```

### Object Pooling System
빈번하게 생성/파괴되는 오브젝트들의 성능 최적화를 위해 오브젝트 풀링을 구현했습니다.

* 총알, 적 캐릭터, 이펙트 등에 적용
* PoolAble 인터페이스 기반의 통합 관리
* 메모리 단편화 방지 및 GC 부하 감소

## 🎮 How to Play

1. 프로젝트를 Unity 2022.3.48f1 버전으로 엽니다
2. Scenes 폴더에서 LoadingScene을 실행합니다
3. WASD 키로 이동, 마우스로 조준 및 클릭으로 발사합니다
4. 물밀듯 밀려오는 적들을 처치하거나 본진을 사수하세요!

## 🤝 Contributing

이 프로젝트는 교육 목적의 프로젝트입니다. 개선 사항이나 버그 리포트는 이슈를 통해 제안해주세요.

## 📜 License

이 프로젝트는 MIT 라이선스 하에 배포됩니다. 자세한 내용은 [LICENSE](LICENSE) 파일을 참조하세요.

## 🙏 Acknowledgments

* **Unity Asset Store**: 게임에 사용된 다양한 외부 에셋
* **DOTween**: 부드러운 UI 애니메이션 구현
* **TextMesh Pro**: 고품질 텍스트 렌더링
* **비빔밥 세션**: 커뮤니티(Come Unity) 학습 프로그램

---

**Note**: 이 프로젝트는 학습 목적으로 제작되었으며, 지속적으로 개선되고 있습니다.
