using System.Dynamic;
using System.Collections;
using System.Collections.Generic;

class Faction{
    string name { get; set; }
    string type { get; set; }
    double playerRepToFaction { get; set; }
    List<object> ratList { get; set; }
    List<object> territoryList { get; set; }

    public Faction(string name, string type, double playerRepToFaction, List<object> territoryList){
        this.name = name;
        this.type = type;
        this.playerRepToFaction = playerRepToFaction;
        this.territoryList = territoryList;
    }

}