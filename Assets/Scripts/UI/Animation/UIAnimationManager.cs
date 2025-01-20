using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Settings;
using UnityEngine;

public class UIAnimationManager
{
    private float scalarSplitAnimation;
    public void InitSplitPanelRectTransform(ref RectTransform left, ref RectTransform right)
    {
        scalarSplitAnimation = SavedSettingData.ResolutionWidth / 2;
        if (left != null && right != null)
        {
            left.anchoredPosition3D = new Vector3(
                -SavedSettingData.ResolutionWidth + SavedSettingData.ResolutionWidth / 4,
                left.anchoredPosition3D.y,
                left.anchoredPosition3D.z);
            right.anchoredPosition3D = new Vector3(
                SavedSettingData.ResolutionWidth - SavedSettingData.ResolutionWidth / 4,
                right.anchoredPosition3D.y,
                right.anchoredPosition3D.z);

            left.localScale = new Vector3(
                SavedSettingData.ResolutionWidth / 2,
                SavedSettingData.ResolutionHeight,
                1f);
            right.localScale = new Vector3(
                SavedSettingData.ResolutionWidth / 2,
                SavedSettingData.ResolutionHeight,
                1f);
        }
    }
    public void AnimationSlideIn(ref RectTransform left, ref RectTransform right, System.Action onComplete = null)
    {
        left.DOAnchorPosX(left.anchoredPosition.x + scalarSplitAnimation, 1f);
        right.DOAnchorPosX(right.anchoredPosition.x - scalarSplitAnimation, 1f)
            .OnComplete(() => onComplete?.Invoke());
    }
    public void AnimationSlideOut(ref RectTransform left, ref RectTransform right, System.Action onComplete = null)
    {
        left.DOAnchorPosX(left.anchoredPosition.x - scalarSplitAnimation, 1f).SetEase(Ease.OutQuad);
        right.DOAnchorPosX(right.anchoredPosition.x + scalarSplitAnimation, 1f).SetEase(Ease.OutQuad)
            .OnComplete(() => onComplete?.Invoke());
    }
}
