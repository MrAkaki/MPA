using System.Collections;
using Movement;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementBehaviourTest : BaseTestSuite
{
    GameObject _ground;

    GameObject _gameObject;
    MovementBehaviour _movementBehaviour;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Time.timeScale = 20.0f;
        _ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _ground.transform.localScale = new Vector3(10, 1, 10);
        _ground.transform.localPosition = new Vector3(0, -1.6f, 0);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Object.DestroyImmediate(_ground);
    }

    [SetUp]
    public void SetUp()
    {
        _gameObject = new GameObject();
        _movementBehaviour = _gameObject.AddComponent<MovementBehaviour>();
        _movementBehaviour.PlayerInputs = Substitute.For<IPlayerInputs>();
        GameObject debugObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        debugObject.transform.SetParent(_gameObject.transform);
        debugObject.transform.localPosition = Vector3.zero;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_gameObject);
    }

    [Test]
    public void ShouldAddCharacterController()
    {
        Assert.NotNull(_gameObject.GetComponent<CharacterController>());
    }

    [UnityTest]
    public IEnumerator IfGravityIsZeroDontFall()
    {
        _ground.SetActive(false);
        _movementBehaviour.Gravity = 0;

        yield return RunFrames(10);
        _ground.SetActive(true);
        Assert.AreEqual(0,_gameObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator IfGravityIsPositiveShouldFall()
    {
        _ground.SetActive(false);
        _movementBehaviour.Gravity = 10;

        yield return RunFrames(10);
        _ground.SetActive(true);
        Assert.Less(_gameObject.transform.position.y, 0);
    }

    [UnityTest]
    public IEnumerator IfGravityIsNegativeShouldFloat()
    {
        _movementBehaviour.Gravity = -10;

        yield return RunFrames(10);
        Assert.Greater(_gameObject.transform.position.y, 0);
    }

    [UnityTest]
    public IEnumerator DontMoveIfIsInAir()
    {
        _ground.SetActive(false);
        _movementBehaviour.Speed = 10;
        _movementBehaviour.PlayerInputs.Vertical.Returns(1);
        _movementBehaviour.PlayerInputs.Horizontal.Returns(1);

        yield return RunFrames(10);
        _ground.SetActive(true);
        Assert.AreEqual(_gameObject.transform.position.z, 0);
        Assert.AreEqual(_gameObject.transform.position.x, 0);
    }

    [UnityTest]
    public IEnumerator DontJumpIfIsInAir()
    {
        _ground.SetActive(false);
        _movementBehaviour.JumpForce = 10;
        _movementBehaviour.PlayerInputs.Jump.Returns(true);

        yield return RunFrames(10);
        _ground.SetActive(true);
        yield return RunFrames(10);
        Assert.AreEqual(_gameObject.transform.position.y, 0);
    }

    [UnityTest]
    public IEnumerator JumpIfIsGrounded()
    {
        _movementBehaviour.JumpForce = 10;
        _movementBehaviour.Gravity = 10;
        _movementBehaviour.PlayerInputs.Jump.Returns(false);
        yield return RunFrames(10);
        _movementBehaviour.PlayerInputs.Jump.Returns(true);
        yield return RunFrames(1);

        Assert.Greater(_gameObject.transform.position.y, 0);
    }

    [UnityTest]
    public IEnumerator MoveZPositiveIfInputVeriticalPositive()
    {
        _movementBehaviour.Speed = 10;
        _movementBehaviour.Gravity = 10;
        _movementBehaviour.PlayerInputs.Vertical.Returns(1);

        yield return RunFrames(10);

        Assert.Greater(_gameObject.transform.position.z, 0);
    }

    [UnityTest]
    public IEnumerator MoveZNegativeIfInputVeriticalNegative()
    {
        _movementBehaviour.Speed = 10;
        _movementBehaviour.Gravity = 10;
        _movementBehaviour.PlayerInputs.Vertical.Returns(-1);

        yield return RunFrames(10);

        Assert.Less(_gameObject.transform.position.z, 0);
    }

    [UnityTest]
    public IEnumerator MoveXPositiveIfInputHorizontalPositive()
    {
        _movementBehaviour.Speed = 10;
        _movementBehaviour.Gravity = 10;
        _movementBehaviour.PlayerInputs.Horizontal.Returns(1);

        yield return RunFrames(10);

        Assert.Greater(_gameObject.transform.position.x, 0);
    }

    [UnityTest]
    public IEnumerator MoveXNegativeIfInputHorizontalNegative()
    {
        _movementBehaviour.Speed = 10;
        _movementBehaviour.Gravity = 10;
        _movementBehaviour.PlayerInputs.Horizontal.Returns(-1);

        yield return RunFrames(10);

        Assert.Less(_gameObject.transform.position.x, 0);
    }

    [UnityTest]
    public IEnumerator MoveXNegativeAndYPositiveIfInputHorizontalNegativeAndVerticalPositive()
    {
        _movementBehaviour.Speed = 10;
        _movementBehaviour.Gravity = 10;
        _movementBehaviour.PlayerInputs.Horizontal.Returns(-1);
        _movementBehaviour.PlayerInputs.Vertical.Returns(1);

        yield return RunFrames(10);

        Assert.Less(_gameObject.transform.position.x, 0);
        Assert.Greater(_gameObject.transform.position.z, 0);
    }

}
