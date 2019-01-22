using UnityEngine;

public static class HelperUI  {

    public static string motivationText()
    {
        switch (Random.Range(0, 5))
        {
            case 0: return "AWESOME!";
            case 1: return "AMAZING!";
            case 2: return "GOOD!";
            case 3: return "NICE!";
            case 4: return "VERY GOOD!";
            case 5: return "WOW";
            default: return "WOW!";
        }
    }
}
