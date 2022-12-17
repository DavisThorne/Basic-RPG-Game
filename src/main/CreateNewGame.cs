using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static System.IO.FileMode;

namespace Basic_RPG_Game;

internal class CharacterInfo
{
    public string? Name { get; set; }
    public string? CharacterClass { get; set; }
    public int? Level { get; set; }
    
    public int? Health { get; set; }
    
    public int? InventorySize { get; set; }
    
    public Dictionary<string, int> Stats { get; set; }
    
    public Dictionary<string, Dictionary<string, int>> Inventory { get; set; }
}

internal class SaveData
{
    
    public string? SaveGameStage { get; set; }
    
}

public static class CreateNewGame
{
    private static string? NameInput()
    {
        Console.WriteLine("What is the name of your Character?");
        string? name = Console.ReadLine();
        CreateFile(name);
        return name;
    }

    private static string CreateFile(string? name)
    {
        var path = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\OneDrive\\Documents\\RPGGame\\");
        var timeDate = DateTime.Today;
        var saveDirectory = $"{path}{name}\\";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
        
        var playerDataFileName = $"{name}PlayerData{timeDate:dd-MM-yyyy}.json";
        var saveFileName = $"{name}SaveData{timeDate:dd-MM-yyyy}.json";
        var playerDataFullPath = Path.Combine(saveDirectory, playerDataFileName);
        var saveFullPath = Path.Combine(saveDirectory, saveFileName);
        Console.WriteLine(playerDataFullPath);
        File.Create(playerDataFullPath).Dispose();
        File.Create(saveFullPath).Dispose();
        DataCollection(name, playerDataFullPath, saveFullPath);
        return playerDataFullPath;
    }

    private static void DataCollection(string? name, string playerDataFullPath, string saveFullPath)
    {
        Console.WriteLine("Please Chose a Class. \n 1. Warrior \n 2. Mage \n 3. Archer");
        var classChoice = Console.ReadLine();
        string characterClass;
        var level = 1;

        switch (classChoice)
        {
            case "1":
                Console.WriteLine("You have chosen Warrior");
                characterClass = "Warrior";
                JsonParser(characterClass, level, name, playerDataFullPath, saveFullPath);
                break;
            case "2":
                Console.WriteLine("You have chosen Mage");
                characterClass = "Mage";
                JsonParser(characterClass, level, name, playerDataFullPath, saveFullPath);
                break;
            case "3":
                Console.WriteLine("You have chosen Archer");
                characterClass = "Archer";
                JsonParser(characterClass, level, name, playerDataFullPath, saveFullPath);
                break;
            default:
                Console.WriteLine("Please enter a valid number");
                break;
        }
    }

    private static void JsonParser(string characterClass, int level, string? name, string playerDataFullPath, string saveFullPath)
    {
        var stats = new Dictionary<string, int>();
        var inventory = new Dictionary<string, Dictionary<string, int>>();
        var weaponProperties = new Dictionary<string, int>();
        switch (characterClass)
        {
            case "Warrior":
                weaponProperties.Add("Damage", 10);
                weaponProperties.Add("Durability", 100);
                weaponProperties.Add("Weight", 10);
                stats.Add("Strength", 10);
                stats.Add("Dexterity", 5);
                stats.Add("Intelligence", 3);
                stats.Add("Vitality", 8);
                inventory.Add("Copper Sword", weaponProperties);
                break;
            case "Mage":
                stats.Add("Strength", 3);
                stats.Add("Dexterity", 5);
                stats.Add("Intelligence", 10);
                stats.Add("Vitality", 5);
                break;
            case "Archer":
                stats.Add("Strength", 5);
                stats.Add("Dexterity", 10);
                stats.Add("Intelligence", 5);
                stats.Add("Vitality", 5);
                break;
        }

        CharacterInfo characterInfo = new CharacterInfo
        {
            Name = name,
            CharacterClass = characterClass,
            Level = level,
            Stats = stats,
            Inventory = inventory,
            Health = 100,
            InventorySize = 15,
        };
        
        SaveData saveData = new SaveData
        {
            SaveGameStage = "NewGame"
        };
        
        string jsonStringPlayerData = JsonConvert.SerializeObject(characterInfo, Formatting.Indented);
        using (FileStream fs = File.Open(playerDataFullPath, FileMode.Open))
        {
            Byte[] json = new UTF8Encoding(true).GetBytes(jsonStringPlayerData);
            fs.Write(json);
        }
        
        string jsonStringSaveData = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        using (FileStream fs = File.Open(saveFullPath, FileMode.Open))
        {
            Byte[] json = new UTF8Encoding(true).GetBytes(jsonStringSaveData);
            fs.Write(json);
        }
        
        Console.WriteLine($"Save Data Directory: {saveFullPath} \nPlayer Data Directory: {playerDataFullPath}");
    }


    public static void Main()
    {
        Console.WriteLine("Welcome to the world of RPG!");
        NameInput();
    }
}

//q: How to add properties to a json element
//a: https://stackoverflow.com/questions/18138367/how-to-add-properties-to-a-json-element