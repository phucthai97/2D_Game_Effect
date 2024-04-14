using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationEffect : MonoBehaviour
{
    [Header(" Settings")]
    [SerializeField] private AnimaEffectType _type;
    [Space(8)]

    [SerializeField] private float _duration = 3f;
    [SerializeField] private bool _reverseRotation = false;
    [SerializeField] private float _scaleFactor = 1.2f;
    [SerializeField] private AnimationCurve _aniCurveScaleInOut;

    public enum AnimaEffectType
    {
        Rotationz,
        ScaleInOut,
        StetchScale,
    }


    public AnimaEffectType Type { get => _type; set => _type = value; }

    // Start is called before the first frame update
    void Start()
    {
        Transform targetTransform = transform;
        if (_type == AnimaEffectType.Rotationz)
        {
            float zAxis = -360;
            if (_reverseRotation)
                zAxis *= -1;
            targetTransform.DORotate(new Vector3(0f, 0f, zAxis), _duration, RotateMode.FastBeyond360)
                                    .SetEase(Ease.Linear)
                                    .SetLoops(-1, LoopType.Restart);
        }
        else if (_type == AnimaEffectType.ScaleInOut)
        {
            float initialScale = targetTransform.localScale.x;
            targetTransform.DOScale(initialScale * _scaleFactor, _duration)
                            .SetEase(Ease.OutSine)
                            .SetLoops(-1, LoopType.Yoyo);
        }
        else if (_type == AnimaEffectType.StetchScale)
        {

        }
    }
}
