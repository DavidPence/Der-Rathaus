using System.Collections;
using System.Collections.Generic;

class CombatCPU {
    public string name { get; set; }
    string type { get; set; }
    double playerRepToNPC { get; set; }
    private List<object> cpuRatRoster = new List<object>();
    public string allegiance { get; set; }
    List<Territory> territoryList { get; set; }

    // Constructor
    public CombatCPU(string name, string type, double playerRepToNPC, string allegiance, List<Territory> territoryList) {
        this.name = name;
        this.type = type;
        this.playerRepToNPC = playerRepToNPC;
        this.allegiance = allegiance;
        this.territoryList = territoryList;
    }

    public List<Territory> getTerritoryList() {
        return territoryList;
    }

    public List<object> getCPURatRoster (){
        return cpuRatRoster;
    }
    public Rat getCPURat(int i){
            Rat tempRat = (Rat)cpuRatRoster[i];
            return tempRat;
    }

    public void addToCPURatRoster(object newRat) {
        cpuRatRoster.Add(newRat);
    }

    public void clearCPURatRoster() {
        cpuRatRoster.Clear();
    }

    // ToString
    public override string ToString() {
            return GetType().GetProperties()
                    .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"))
                    .Aggregate( new System.Text.StringBuilder(),
                               (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                                sb => sb.ToString()
                              );
    }

    public void debugDetails (){
        Console.WriteLine(  "Name: " + name +
                            "\nType: " + type +
                            "\nplayerRepTo NPC: " + playerRepToNPC +
                            "\nAllegiance: " + allegiance +
                            "\nFull Rat Roster: \n");
        foreach (Rat rat in cpuRatRoster) {
            Console.WriteLine("Rat: " + rat.name);
        }
        Console.WriteLine("\nFull 'Terry' list: \n");
        foreach (Territory terry in territoryList) {
            Console.WriteLine("" + terry.name);
        }
    }

    public void displayTerritories(){
        if (territoryList != null && territoryList.Count > 0) {
            Console.WriteLine($"Sure thing. Here's the list of locations still owned by {name}:");
            for (int i = 0; i < territoryList.Count; i++) {
                Console.WriteLine($"[{i+1}] - {territoryList[i].name} - Level {territoryList[i].level}");
            }
        } else {
            Console.WriteLine("Sorry Boss, we've already pick them clean");
        }
    }

    
}
 