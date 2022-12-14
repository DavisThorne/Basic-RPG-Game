using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static System.IO.FileMode;

namespace Basic_RPG_Game;

class CharacterInfo
{
    public string name { get; set; }
    public string characterClass { get; set; }
    public int level { get; set; }
    public List<string> stats { get; set; }
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
        var timeDate = DateTime.Now;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var fileName = $"{name}{timeDate:dd-MM-yyyy-HH-mm-ss}.json";
        var fullPath = Path.Combine(path, fileName);
        Console.WriteLine(fullPath);
        File.Create(fullPath).Dispose();
        DataCollection(name, fullPath);
        return fullPath;
    }

    private static void DataCollection(string? name, string fullPath)
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
                JsonParser(characterClass, level, name, fullPath);
                break;
            case "2":
                Console.WriteLine("You have chosen Mage");
                characterClass = "Mage";
                JsonParser(characterClass, level, name, fullPath);
                break;
            case "3":
                Console.WriteLine("You have chosen Archer");
                characterClass = "Archer";
                JsonParser(characterClass, level, name, fullPath);
                break;
            default:
                Console.WriteLine("Please enter a valid number");
                break;
        }
    }

    private static void JsonParser(string characterClass, int level, string? name, string fullPath)
    {
        var stats = new List<string>();
        switch (characterClass)
        {
            case "Warrior":
                stats.Add("Strength: 10");
                stats.Add("Dexterity: 5");
                stats.Add("Intelligence: 3");
                stats.Add("Vitality: 8");
                break;
            case "Mage":
                stats.Add("Strength: 3");
                stats.Add("Dexterity: 5");
                stats.Add("Intelligence: 10");
                stats.Add("Vitality: 5");
                break;
            case "Archer":
                stats.Add("Strength: 5");
                stats.Add("Dexterity: 10");
                stats.Add("Intelligence: 5");
                stats.Add("Vitality: 5");
                break;
        }

        CharacterInfo characterInfo = new CharacterInfo
        {
            name = name,
            characterClass = characterClass,
            level = level,
            stats = stats
        };

        string jsonString = JsonConvert.SerializeObject(characterInfo, Formatting.Indented);
        using (FileStream fs = File.Open(fullPath, FileMode.Open))
        {
            Byte[] json = new UTF8Encoding(true).GetBytes(jsonString);
            fs.Write(json);
        }

        Console.WriteLine(jsonString);
    }


    public static void Main()
    {
        Console.WriteLine("Welcome to the world of RPG!");
        NameInput();
    }
}