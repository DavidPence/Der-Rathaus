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
            Thread.Sleep(300); 
        }
    }

    public string getBasicPlayerInfo() {
        string allRatNamesInRoster= "";
        int G = money;
        foreach (Rat rat in playerRatRoster){
            allRatNamesInRoster += ($"{rat.ratName}\n");
        }
        return $"\nYour Rat(s):\n{allRatNamesInRoster}\nG : {G}\n";
    }

    //Adds a Rat object to List<> playerRatCollection
    public void addToPlayerRatRoster(object newRat) {
        if (getRatRosterCount() <= 5) {
        playerRatRoster.Add(newRat);
        } else {
            Console.WriteLine ("At maximium # of Rat you can own at a time (Sell a Rat to make room)");
        }
    }

    public void addToActivePlayerRatRoster(object newRat) {
        activePlayerRatRoster.Add(newRat);
    }

    public void removePlayerRat(int i){
        playerRatRoster.RemoveAt(i);
    }

    public void clearActivePlayerRatRoster(){
        activePlayerRatRoster.Clear();
    }

}