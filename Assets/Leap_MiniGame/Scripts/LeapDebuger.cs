using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using System.Linq;
using UnityEngine.UI;

public class LeapDebuger : MonoBehaviour
{
    public enum LR
    {
        L, R
    }

    public Text text;
    public LR lr = LR.L;

    private void Start()
    {
        
    }

    private void Update()
    {
        var hand = GetHand(lr);

        if (hand != null)
        {
            //text.text = $"FistStrength: {hand?.GetFistStrength()}\n" +
            //    $"PinchPos: {hand.GetPinchPosition()}\n" +
            //    $"PredictedPinchPos: {hand.GetPredictedPinchPosition()}\n" +
            //    $"GrabAngle: { hand.GrabAngle}\n" +
            //    $"GrabStrength: { hand.GrabStrength}\n" +
            //    $"IsPinching: { hand.IsPinching()}\n" +
            //    $"PalmarAxis: { hand.PalmarAxis()}\n" +
            //    $"PalmNormal: { hand.PalmNormal}\n" +
            //    $"PalmPos: { hand.PalmPosition}\n" +
            //    $"PalmVelo: { hand.PalmVelocity}\n" +
            //    $"PalmWidth: { hand.PalmWidth}\n" +
            //    $"PinchDistance: { hand.PinchDistance}\n" +
            //    $"PinchStreangth: { hand.PinchStrength}\n" +
            //    $"RadialAxis: { hand.RadialAxis()}\n" +
            //    $"Rotation: { hand.Rotation.ToQuaternion().eulerAngles}\n" +
            //    $"StabilizedPalmPos: { hand.StabilizedPalmPosition}\n" +
            //    $"TimeVisible: { hand.TimeVisible}\n"
            //;
        }
        else
        {
            text.text = "Hand is not detected";
        }
        
    }

    Leap.Hand GetHand(LR lr)
    {
        var hands = LeapHandAccessor.hands;
        return hands.FirstOrDefault(h => lr == LR.L ? h.IsLeft : h.IsRight);
    }
}
