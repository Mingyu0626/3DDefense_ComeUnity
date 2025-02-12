using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementPresenter : MonoBehaviour
{
    [SerializeField] private BasementDataModel basementDataModel;
    [SerializeField] private UIBasementView basementView;

    
    private void Start()
    {
        basementDataModel.BasementHP = basementDataModel.BasementHPMax;
        basementDataModel.BasementHPChanged += basementView.SetSliderBasementHP;
        basementView.InitSliderBasementHP(basementDataModel.BasementHPMax);
    }

    public void OnBasementDamaged(int damage)
    {
        basementDataModel.BasementHP -= damage;
    }
    public int GetBasementHP() { return basementDataModel.BasementHP; }
}
