using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneObserver  
{
    void OnSceneChanged(string sceneName);
}
