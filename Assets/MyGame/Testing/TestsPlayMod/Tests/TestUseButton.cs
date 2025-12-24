using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestUseButton
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnitySetUp]
    public void SetUp()
    {
        SceneManager.LoadScene(0);
    }

    //[UnityTest]
    //public IEnumerator TestUseButtonWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.

    //    yield return null;
    //    //WaitForSeconds(60f);
    //    //Assert.AreEqual(0, 0);
    //    //Assert.AreEqual(true, isPlay);
    //    // Проверяет равны ли элементы 
    //}

    [TearDown]
    public void TearDown()
    {
        LogAssert.Expect(LogType.Error, "Normal");
    }
}
