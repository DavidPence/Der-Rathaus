using System.Dynamic;
using System.Collections;
using System.Collections.Generic;

class Player{
    public int money { get; set; }
    int reputation { get; set; }
    List<object> playerRatRoster = new List<object>();
    public List<object> activePlayerRatRoster = new List<object>();

    //Constructer
    public Player (){
        money = 400;
        reputation = 50;
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

    //Returns List playerRatCollection
    public List<object> getPlayerRatRoster(){
            return playerRatRoster;
    }

    public List<object> getActivePlayerRatRoster(){
            return activePlayerRatRoster;
    }

    public int getRatRosterCount() {
        return playerRatRoster.Count();
    }

    public void InfoDumpOfRatRoster () {
        foreach (var rat in playerRatRoster) {
            Console.WriteLine(rat.ToString());
        }
    }

    // public object getARatFromRatRoster(int ratRosterID){
    //     return playerRatRoster[ratRosterID];
    // }

    //Adds a Rat object to List<> playerRatCollection
    public void addToPlayerRatRoster(object newRat) {
        playerRatRoster.Add(newRat);
    }

    public void addToActivePlayerRatRoster(object newRat) {
        playerRatRoster.Add(newRat);
    }

    public void clearActivePlayerRatRoster(){
        activePlayerRatRoster.Clear();
    }

}