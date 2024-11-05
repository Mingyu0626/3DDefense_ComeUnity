using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }
    public Transform PlayerTransform { get; private set; }
    private int maxHp = 100;
    private int hp;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        hp = maxHp;
    }

    void Start()
    {
        
    }

    void Update()
    {
        PlayerTransform = transform;
    }
}
