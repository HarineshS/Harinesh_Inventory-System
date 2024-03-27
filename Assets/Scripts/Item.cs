using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(menuName = "New Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public int id;
    public ItemType type;
    public GameObject Asset;
    public ActionType actionType;
    [Header("Only UI")]
    public bool stackable = true;
    [Header("Both")]
    public Sprite image;

    public bool Automatic;


    public enum ItemType
    {
        Weapon,Consumable,Collectible

    }

    public enum ActionType
    {
        Hit,Shoot,Consume
    }
    




    





}
