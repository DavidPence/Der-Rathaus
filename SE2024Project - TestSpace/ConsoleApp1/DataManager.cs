using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

static class DataManager
{
    // Simple DTOs for safe serialization
    private class RatDto
    {
        public string ratName { get; set; }
        public int level { get; set; }
        public int hp { get; set; }
        public int stam { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int spd { get; set; }
    }

    private class PlayerDto
    {
        public int Money { get; set; }
        public List<RatDto> Rats { get; set; } = new();
    }

    private class TerryDto
    {   
        public int level { get; set; }
        public string name { get; set;}
        public int areaCode { get; set; }
        public double playerControlPrecent { get; set;}
        public double catControl { get; set;}
        public double birdControl { get; set;}
        public double dogControl { get; set;}
    }

    private static readonly JsonSerializerOptions _opts = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // Save Player and their rats to filePath (e.g. "PlayerData.json")
    public static void SavePlayer(string filePath, Player player)
    {
        Console.WriteLine($"Saving player data to {filePath}...");
        var dto = new PlayerDto
        {
            Money = player.money
        };

        foreach (var r in player.getPlayerRatRoster())
        {
            dto.Rats.Add(new RatDto {
                                        ratName = r.name, 
                                        level = r.level, 
                                        hp = r.hp,
                                        stam = r.stam, 
                                        atk = r.atk, 
                                        def = r.def, 
                                        spd = r.spd
                                    });
        }

        File.WriteAllText(filePath, JsonSerializer.Serialize(dto, _opts));

        //SaveTerrys("TerritoryData.json", terryList); // Placeholder for territory saving
        Console.WriteLine("Saved!");
    }

    public static void SaveTerrys(string filePath, List<Territory> terrys)
    {
        // write to application data folder so it's always consistent
        var fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, filePath));
        Console.WriteLine($"Saving Territory data to {fullPath}...");
        // Console.WriteLine($"Saving Territory data to {filePath}...");
        var dtoList = new List<TerryDto>();

        foreach (Territory terry in terrys)
        {
            var dto = new TerryDto
            {
                level = terry.level,
                name = terry.name,
                areaCode = terry.areaCode,
                playerControlPrecent = terry.playerControlPrecent,
                catControl = terry.catControl,
                birdControl = terry.birdControl,
                dogControl = terry.dogControl
            };
            dtoList.Add(dto);
        }

        var dir = Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrEmpty(dir)){ Directory.CreateDirectory(dir); }

        File.WriteAllText(fullPath, JsonSerializer.Serialize(dtoList, _opts));
        Console.WriteLine("Territory data saved!");
        
    }

    // Load Player from filePath. Returns a new Player instance populated from file.
    public static Player LoadPlayer(string filePath)
    {
        Console.WriteLine($"Loading player data from {filePath}...");
        var player = new Player();

        if (!File.Exists(filePath))
            return player;

        try
        {
            var json = File.ReadAllText(filePath);
            var dto = JsonSerializer.Deserialize<PlayerDto>(json, _opts);
            if (dto == null) return player;

            player.money = dto.Money;

            // Add saved rats using existing Rat constructor
            foreach (var r in dto.Rats)
            {
                var rat = new Rat(r.ratName, r.level, r.hp, r.stam, r.atk, r.def, r.spd);
                player.addToPlayerRatRoster(rat);
            }
        }
        catch
        {
            // fail safe: return empty defaults on error
            return new Player();
        }
        Console.WriteLine("Loaded!");
        return player;
    }

    public static List<Territory> LoadTerrys(string filePath)
    {
        var fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, filePath));
        Console.WriteLine($"Loading Territory data from {fullPath}...");
        //Console.WriteLine($"Loading Territory data from {filePath}...");
        var terryList = new List<Territory>();

        if (!File.Exists(fullPath))
            return terryList;

        try
        {
            var json = File.ReadAllText(fullPath);
            var dtoList = JsonSerializer.Deserialize<List<TerryDto>>(json, _opts);
            if (dtoList == null) return terryList;

            // Add saved terrys using existing Territory constructor
            foreach (var t in dtoList)
            {
                var terry = new Territory(t.level, t.name, t.areaCode, t.playerControlPrecent, t.catControl, t.birdControl, t.dogControl);
                terryList.Add(terry);
            }
        }
        catch
        {
            // fail safe: return empty defaults on error
            Console.WriteLine("Error loading Territory data.");
            return new List<Territory>();
        }
        Console.WriteLine("Territory data loaded!");
        return terryList;
    }
}