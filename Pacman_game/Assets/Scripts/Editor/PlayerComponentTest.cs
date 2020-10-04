using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerComponentTest {

    private Player PlayerComponent;

    [SetUp]
    public void SetUp()
    {
        PlayerComponent = Player.getInstance();
    }
    
    [Test]
    public void getLivesTest()
    {
        int lives = PlayerComponent.getLives();
        Assert.AreEqual(3, lives);
    }

    [Test]
    public void setLivesTest()
    {
        PlayerComponent.setLives();
        int lives = PlayerComponent.getLives();
        Assert.AreEqual(2, lives);
    }

    [Test]
    public void getInstanceTest()
    {
        Player player = Player.getInstance();
        Assert.AreSame(player, PlayerComponent);
    }

    [Test]
    public void getSpeedTest()
    {
        float speed = PlayerComponent.getSpeed();
        Assert.AreEqual(0.2f, speed);
    }

}
