using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Settings;
using TMPro;
using UnityEngine;

public class UIAnimationManager
{
    private RectTransform slidePanelLeft, slidePanelRight;



    public void InitSlidePanelRectTransform(ref RectTransform left, ref RectTransform right)
    {
        slidePanelLeft = left;
        slidePanelRight = right;
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
    public void SlideIn(float slideTime, float scalarSlideAnimation, params System.Action[] onCompleteActions)
    {
        slidePanelLeft.DOAnchorPosX(slidePanelLeft.anchoredPosition.x + scalarSlideAnimation, slideTime);
        slidePanelRight.DOAnchorPosX(slidePanelRight.anchoredPosition.x - scalarSlideAnimation, slideTime)
            .OnComplete(() =>
            {
                foreach (var action in onCompleteActions)
                {
                    action?.Invoke();
                }
            });
    }
    public void SlideIn(ref RectTransform left, ref RectTransform right, float slideTime, float scalarSlideAnimation, params System.Action[] onCompleteActions)
    {
        left.DOAnchorPosX(left.anchoredPosition.x + scalarSlideAnimation, slideTime);
        right.DOAnchorPosX(right.anchoredPosition.x - scalarSlideAnimation, slideTime)
            .OnComplete(() =>
            {
                foreach (var action in onCompleteActions)
                {
                    action?.Invoke();
                }
            });
    }
    public void SlideOut(float slideTime, float scalarSlideAnimation, params System.Action[] onCompleteActions) 
    {
        slidePanelLeft.DOAnchorPosX(slidePanelLeft.anchoredPosition.x - scalarSlideAnimation, slideTime).SetEase(Ease.OutQuad);
        slidePanelRight.DOAnchorPosX(slidePanelRight.anchoredPosition.x + scalarSlideAnimation, slideTime).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                foreach (var action in onCompleteActions)
                {
                    action?.Invoke();
                }
            });
    }
    public void SlideOut(ref RectTransform left, ref RectTransform right, float slideTime, float scalarSlideAnimation, params System.Action[] onCompleteActions)
    {
        left.DOAnchorPosX(left.anchoredPosition.x - scalarSlideAnimation, slideTime).SetEase(Ease.OutQuad);
        right.DOAnchorPosX(right.anchoredPosition.x + scalarSlideAnimation, slideTime).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                foreach (var action in onCompleteActions)
                {
                    action?.Invoke();
                }
            });
    }

    public void Fade(ref TextMeshProUGUI tmp, float targetFadeValue, float fadeTime, params System.Action[] onCompleteActions)
    {
        tmp.DOFade(targetFadeValue, fadeTime).OnComplete(() =>
        {
            foreach (var action in onCompleteActions)
            {
                action?.Invoke();
            }
        });
    }
    public void FadeIn(ref TextMeshProUGUI tmp, float fadeTime, params System.Action[] onCompleteActions)
    {
        tmp.DOFade(255, fadeTime).OnComplete(() =>
        {
            foreach (var action in onCompleteActions)
            {
                action?.Invoke();
            }
        });
    }
    public void FadeOut(ref TextMeshProUGUI tmp, float fadeTime, params System.Action[] onCompleteActions)
    {
        tmp.DOFade(0, fadeTime).OnComplete(() =>
        {
            foreach (var action in onCompleteActions)
            {
                action?.Invoke();
            }
        });
    }

}
