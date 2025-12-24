using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TakeDamageEnemy
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(0);
    }

    [UnityTest]
    public IEnumerator TakeDamageOneHitTake25DamageStayAlife()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == 0f);
        yield return new WaitForSeconds(2);

        TestTarget testTarget = FindTestTarget();
        yield return null;

        Assert.IsNotNull(testTarget);
        testTarget.TakeDamage(25);

        yield return new WaitForSeconds(1);
        Assert.AreEqual(25, testTarget.Health);
    }


    [UnityTest]
    public IEnumerator TakeDamageDieHitOnDie()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == 0f);
        yield return new WaitForSeconds(2);

        TestTarget testTarget = FindTestTarget();
        yield return null;

        Assert.IsNotNull(testTarget);
        testTarget.TakeDamage(50);

        Assert.AreEqual(0, testTarget.Health);
    }


    #region Find
    private TestTarget FindTestTarget()
    {
        TestTarget testTarget = null;
        TestTarget[] target = GameObject.FindObjectsByType<TestTarget>(FindObjectsSortMode.None);

        foreach (var item in target)
        {
            if (item.CompareTag("Test"))
            {
                return item;
            }
        }
        return testTarget;
    }

    #endregion
    [TearDown]
    public void Teardown()
    {
        SceneManager.UnloadSceneAsync(0);
    }
}