using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneObserver  
{
    void OnSceneChanged(string sceneName);

    // ����Ʈ �޼���
    void OnSceneClosed(string sceneName)
    {
        Debug.Log("UIManager�� ������ �ٸ� �Ŵ��� Ŭ������");
    }
}
