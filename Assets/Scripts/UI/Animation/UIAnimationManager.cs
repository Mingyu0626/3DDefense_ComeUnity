using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Settings;
using TMPro;
using UnityEngine;

public class UIAnimationManager
{
    public void InitSlidePanelRectTransform(ref RectTransform left, ref RectTransform right)
    {
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
    public void Slide(ref RectTransform target, float slideTime, float scalarSlideAnimation, params System.Action[] onCompleteActions)
    {
        target.DOAnchorPosX(target.anchoredPosition.x + scalarSlideAnimation, slideTime)
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
