using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PillComponentTest {

    private Pill pill;

    [SetUp]
    public void SetUp()
    {
        pill = new Pill();
    }

    [Test]
    public void getPointsTest()
    {
        int p = pill.getPoints();
        Assert.AreEqual(100, p);
    }

    [Test]
    public void getSuperTest()
    {
        bool s = pill.getSuper();
        Assert.AreEqual(false, s);
    }

    [Test]
    public void setSuperTest()
    {
        pill.setSuper(true);
        bool s = pill.getSuper();
        Assert.AreEqual(true, s);
    }

	[Test]
	public void PillComponentTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator PillComponentTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
