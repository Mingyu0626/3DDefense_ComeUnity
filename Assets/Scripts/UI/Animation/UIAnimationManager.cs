using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Settings;
using UnityEngine;

public class UIAnimationManager
{
    private RectTransform slidePanelLeft, slidePanelRight;

    private float scalarSlideAnimation;


    public void InitSlidePanelRectTransform(ref RectTransform left, ref RectTransform right)
    {
        slidePanelLeft = left;
        slidePanelRight = right;
        scalarSlideAnimation = SavedSettingData.ResolutionWidth / 2;
        if (slidePanelLeft != null && right != null)
        {
            slidePanelLeft.anchoredPosition3D = new Vector3(
                -SavedSettingData.ResolutionWidth + SavedSettingData.ResolutionWidth / 4,
                slidePanelLeft.anchoredPosition3D.y,
                slidePanelLeft.anchoredPosition3D.z);
            slidePanelRight.anchoredPosition3D = new Vector3(
                SavedSettingData.ResolutionWidth - SavedSettingData.ResolutionWidth / 4,
                slidePanelRight.anchoredPosition3D.y,
                slidePanelRight.anchoredPosition3D.z);

            slidePanelLeft.localScale = new Vector3(
                SavedSettingData.ResolutionWidth / 2,
                SavedSettingData.ResolutionHeight,
                1f);
            slidePanelRight.localScale = new Vector3(
                SavedSettingData.ResolutionWidth / 2,
                SavedSettingData.ResolutionHeight,
                1f);
        }
    }
    public void AnimationSlideIn(float slideTime = 1f, System.Action onComplete = null)
    {
        slidePanelLeft.DOAnchorPosX(slidePanelLeft.anchoredPosition.x + scalarSlideAnimation, slideTime);
        slidePanelRight.DOAnchorPosX(slidePanelRight.anchoredPosition.x - scalarSlideAnimation, slideTime)
            .OnComplete(() => onComplete?.Invoke());
    }
    public void AnimationSlideIn(ref RectTransform left, ref RectTransform right, float slideTime = 1f, System.Action onComplete = null)
    {
        left.DOAnchorPosX(left.anchoredPosition.x + scalarSlideAnimation, slideTime);
        right.DOAnchorPosX(right.anchoredPosition.x - scalarSlideAnimation, slideTime)
            .OnComplete(() => onComplete?.Invoke());
    }
    public void AnimationSlideOut(float slideTime = 1f, System.Action onComplete = null)
    {
        slidePanelLeft.DOAnchorPosX(slidePanelLeft.anchoredPosition.x - scalarSlideAnimation, slideTime).SetEase(Ease.OutQuad);
        slidePanelRight.DOAnchorPosX(slidePanelRight.anchoredPosition.x + scalarSlideAnimation, slideTime).SetEase(Ease.OutQuad)
            .OnComplete(() => onComplete?.Invoke());
    }
    public void AnimationSlideOut(ref RectTransform left, ref RectTransform right, float slideTime = 1f, System.Action onComplete = null)
    {
        left.DOAnchorPosX(left.anchoredPosition.x - scalarSlideAnimation, slideTime).SetEase(Ease.OutQuad);
        right.DOAnchorPosX(right.anchoredPosition.x + scalarSlideAnimation, slideTime).SetEase(Ease.OutQuad)
            .OnComplete(() => onComplete?.Invoke());
    }
}