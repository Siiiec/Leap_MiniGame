using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using System.Linq;

public class LeapTracker : MonoBehaviour
{
    [SerializeField]
    private LeapServiceProvider leap;

    public GameObject right;
    public GameObject left;

    private void Update()
    {
        var frame = leap.CurrentFrame;
        var hands = frame.Hands;

        var left = hands.FirstOrDefault(h => h.IsLeft);
        var right = hands.FirstOrDefault(h => h.IsRight);
        
    }

}
