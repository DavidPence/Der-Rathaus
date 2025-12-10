using System; 
using System.Dynamic;
using System.Collections;
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading; 
using System.Threading.Tasks; 

class Player{
    public int money { get; set; }
    int reputation { get; set; }
    List<Rat> playerRatRoster = new List<Rat>();
    public List<Rat> activePlayerRatRoster = new List<Rat>();

    //Constructer
    public Player ()
    {
        money = 400;
        reputation = 50;
    }   

    // ToString
        public override string ToString() 
        {
            return GetType().GetProperties()
                    .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"))
                    .Aggregate( new System.Text.StringBuilder(),
                                (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                                sb => sb.ToString()
                              );
        }

    //Returns List playerRatCollection
    public List<Rat> getPlayerRatRoster()
    {
        return playerRatRoster;
    }

    public Rat getPlayerRat(int i)
    {
        Rat tempRat = (Rat)playerRatRoster[i];
        return tempRat;
    }

    public Rat getActivePlayerRat(int i)
    {
        Rat tempRat = (Rat)playerRatRoster[i];
        return tempRat;
    }


    public List<Rat> getActivePlayerRatRoster()
    {
        return activePlayerRatRoster;
    }

    public int getRatRosterCount() 
    {
        return playerRatRoster.Count();
    }

    public int getActiveRatRosterCount() 
    {
        return activePlayerRatRoster.Count();
    }

    public void InfoDumpOfRatRoster () 
    {
        int i = 1;
        foreach (object rat in playerRatRoster) 
        {
            Console.WriteLine("vvv Rat [" + i + "] vvv");
            Console.WriteLine(rat.ToString());
            i++;
            Thread.Sleep(300); 
        }
    }

    public void NameDumpOfActiceRatRoster () 
    {
        int i = 1;
        Console.WriteLine("Order of Line-up");
        foreach (Rat rat in activePlayerRatRoster) 
        {
            Console.WriteLine($"[{i}] - {rat.name}");
            i++;
            Thread.Sleep(300); 
        }
    }

    public string getBasicPlayerInfo() 
    {
        string allNamesInRoster = "";
        int G = money;
        foreach (Rat rat in playerRatRoster){
            allNamesInRoster += ($"{rat.name}\n");
        }
        return $"\nYour Rat(s):\n{allNamesInRoster}\nG : {G}\n";
    }

    //Adds a Rat object to List<> playerRatCollection
    public void addToPlayerRatRoster(Rat newRat) 
    {
        if (getRatRosterCount() <= 5) {
        playerRatRoster.Add(newRat);
        } else {
            Console.WriteLine ("At maximium # of Rat you can own at a time (Sell a Rat to make room)");
        }
    }

    public void addToActivePlayerRatRoster(Rat newRat) 
    {
        activePlayerRatRoster.Add(newRat);
    }

    public void removePlayerRat(int i)
    {
        playerRatRoster.RemoveAt(i);
    }

    public void clearActivePlayerRatRoster()
    {
        activePlayerRatRoster.Clear();
    }

}