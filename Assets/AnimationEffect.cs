using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationEffect : MonoBehaviour
{
    [Header(" Settings")]
    [SerializeField] private AnimaEffectType _type;

    [SerializeField] private float _duration = 3f;

    [SerializeField] private bool _reverseRotation = false;
    [SerializeField] private float _scaleFactor = 1.2f;

    /// <summary>
    /// Setting for sca
    /// </summary>
    [SerializeField] private float _scaleFactorX = 1.5f;
    [SerializeField] private float _scaleFactorY = 1.2f;
    [SerializeField] public AnimationCurve _aniCurveStrectch;

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
        Transform targetTrans = transform;
        if (_type == AnimaEffectType.Rotationz)
        {
            float zAxis = -360;
            if (_reverseRotation)
                zAxis *= -1;
            targetTrans.DORotate(new Vector3(0f, 0f, zAxis), _duration, RotateMode.FastBeyond360)
                                    .SetEase(Ease.Linear)
                                    .SetLoops(-1, LoopType.Restart);
        }
        else if (_type == AnimaEffectType.ScaleInOut)
        {
            float initialScale = targetTrans.localScale.x;
            targetTrans.DOScale(initialScale * _scaleFactor, _duration)
                            .SetEase(Ease.OutSine)
                            .SetLoops(-1, LoopType.Yoyo);
        }
        else if (_type == AnimaEffectType.StetchScale)
        {
            float initScaleX = targetTrans.localScale.x;
            float initScaleY = targetTrans.localScale.y;
            Sequence seq = DOTween.Sequence();
            seq.Append(targetTrans .DOScale(new Vector3(initScaleX * _scaleFactorX
                                                    , initScaleY * _scaleFactorY, 1)
                                                    , _duration)
                                .SetEase(_aniCurveStrectch))
                .AppendInterval(1).SetLoops(-1);

        }
    }
}
