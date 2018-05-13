using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Leap.Unity;
using Leap.Unity.Query;
using Leap.Unity.Interaction;
using Leap;

public class PinchObjSpawner : MonoBehaviour
{

    public InteractionBehaviour prefab;

    public float distanceThreshold = 0.05f;

    public float pinchThreshold = 0.9f;

    public bool hasPair
    {
        get
        {
            return left != null && right != null;
        }
    }

    Hand left;

    Hand right;

    bool isHoldingPrefab = false;

    InteractionBehaviour holdingObj;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasPair)
            FindPinchingPair();
        
        if (hasPair && !isHoldingPrefab)
        {
            var leftPos = left.GetPinchPosition();
            var rightPos = right.GetPinchPosition();
            var upWard = Vector3.Cross(Camera.main.transform.forward, rightPos - leftPos).normalized;

            holdingObj = Instantiate(prefab,
                Vector3.Lerp(leftPos, rightPos, 0.5f)
                , Quaternion.LookRotation(Camera.main.transform.forward, upWard));

            holdingObj.GetComponent<Rigidbody>().isKinematic = true;

            isHoldingPrefab = true;
        }

        if (isHoldingPrefab)
        {
            //Release holding Object when not pinching
            if (right.PinchStrength < pinchThreshold || left.PinchStrength < pinchThreshold)
            {
                isHoldingPrefab = false;
                left = null;
                right = null;
                holdingObj.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

           
    }

    bool FindPinchingPair()
    {
        var hands = LeapHandAccessor.hands;

        var left = hands.Where(h => h.IsLeft && h.PinchStrength >= pinchThreshold);
        var right = hands.Where(h => h.IsRight && h.PinchStrength >= pinchThreshold);

        foreach (var l in left)
            foreach (var r in right)
                if (Vector3.Distance(l.GetPinchPosition(), r.GetPinchPosition()) < distanceThreshold)
                {
                    this.left = l;
                    this.right = r;
                }

        return hasPair;
    }
}
