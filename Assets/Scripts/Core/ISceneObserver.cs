using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneObserver  
{
    void OnSceneChanged(string sceneName);

    // 디폴트 메서드
    void OnSceneClosed(string sceneName)
    {
        Debug.Log("UIManager를 제외한 다른 매니저 클래스들");
    }
}
