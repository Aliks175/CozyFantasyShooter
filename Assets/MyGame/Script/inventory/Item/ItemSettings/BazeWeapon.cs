using UnityEngine;

[CreateAssetMenu(fileName = "BazeWeapon", menuName = "Create/Item/Weapon")]
public class BazeWeapon : BazeItem
{
    [Header("Weapon")]
    public DataWeapon DataWeapon;
}