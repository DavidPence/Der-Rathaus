using System.Dynamic;
using System.Collections;
using System.Collections.Generic;

class Player{
    int money { get; set; }
    int reputation { get; set; }
    List<object> playerRatRoster = new List<object>();

    //Constructer
    public Player (){
        money = 200;
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

    //Returns ArrayList playerRatCollection
    public List<object> getPlayerRatRoster(){
            return playerRatRoster;
    }

    public object getARatFromRatRoster(int ratRosterID){
        return playerRatRoster[ratRosterID];
    }

    //Adds a Rat object to ArrayList playerRatCollection
    public void addToPlayerRatRoster(object newRat) {
        playerRatRoster.Add(newRat);
    }

}