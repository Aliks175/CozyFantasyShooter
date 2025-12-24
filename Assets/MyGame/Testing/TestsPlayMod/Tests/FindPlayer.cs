using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class FindPlayer 
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(0);
    }

    [UnityTest]
    public IEnumerator FindPlayerSimpleSceneGoodRetern()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == 0f);
        yield return new WaitForSeconds(2);

        PlayerCharacter player = FindPlayerCharacter();
        yield return null;

        Assert.IsNotNull(player);
    }

    #region Find
    private PlayerCharacter FindPlayerCharacter()
    {
        PlayerCharacter playerCharacter = GameObject.FindFirstObjectByType<PlayerCharacter>();
        return playerCharacter;
    }

    #endregion
    [TearDown]
    public void Teardown()
    {
        SceneManager.UnloadSceneAsync(0);
    }
}