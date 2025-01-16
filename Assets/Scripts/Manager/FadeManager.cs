using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager
{
    public IEnumerator FadeIn(Graphic obj, float fadeTime = 1f, System.Action onComplete = null)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            float alphaValue = Mathf.Lerp(0, 1, percent);
            obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, alphaValue);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        onComplete?.Invoke();
    }
    public IEnumerator FadeIn(IEnumerable<Graphic> objs, float fadeTime = 1f, System.Action onComplete = null)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            float alphaValue = Mathf.Lerp(0, 1, percent);
            foreach (var obj in objs)
            {
                obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, alphaValue);
            }
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        onComplete?.Invoke();
    }
    public IEnumerator FadeOut(Graphic obj, float fadeTime = 1f, System.Action onComplete = null)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            float alphaValue = Mathf.Lerp(1, 0, percent);
            obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, alphaValue);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        onComplete?.Invoke();
    }
    public IEnumerator FadeOut(IEnumerable<Graphic> objs, float fadeTime = 1f, System.Action onComplete = null)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            float alphaValue = Mathf.Lerp(1, 0, percent);
            foreach (var obj in objs)
            {
                obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, alphaValue);
            }
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        onComplete?.Invoke();
    }
    public IEnumerator FadeInColor(Graphic obj, float fadeTime = 1f, System.Action onComplete = null)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            float rgbValue = Mathf.Lerp(0, 1, percent);
            obj.color = new Color(rgbValue, rgbValue, rgbValue);
            yield return null;
        }
    }
    public IEnumerator FadeInColor(IEnumerable<Graphic> objs, float fadeTime = 1f, System.Action onComplete = null)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            float rgbValue = Mathf.Lerp(0, 1, percent);
            foreach (var obj in objs)
            {
                obj.color = new Color(rgbValue, rgbValue, rgbValue);
            }
            yield return null;
        }
    }
    public IEnumerator FadeOutColor(Graphic obj, float fadeTime = 1f, System.Action onComplete = null)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            float rgbValue = Mathf.Lerp(1, 0, percent);
            obj.color = new Color(rgbValue, rgbValue, rgbValue);
            yield return null;
        }
    }
    public IEnumerator FadeOutColor(IEnumerable<Graphic> objs, float fadeTime = 1f, System.Action onComplete = null)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            float rgbValue = Mathf.Lerp(1, 0, percent);
            foreach (var obj in objs)
            {
                obj.color = new Color(rgbValue, rgbValue, rgbValue);
            }
            yield return null;
        }
    }
}
