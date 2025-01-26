using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] List<GameObject> cameraList = new List<GameObject>();
    private float switchTime = 10f;
    private void Awake()
    {
        if (cameraList.Count == 0)
        {
            Debug.LogError("ī�޶� ����Ʈ�� �������.");
            return;
        }
    }
    private void Start()
    {
        StartCoroutine(SwitchCamera());
    }

    private IEnumerator SwitchCamera()
    {
        int cameraIndex = 1;
        yield return new WaitForSecondsRealtime(switchTime);
        while (true)
        {
            Camera.main.gameObject.SetActive(false);
            cameraList[cameraIndex % cameraList.Count].SetActive(true);
            yield return new WaitForSecondsRealtime(switchTime);
            cameraIndex++;
        }
    }
}
