using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerPickUpWeaponSimplePickUpEquipWeapon
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(0);
    }

    [UnityTest]
    public IEnumerator PlayerPickUpWeaponSimplePickUpEquipWeaponWithEnumeratorPasses()
    {
        bool isChangeWeapon = false;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == 0f);
        yield return new WaitForSeconds(2);
        PickUp pickUp = FindWeapon();
        PlayerCharacter player = FindPlayer();

        Assert.IsNotNull(player);
        Assert.IsNotNull(pickUp);

        yield return null;
        pickUp.transform.position = player.transform.position;


        pickUp.BaseInteract();
        yield return null;

        player.PlayerData.ChooseWeapon.OnChangeWeapon += (_) => isChangeWeapon = true;
        player.PlayerData.ChooseWeapon.GiveWeapon(SlotNumber.OneSlot);

        yield return new WaitForSeconds(1);
        Assert.AreEqual(true, isChangeWeapon);

    }

    #region Find
    private PickUp FindWeapon()
    {
        PickUp pickUp = null;
        PickUp[] weapon = GameObject.FindObjectsByType<PickUp>(FindObjectsSortMode.None);

        foreach (var item in weapon)
        {
            if (item.CompareTag("Test"))
            {
                return item;
            }
        }
        return pickUp;
    }

    private PlayerCharacter FindPlayer()
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
