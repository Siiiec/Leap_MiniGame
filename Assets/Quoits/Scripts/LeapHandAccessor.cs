using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;
using System.Linq;
using Leap.Unity.Query;

public class LeapHandAccessor {

    private static LeapServiceProvider s_leap;

    public static LeapServiceProvider leapServiceProvider
    {
        get
        {
            if (s_leap == null)
            {
                s_leap = Object.FindObjectOfType<LeapServiceProvider>();
            }

            return s_leap;
        }
    }

    public static Hand left
    {
        get { return leapServiceProvider.CurrentFrame.Hands.FirstOrDefault(h => h.IsLeft); }
    }

    public static Hand right
    {
        get { return leapServiceProvider.CurrentFrame.Hands.FirstOrDefault(h => h.IsRight); }
    }

    public static List<Hand> hands
    {
        get { return leapServiceProvider.CurrentFrame.Hands; }
    }


    #region FixedFrame

    public static Hand leftFixed
    {
        get { return leapServiceProvider.CurrentFixedFrame.Hands.FirstOrDefault(h => h.IsLeft); }
    }

    public static Hand rightFixed
    {
        get { return leapServiceProvider.CurrentFixedFrame.Hands.FirstOrDefault(h => h.IsRight); }
    }

    public static List<Hand> handsFixed
    {
        get { return leapServiceProvider.CurrentFixedFrame.Hands; }
    }

    #endregion
}
