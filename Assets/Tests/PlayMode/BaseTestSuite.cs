using NUnit.Framework;
using System.Collections;
using UnityEngine;

public abstract class BaseTestSuite
{
    protected IEnumerator RunFrames(int numberOfFrames, bool isStarted = false)
    {
        if (!isStarted)
        {
            yield return null; // Awake
            yield return null; // Start
        }
        for (int i = 0; i < numberOfFrames; ++i)
        {
            yield return null; // Update
        }
    }

    protected void AreSame(float expected, float actual, bool areAngles = false)
    {
        if (areAngles)
        {
            expected = NormalizeAngles(expected);
            actual = NormalizeAngles(actual);
        }
        Assert.IsTrue(
            Mathf.Approximately(expected, actual),
            "Should be similar expected: " + expected.ToString() + " actual: " + actual.ToString()
            ); ;
    }

    private float NormalizeAngles(float angle)
    {
        while (angle < 0) angle += 360;
        while (angle > 360) angle -= 360;
        return angle;
    }
}