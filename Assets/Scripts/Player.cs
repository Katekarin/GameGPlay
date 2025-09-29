using System.Collections.Generic;
using UnityEngine;

//placeholdery dla innegych plikow
public enum PlayerClass
{
    Peasant
}

[System.Serializable]
public class PassiveSkill
{
    public string name;
    public string description;
    public float bonusValue;
}

[System.Serializable]
public class Item
{
    public string itemName;
    public string type;
    public int attackBonus;
    public int defenseBonus;
    public int hpBonus;
}

//Gracz
public class PlayerStats : MonoBehaviour
{
    [Header("Podstawowe Statystyki")]
    public string playerName = "Hero";
    public int level = 1;
    public int strength = 1;
    public int dexterity = 1;
    public int endurance = 1;
    public int knowagle = 1;
    public int intuition = 1;

    [Header("Klasy postaci")]
    public PlayerClass primaryClass = PlayerClass.Peasant;

    [Header("Pasywki")]
    public List<PassiveSkill> passiveSkills = new List<PassiveSkill>();

    [Header("Ekwipunek")]
    public List<Item> inventory = new List<Item>();

    [Header("Za³o¿ony sprzêt")]
    public Item equippedWeapon;
    public Item equippedArmor;
    public Item equippedAccessory;



    // Gety
    public int GetAttackPower()
    {
        int baseAttack = strength * 2;
        if (equippedWeapon != null) baseAttack += equippedWeapon.attackBonus;
        return baseAttack;
    }

    public int GetDefensePower()
    {
        int baseDefense = endurance;
        if (equippedArmor != null) baseDefense += equippedArmor.defenseBonus;
        return baseDefense;
    }

    public int GetMaxHP()
    {
        int baseHP = endurance * 10;
        if (equippedArmor != null) baseHP += equippedArmor.hpBonus;
        return baseHP;
    }
}
