using System.Collections;
using Movement;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementCameraTest : BaseTestSuite
{
    GameObject _gameObjectCaracter;
    GameObject _gameObjectCameraParent;
    GameObject _gameObjectCamera;
    MovementCamera _movementCamera;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Time.timeScale = 20.0f;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
    }

    [SetUp]
    public void SetUp()
    {
        _gameObjectCaracter = new GameObject();

        _gameObjectCameraParent = new GameObject();
        _gameObjectCameraParent.transform.SetParent(_gameObjectCaracter.transform);

        _gameObjectCamera = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        _gameObjectCamera.transform.SetParent(_gameObjectCameraParent.transform);
        _gameObjectCamera.transform.localPosition = new Vector3(0, 1.75f, -3);
        _gameObjectCamera.transform.localRotation = Quaternion.Euler(20, 0, 0);

        _movementCamera = _gameObjectCaracter.AddComponent<MovementCamera>();
        _movementCamera.PlayerInputs = Substitute.For<IPlayerInputs>();
        _movementCamera.PlayerCameraParent = _gameObjectCameraParent.transform;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_gameObjectCaracter);
    }

    [UnityTest]
    public IEnumerator CharacterDontRotateIfInputsAreZero()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(0);
        _movementCamera.PlayerInputs.LookVertical.Returns(0);
        Quaternion initialRotation = _gameObjectCaracter.transform.rotation;
        yield return RunFrames(10);
        Assert.AreEqual(initialRotation, _gameObjectCaracter.transform.rotation);
    }

    [UnityTest]
    public IEnumerator CharacterDontRotateIfInputVerticalIsNotZero()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(0);
        _movementCamera.PlayerInputs.LookVertical.Returns(1);
        Quaternion initialRotation = _gameObjectCaracter.transform.rotation;
        yield return RunFrames(10);
        Assert.AreEqual(initialRotation, _gameObjectCaracter.transform.rotation);
    }

    [UnityTest]
    public IEnumerator CharacterDontRotateXIfInputHorizontalPositive()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(1);
        _movementCamera.PlayerInputs.LookVertical.Returns(0);
        float initialRotation = _gameObjectCaracter.transform.rotation.x;
        yield return RunFrames(10);
        Assert.AreEqual(initialRotation, _gameObjectCaracter.transform.rotation.eulerAngles.x);
    }

    [UnityTest]
    public IEnumerator CharacterRotateYIfInputHorizontalPositive()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(1);
        _movementCamera.PlayerInputs.LookVertical.Returns(0);
        float initialRotation = _gameObjectCaracter.transform.rotation.y;
        yield return RunFrames(10);
        Assert.AreNotEqual(initialRotation, _gameObjectCaracter.transform.rotation.eulerAngles.y);
    }

    [UnityTest]
    public IEnumerator CharacterDontRotateXIfInputHorizontalNegative()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(-1);
        _movementCamera.PlayerInputs.LookVertical.Returns(0);
        float initialRotation = _gameObjectCaracter.transform.rotation.eulerAngles.x;
        yield return RunFrames(10);
        Assert.AreEqual(initialRotation, _gameObjectCaracter.transform.rotation.eulerAngles.x);
    }

    [UnityTest]
    public IEnumerator CharacterRotateYIfInputHorizontalNegative()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(-1);
        _movementCamera.PlayerInputs.LookVertical.Returns(0);
        float initialRotation = _gameObjectCaracter.transform.rotation.eulerAngles.y;
        yield return RunFrames(10);
        Assert.AreNotEqual(initialRotation, _gameObjectCaracter.transform.rotation.eulerAngles.y);
    }

    [UnityTest]
    public IEnumerator CameraParentDontRotateIfInputsAreZero()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(0);
        _movementCamera.PlayerInputs.LookVertical.Returns(0);
        Quaternion initialRotation = _gameObjectCameraParent.transform.localRotation;
        yield return RunFrames(10);
        Assert.AreEqual(initialRotation, _gameObjectCameraParent.transform.localRotation);
    }

    [UnityTest]
    public IEnumerator CameraParentDontRotateIfInputHorizontalIsNotZero()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(1);
        _movementCamera.PlayerInputs.LookVertical.Returns(0);
        Quaternion initialRotation = _gameObjectCameraParent.transform.localRotation;
        yield return RunFrames(10);
        Assert.AreEqual(initialRotation, _gameObjectCameraParent.transform.localRotation);
    }

    [UnityTest]
    public IEnumerator CameraParentRotateXIfInputVerticalIsPositive()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(0);
        _movementCamera.PlayerInputs.LookVertical.Returns(1);
        float initialRotation = _gameObjectCameraParent.transform.localRotation.eulerAngles.x;
        yield return RunFrames(10);
        Assert.AreNotEqual(initialRotation, _gameObjectCameraParent.transform.localRotation.eulerAngles.x);
    }

    [UnityTest]
    public IEnumerator CameraParentDontRotateYIfInputVerticalIsPositive()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(0);
        _movementCamera.PlayerInputs.LookVertical.Returns(1);
        float initialRotation = _gameObjectCameraParent.transform.localRotation.eulerAngles.y;
        yield return RunFrames(10);
        Assert.AreEqual(initialRotation, _gameObjectCameraParent.transform.localRotation.eulerAngles.y);
    }

    [UnityTest]
    public IEnumerator CameraParentRotateXIfInputVerticalIsNegative()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(0);
        _movementCamera.PlayerInputs.LookVertical.Returns(-1);
        float initialRotation = _gameObjectCameraParent.transform.localRotation.eulerAngles.x;
        yield return RunFrames(10);
        Assert.AreNotEqual(initialRotation, _gameObjectCameraParent.transform.localRotation.eulerAngles.x);
    }

    [UnityTest]
    public IEnumerator CameraParentDontRotateYIfInputVerticalIsNegative()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(0);
        _movementCamera.PlayerInputs.LookVertical.Returns(-1);
        float initialRotation = _gameObjectCameraParent.transform.localRotation.eulerAngles.y;
        yield return RunFrames(10);
        Assert.AreEqual(initialRotation, _gameObjectCameraParent.transform.localRotation.eulerAngles.y);
    }

    [UnityTest]
    public IEnumerator CameraParentRotateCalmpIfInputVerticalIsPositive()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(0);
        _movementCamera.PlayerInputs.LookVertical.Returns(1);
        _movementCamera.LookSpeed = 10;
        yield return RunFrames(10);
        Assert.AreNotEqual(_movementCamera.LookXLimit, _gameObjectCameraParent.transform.localRotation.eulerAngles.x);
    }

    [UnityTest]
    public IEnumerator CameraParentRotateCalmpIfInputVerticalIsNegative()
    {
        _movementCamera.PlayerInputs.LookHorizontal.Returns(0);
        _movementCamera.PlayerInputs.LookVertical.Returns(-1);
        _movementCamera.LookSpeed = 10;
        yield return RunFrames(10);
        Assert.AreNotEqual(-_movementCamera.LookXLimit, _gameObjectCameraParent.transform.localRotation.eulerAngles.x);
    }

    static float[] values = new float[] { -15f, 1, 3, 17 };
    [UnityTest]
    public IEnumerator RotateSpeedIsLookSpeedInputVerticalPositive([ValueSource(nameof(values))] float speed)
    {
        _movementCamera.LookSpeed = speed;
        yield return RunFrames(1);
        _movementCamera.PlayerInputs.LookVertical.Returns(1);
        float initialRotation = _gameObjectCameraParent.transform.localRotation.x;
        yield return RunFrames(1, true);
        float expected = initialRotation - (speed * Time.deltaTime);
        AreSame(expected, _gameObjectCameraParent.transform.localRotation.eulerAngles.x, true);
    }

    [UnityTest]
    public IEnumerator RotateSpeedIsNegativeLookSpeedInputVerticalNegative([ValueSource(nameof(values))] float speed)
    {
        _movementCamera.LookSpeed = speed;
        yield return RunFrames(1);
        _movementCamera.PlayerInputs.LookVertical.Returns(-1);
        float initialRotation = _gameObjectCameraParent.transform.localRotation.eulerAngles.x;
        yield return RunFrames(1, true);
        float expected = initialRotation + (speed * Time.deltaTime);
        AreSame(expected, _gameObjectCameraParent.transform.localRotation.eulerAngles.x, true);
    }

    [UnityTest]
    public IEnumerator RotateSpeedIsNevativeLookSpeedInputHorizontalPositive([ValueSource(nameof(values))] float speed)
    {
        _movementCamera.LookSpeed = speed;
        yield return RunFrames(1);
        _movementCamera.PlayerInputs.LookHorizontal.Returns(1);
        float initialRotation = _gameObjectCaracter.transform.localRotation.eulerAngles.y;
        yield return RunFrames(1, true);
        float expected = initialRotation + (speed * Time.deltaTime);
        AreSame(expected, _gameObjectCaracter.transform.localRotation.eulerAngles.y, true);
    }

    [UnityTest]
    public IEnumerator RotateSpeedIsLookSpeedInputHorizontalNegative([ValueSource(nameof(values))] float speed)
    {

        _movementCamera.LookSpeed = speed;
        yield return RunFrames(1);
        _movementCamera.PlayerInputs.LookHorizontal.Returns(-1);
        float initialRotation = _gameObjectCaracter.transform.localRotation.eulerAngles.y;
        yield return RunFrames(1, true);
        float expected = initialRotation - (speed * Time.deltaTime);
        AreSame(expected, _gameObjectCaracter.transform.localRotation.eulerAngles.y, true);
    }

}