using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CircleLight : MonoBehaviour
{
    public Color tsToColor;
    public float LoopTime = 0.7f;
    Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
        _image.DOColor(tsToColor, LoopTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
