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

    public Rat getPlayerRat(int i){
            Rat tempRat = (Rat)playerRatRoster[i];
            return tempRat;
    }

    public Rat getActivePlayerRat(int i){
            Rat tempRat = (Rat)playerRatRoster[i];
            return tempRat;
    }


    public List<object> getActivePlayerRatRoster(){
            return activePlayerRatRoster;
    }

    public int getRatRosterCount() {
        return playerRatRoster.Count();
    }

    public int getActiveRatRosterCount() {
        return activePlayerRatRoster.Count();
    }

    public void InfoDumpOfRatRoster () {
        int i = 1;
        foreach (object rat in playerRatRoster) {
            Console.WriteLine("vvv Rat [" + i + "] vvv");
            Console.WriteLine(rat.ToString());
            i++;
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

    public void removePlayerRat(int i){
        playerRatRoster.RemoveAt(i);
    }

    public void clearActivePlayerRatRoster(){
        activePlayerRatRoster.Clear();
    }

}