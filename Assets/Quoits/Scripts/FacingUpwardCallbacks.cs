using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FacingUpwardCallbacks : MonoBehaviour
{
    public Transform facingTransform;

    public UnityEvent OnBeginFacing;
    public UnityEvent OnFacing;
    public UnityEvent OnEndFacing;

    private bool isFacing = false;
    private bool isFacingPrev = false;

    // Update is called once per frame
    void Update()
    {
        isFacingPrev = isFacing;
        isFacing = (IsFacing(facingTransform, Vector3.up));

        if (isFacing)
            OnFacing.Invoke();
        if (isFacing && isFacingPrev)
            OnBeginFacing.Invoke();
        if (!isFacing && isFacingPrev)
            OnEndFacing.Invoke();
    }

    public static bool IsFacing(Transform facingTransform, Vector3 direction, float minAllowedDotProduct = 0.8f)
    {
        return Vector3.Dot(-facingTransform.up, direction) > minAllowedDotProduct;
    }
}
