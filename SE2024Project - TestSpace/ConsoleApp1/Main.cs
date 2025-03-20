using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System.Data.SqlTypes;
class Program
{
    public static void Main(string[] args)
    {
    
        Console.WriteLine("vvv STARTING PROGRAM vvv\n\n");

        //Variables

        bool canBuy = true;

        string[] ratAdjNameList = {"Cheesy", "Cheezy" , "The" , "Der"
                                    , "Slimey", "Terrible", "Hairy", "Nasty", "Awful", "Hideous", "Lousy", "Vuglar", "Rotten", "Detestable", "Stupid"
                                    , "Spiffy", "Clean", "Shining", "Fancy", "Dapper", "Dashing", "Chique", "Snazzy", "Neat", "Respectful"
                                    , "Muslce-Bound", "Strong", "Big", "Firm", "Powerful", "Brawny", "Mighty", "Stout", "Beefy", "Rugged", "Stalwart" 
                                    , "Zippy", "Blistering", "Quick", "Hasty", "Nippy", "Rapid", "Energetic", "Rushing"}; 

        string[] ratNounNameList = {"Rat", "Cheese", "Rattus", "Remy", "Maus", "Mouse", "Nezumi", "Mickey", "Minnie", "Suzy", "Jerry", "Mordax", "Mortimer", "Rizzo", "Stuart", "Fucker", "Pence", "Morse", "Dugan", "Pinky", "Brain", "Ratsby", "Ratsputin"};
        string[] catNounNameList = {"Cat", "Furball", "Felis", "Catus", "Kitten", "Kitty", "Kat", "Slivester", "Tom", "Putty-Tat"};

        int[,] MAP = {  {8,8,8,8,8,5,5,5,5,5},
                        {8,8,8,8,8,5,5,5,5,5},
                        {8,8,8,8,8,3,3,9,9,9},
                        {6,6,2,2,2,3,3,9,0,9},
                        {6,6,2,2,2,3,3,9,0,9},
                        {6,6,6,0,0,3,3,9,0,9},
                        {6,6,6,0,0,3,3,9,9,9},
                        {7,7,7,1,1,4,4,4,4,4},
                        {7,7,7,1,1,4,0,0,0,4},
                        {7,7,7,7,7,4,4,4,4,4}}; 
        
        //Main

        // Creates Territorys
        Territory Byway         = new Territory(1, "The Byway"         , 75.00, 25.00, 00.00, 00.00);
        Territory Thoroughfare  = new Territory(2, "The Thoroughfare"  , 25.00, 75.00, 00.00, 00.00);
        Territory Highroad      = new Territory(2, "The Highroad"      , 25.00, 00.00, 75.00, 00.00);
        Territory Swamp         = new Territory(2, "The Swamp"         , 25.00, 00.00, 00.00, 75.00);
        Territory NoMansLand    = new Territory(3, "No Man's Land"     , 10.00, 45.00, 45.00, 00.00);
        Territory Cage          = new Territory(3, "The Cage"          , 00.00, 00.00, 50.00, 50.00);
        Territory HellsKitchen  = new Territory(3, "Hell's Kitchen"    , 00.00, 50.00, 00.00, 50.00);
        Territory Backalley     = new Territory(4, "The Backalley"     , 00.00, 100.00, 00.00, 00.00);
        Territory Aviary        = new Territory(4, "The Aviary"        , 00.00, 00.00, 100.00, 00.00);
        Territory Pound         = new Territory(4, "The Pound"         , 00.00, 00.00, 00.00, 100.00);

        Player mainPlayer = new Player();
        CombatCPU Catsby = new CombatCPU("Theives' Cat", "gang", 50.00,"ThievesCat", [Byway, Thoroughfare, HellsKitchen, NoMansLand, Backalley]); 
        CombatCPU Emilio = new CombatCPU("Bird Mafia", "gang", 50.00, "BirdMafia", [Highroad, Cage, NoMansLand, Aviary]);
        CombatCPU Theodog = new CombatCPU("The Fuzz", "law", 50.00, "TheFuzz", [Swamp, HellsKitchen, Cage, Pound]);

        /*Names for bird leader
        Emilio "The Migrator" Bellini
        Vincenzo "The Nestkeeper" Varela


        
        */

        int numCheck (string num){
            while (true){
                try {
                    return Convert.ToInt32(num);
                } catch {
                    Console.Write("Please input valid #: ");
                    num = Console.ReadLine();
                }
            }
        }

        void MainMenu () {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to ...Derrr RrrATHaus...");
            int selector;
            Thread.Sleep(300); 
            while (true) {
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(500); 
                Console.WriteLine("*** Main Menu *** ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(mainPlayer.getBasicPlayerInfo());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[1] - Shop\n[2] - Fight\n[3] - Your Inventory\n[0] - Close Program\nEnter: ");
                selector = numCheck(Console.ReadLine());
                if (selector == 1) {
                    ShopMenu();
                } else if (selector == 2) {
                    mainPlayer.money = mainPlayer.money + TerritoryMenu();
                } else if (selector == 3) {
                    InventoryMenu();
                } else if (selector == 0) {
                    return;
                } 
                // Console.WriteLine("[1] - Shop\n[2] - Fight\n[3] - Your Inventory\n[0] - Close Program\n Enter: ");
                // selector = numCheck(Console.ReadLine());
            }
        }

        // ************* Shop *************
        
        // Has Rats created then display in text
        List<object> ShopDisplay (int numOfRatsToCreate, int RatLv) {
            int costOfRat = 1000;
            List<object> shopRatList = new List<object>();
            while (true){
                if (RatLv == 1) {
                    costOfRat = 100;
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + RatLv + " rat\n");
                        canBuy = false;
                        return shopRatList;
                    }
                    break;
                } else if (RatLv == 2) {
                    costOfRat = 500;
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + RatLv + " rat\n");
                        canBuy = false;
                        return shopRatList;
                    }
                    break;
                } else if (RatLv == 3) {
                    costOfRat = 1000;
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + RatLv + " rat\n");
                        canBuy = false;
                        return shopRatList;
                    }
                    break;
                }
            }
            //List<object> shopRatList = new List<object>();
            for (int i = 0; i < numOfRatsToCreate+1; i++) {
                shopRatList.Add( ratCreator(RatLv) );
            }
            int count = 0;
            Console.WriteLine(""); //For Formatting
            foreach (object shopRat in shopRatList) {
                count++;
                Console.WriteLine("vvv Displaying Rat [" + count + "] vvv");
                Console.WriteLine(shopRat.ToString());
                Thread.Sleep(300); 
            }
            Console.WriteLine("End of Shop Display");
            return shopRatList;
        }

        // Handles buying selected rat and add it to player rat list 
        void ShopBuy (int selector, List<object> shopRatList, int LvToCost) {
            int costOfRat = 1000;
            while (true){
                if (LvToCost == 1) {
                    costOfRat = 100;
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + LvToCost + " rat\n");
                        canBuy = false;
                        return;
                    }
                    break;
                } else if (LvToCost == 2) {
                    costOfRat = 500;
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + LvToCost + " rat\n");
                        canBuy = false;
                        return;
                    }
                    break;
                } else if (LvToCost == 3) {
                    costOfRat = 1000;
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + LvToCost + " rat\n");
                        canBuy = false;
                        return;
                    }
                    break;
                }
            }
            while (canBuy) {    
                if (selector == 0) {
                    Console.WriteLine("Returning\n");
                    break;
                } else if (selector == 1) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[0]);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Buying {shopRatList[0]}");
                    Console.ResetColor();
                    // Console.WriteLine($"Buying Rat 1");
                    mainPlayer.money = mainPlayer.money - costOfRat;
                    break;
                } else if (selector == 2) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[1]);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Buying {shopRatList[1]}");
                    Console.ResetColor();
                    mainPlayer.money = mainPlayer.money - costOfRat;
                    break;
                } else if (selector == 3) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[2]);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Buying {shopRatList[2]}");
                    Console.ResetColor();
                    mainPlayer.money = mainPlayer.money - costOfRat;
                    break;
                } else if (selector == 4) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[3]);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Buying {shopRatList[3]}");
                    Console.ResetColor();
                    mainPlayer.money = mainPlayer.money - costOfRat;
                    break;
                } else if (selector == 5) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[4]);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Buying {shopRatList[4]}");
                    Console.ResetColor();
                    mainPlayer.money = mainPlayer.money - costOfRat;
                    break;
                } else if (selector == 6) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[5]);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Buying {shopRatList[5]}");
                    Console.ResetColor();
                    mainPlayer.money = mainPlayer.money - costOfRat;
                    break;
                } else {
                    Console.WriteLine("Please input valid #: ");
                    selector = numCheck(Console.ReadLine());
                }
            }
        }

        void ShopSell (int selector) {
            string comfirm = "N";
            int ratSellPrice = ShopRatSellPrice( mainPlayer.getPlayerRat(selector-1) );
            Console.Write("Are you sure you want to sell " + mainPlayer.getPlayerRat(selector-1).name + " for " + ratSellPrice + "G Enter [Y/N]: ");
            comfirm = Console.ReadLine();
            if (comfirm == "Y") {
                mainPlayer.money = mainPlayer.money + ratSellPrice;
                Console.WriteLine("Selling " + mainPlayer.getPlayerRat(selector-1).name + "\n");
                mainPlayer.removePlayerRat(selector-1);
            } else {
                Console.WriteLine("Not Selling Rat, Returning...\n");
            }
        }

        int ShopRatSellPrice (Rat rat) {
            if (rat.level == 1) {
                return 50;
            } else if (rat.level == 2) {
                return 250;
            } else if (rat.level == 3) {
                return 500;
            } else {
                return 0;
            }
        }

        //bool canBuy =true;
        void ShopMenu (){
            canBuy = true;
            int input /*= numCheck(Console.ReadLine())*/;
            while (true) { 
                Thread.Sleep(500); 
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n***** Welcome to the Rats R Us*****");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(mainPlayer.getBasicPlayerInfo());
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("[1] Buy Rat\n[2] Sell Rat\n[0] Return\nEnter: ");
                input = numCheck(Console.ReadLine());
                if (input == 1) { // To Buy Rat
                    Console.Write("Lvl of Rats: ");
                    int LvOfRats = numCheck(Console.ReadLine());
                    List<object> shopRatList = new List<object>();
                    shopRatList = ShopDisplay(4, LvOfRats);
                    Thread.Sleep(500); 
                    if (canBuy) {
                        Console.WriteLine("Enter [1-5] To buy respective Rat, [0] to close: ");
                        int input2 = numCheck(Console.ReadLine());
                        ShopBuy(input2, shopRatList, LvOfRats);
                    }

                } else if (input == 2) { //To Sell Rat
                    if ( mainPlayer.getRatRosterCount() > 1) {
                        mainPlayer.InfoDumpOfRatRoster();
                        Console.Write("What Rat would you like to sell\nEnter: ");
                        ShopSell( numCheck(Console.ReadLine()) );
                    } else {
                        Console.WriteLine("Cannot sell any rats. You must have ONE at all times\n");
                    }

                } else if (input == 0) {
                    Console.WriteLine("Returning...\n****************");
                    Thread.Sleep(200); 
                    break;

                } else {
                    Console.Write("Please input valid #: ");
                    input = Convert.ToInt32(Console.ReadLine());
                }
                // Console.Write("[1] Buy Rat\n[2] Sell Rat\n[3] Return\nEnter: ");
                // input = Convert.ToInt32(Console.ReadLine());
            }
        }



        // ************* Inventory *************

        void InventoryMenu() {
            int selector;
            while (true) {
                Console.WriteLine("\n***** Your Inventory *****");
                Console.Write("[1] - Your Rat(s)\n[2] - Your Stats\n[0] - Return\nEnter: ");
                selector = numCheck(Console.ReadLine());
                if (selector == 1) {
                        mainPlayer.InfoDumpOfRatRoster();
                    } else if (selector == 2) {
                        Console.WriteLine("\nYour Money: " + mainPlayer.money);
                    } else if (selector == 0) {
                        break;
                    } else {
                        Console.WriteLine("Please input valid number: ");
                        selector = numCheck(Console.ReadLine());
                    }
            }
            Console.WriteLine("Returning...");
        }

        // ************* Combat/Combat Menu *************

        int TerritoryMenu (){
            Console.WriteLine("\nWho's on the choppin' block today:");
            Console.WriteLine("[1] - Cats\n[2] - Birds\n[3] - Dogs\n[0] - Return\nEnter: ");
            int selector = numCheck(Console.ReadLine());
            switch (selector) {
                case 1:
                    Console.WriteLine("**Cats**");
                    return CombatMenu(Catsby);
                case 2:
                    Console.WriteLine("**Birds**");
                    return CombatMenu(Emilio);
                case 3:
                    Console.WriteLine("**Dogs**");
                    return CombatMenu(Theodog);
                case 0:
                    return 0;
            }
            return 0;
        }

        int CombatMenu (CombatCPU opponent){
            opponent.displayTerritories();

            //Console.WriteLine("What Level of Rats do you want to fight\n[1] The Thourghfare - Level 1\n[2] Hell's Kitchen - Level 2\n[3] No Man's Land - Level 3\n[4] The Backalley - Level 4\n[0] - Return\nEnter: ");
            Console.Write("[0] - Return\nEnter: ");
            int selector = numCheck(Console.ReadLine());
            while (true) {  
                if (selector == 0) {
                    Console.WriteLine("Returning..."); // Could add effect
                    return 0;
                } else if (selector > 0 && selector <= opponent.getTerritoryList().Count){
                    return ChallengeSetupCombatMenu(opponent.getTerritoryList()[selector - 1].level, opponent);
                } else {
                    Console.WriteLine("Please input valid number: ");
                    selector = numCheck(Console.ReadLine());
                }

                



                // if (selector == 1) {
                //     return ChallengeSetupCombatMenu(1, opponent);
                // } else if (selector == 2) {
                //     return ChallengeSetupCombatMenu(2, opponent);
                // } else if (selector == 3) {
                //     return ChallengeSetupCombatMenu(3, opponent);
                // } else if (selector ==4) {
                //     return ChallengeSetupCombatMenu(4, opponent);
                // } else if (selector == 0) {
                //     Console.WriteLine("Returning...");
                //     return 0;
                // } else {
                //     Console.WriteLine("Please input valid number: ");
                //     selector = numCheck(Console.ReadLine());
                // }
            }
        }

        int ChallengeSetupCombatMenu (int LvSet, CombatCPU opponent){ //Problem here too maybe
            Console.WriteLine("What Challenge do you want to face\n[1] - 1 Enemy\n[2] - 3 Enemies\n[3] - 5 Enemies\n[0] - Return\nEnter: ");
            int selector = numCheck(Console.ReadLine());
            mainPlayer.clearActivePlayerRatRoster();
            while (true) {
                Console.Write("Choosing  case...");

                // case 1
                if (selector == 1) { //Player should always have at least one rat
                    Console.WriteLine("CASE 1");
                    cpuRatRosterSetup(1, LvSet, opponent);
                    playerRatRosterSetup(1);
                    return ACTUALFUCKINGRATFIGHT(mainPlayer.getActivePlayerRatRoster(), opponent.getCPURatRoster(), 1, LvSet, opponent);
                // case 2
                } else if (selector == 2) {
                    Console.WriteLine("CASE 2");
                    if (mainPlayer.getRatRosterCount() >= 3) {
                        cpuRatRosterSetup(3, LvSet, opponent);
                        playerRatRosterSetup(3);
                        return ACTUALFUCKINGRATFIGHT(mainPlayer.getActivePlayerRatRoster(), opponent.getCPURatRoster(), 3, LvSet, opponent);
                    } else {
                        Console.WriteLine("You do not have enough rats");
                        return 0;
                    }
                // case 3
                } else if (selector == 3) {
                    Console.WriteLine("CASE 3");
                    if (mainPlayer.getRatRosterCount() >= 5) {
                        cpuRatRosterSetup(5, LvSet, opponent);
                        playerRatRosterSetup(5);
                        return ACTUALFUCKINGRATFIGHT(mainPlayer.getActivePlayerRatRoster(), opponent.getCPURatRoster(), 5, LvSet, opponent);
                    } else {
                        Console.WriteLine("You do not have enough rats");
                        return 0;
                    }
                // case 0 (failsafe)
                } else if (selector == 0) {
                    return 0;
                } else {
                    Console.WriteLine("Please input valid number: ");
                    selector = numCheck(Console.ReadLine());
                }

                // failsafe for failsafe
                Console.WriteLine("Returning...\n");
                return 0;
            }
        }

        // sets up cpu roster based on who's who
        // This could be done way better
        void cpuRatRosterSetup (int numOfRatsToCreate, int Lv, CombatCPU opponent) { //Problem here probably
            Console.WriteLine("Setting up CPU Cats");
            opponent.clearCPURatRoster();
            Console.WriteLine($"Choosing case based on {opponent.allegiance}...");
            switch (opponent.allegiance) {
                case "ThievesCat":
                    Console.WriteLine("CASE 1");
                    for (int i = 0; i < numOfRatsToCreate; i++) {
                        Console.WriteLine($"Adding {i+1} cat");
                        Catsby.addToCPURatRoster( catCreator(Lv) );
                    } break;
                case "BirdMafia":
                    Console.WriteLine("CASE 2");
                    for (int i = 0; i < numOfRatsToCreate; i++) {
                        Emilio.addToCPURatRoster( birdCreator(Lv) );
                    } break;
                case "TheFuzz":
                    Console.WriteLine("CASE 3");
                    for (int i = 0; i < numOfRatsToCreate; i++) {
                        Theodog.addToCPURatRoster( dogCreator(Lv) );
                    } break;
            }
        }
        
        void playerRatRosterSetup(int numOfRatsAllowed){
            Console.WriteLine(numOfRatsAllowed + " Rat(s) are allowed, and you get to choose who fights and the order.");
            Console.WriteLine("\nDisplaying your Rats\n");
            mainPlayer.InfoDumpOfRatRoster();

            for (int i = 0; i < numOfRatsAllowed; i++){
                if (mainPlayer.getActiveRatRosterCount() == 5){
                    Console.WriteLine("Active Roster Full");
                    break;
                }

                Console.WriteLine("Choose Rat: ");
                int selector = numCheck(Console.ReadLine());

                if (selector < 1 || selector > mainPlayer.getPlayerRatRoster().Count)
                {
                    Console.WriteLine("Invalid selection. Try again.");
                    i--;  // Retry this iteration
                    continue;
                }

                var selectedRat = mainPlayer.getPlayerRat(selector - 1);

                // Check for duplicates
                if (mainPlayer.getActivePlayerRatRoster().Contains(selectedRat))
                {
                    Console.WriteLine("Rat already in active roster");
                    continue;
                }

                // Add rat to active roster
                Console.WriteLine($"Adding {selectedRat.name}");
                mainPlayer.addToActivePlayerRatRoster(selectedRat);

                Console.WriteLine("Current Active Roster:");
                // foreach (var rat in mainPlayer.getActivePlayerRatRoster())
                //      Console.WriteLine(rat.name);
                for (int j = 0; j<mainPlayer.getActiveRatRosterCount()-1; j++){
                    Console.WriteLine(mainPlayer.getActivePlayerRat(j).name);
                }
                Console.WriteLine("Conitueing");
            }
        }

        int ACTUALFUCKINGRATFIGHT (List<object> playersRats, List<object> enemysRats, int numOfFighters, int fightLv, CombatCPU opponent){
            Console.WriteLine("FUCKING FIGHTING");
            if (RatFightHandler(numOfFighters, opponent)){
                Console.WriteLine("Player Wins");
                if (fightLv == 1) {
                    return 40*numOfFighters;
                } else if (fightLv == 2) {
                    return 200*numOfFighters;
                } else if (fightLv == 3) {
                    return 400*numOfFighters;
                } else if (fightLv == 4) {
                    return 800*numOfFighters;  
                } else {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("If you are seeing this I did something wrong :P");
                    Console.ResetColor();
                    return 0*numOfFighters;
                }
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CPU Wins");
                Console.ResetColor();
                return 0;
            }
        }

        // Atk/(2^(Atk/Def))
        bool RatFightHandler(int numOfFighters, CombatCPU opponent) {
            int wins = 1;
            int loses = 1;
            for (int i = 0; i < numOfFighters; i++) {
                Console.WriteLine($"\n*******************************************\nRound {i+1}, Fight!");
                if (RatFight(i, opponent)) {
                    wins++;
                } else {
                    loses++;
                }
            }
            if (wins/loses >= 1) {
                return true;
            } else {
                return false;
            }
        }

        bool RatFight (int i, CombatCPU opponent) {
            Random rnd = new Random();
            // player rat vars
            string PlayerName = mainPlayer.getActivePlayerRat(i).name;
            float PlayerAtk = mainPlayer.getActivePlayerRat(i).atk;
            float PlayerDef = mainPlayer.getActivePlayerRat(i).def;
            float PlayerSpd = mainPlayer.getActivePlayerRat(i).spd;
            float PlayerHp = mainPlayer.getActivePlayerRat(i).hp;
            bool playerTurn = true;

            // CPU rat vars
            Console.WriteLine("i: " + i);
            //Console.WriteLine("Printing CPU: \n" + opponent.getCPURatRoster);
            opponent.debugDetails();
            string CPUName = opponent.getCPURat(i).name; // ***FAILING HERE***
            /*
            Unhandled exception. System.ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')
            at System.Collections.Generic.List`1.get_Item(Int32 index)
            at CombatCPU.getCPURat(Int32 i) in C:\Users\dpenc\Documents\GitHub\Der-Rathaus\SE2024Project - TestSpace\ConsoleApp1\CombatCPU.cs:line 29
            at Program.<Main>g__RatFight|0_15(Int32 i, CombatCPU opponent, <>c__DisplayClass0_0&) in C:\Users\dpenc\Documents\GitHub\Der-Rathaus\SE2024Project - TestSpace\ConsoleApp1\Main.cs:line 563
            */
            float CPUAtk = opponent.getCPURat(i).atk;
            float CPUDef = opponent.getCPURat(i).def;
            float CPUSpd = opponent.getCPURat(i).spd;
            float CPUHp = opponent.getCPURat(i).hp;
            //bool CPUTurn = false;

            // Damage values for each side
            float playerDmg = (float)( PlayerAtk / Math.Pow(2, (double)PlayerAtk / CPUDef) );
            float CPUDmg = (float)( CPUAtk / Math.Pow(2, (double)CPUAtk / PlayerDef) );

            // decides who goes first
            if (PlayerSpd < CPUSpd) {
                playerTurn = false;
            } else if (PlayerSpd == CPUSpd) {
                if (rnd.Next(0,2) == 1) {
                    // CPU wins tie
                    playerTurn = false;
                }
            }

            // Rats trade blows till K.O.
            while (true) {
                if (playerTurn) {
                    CPUHp -= playerDmg;
                    Console.WriteLine($"{CPUName} took {Math.Round(playerDmg, 2)} and is at {Math.Round(CPUHp, 2)}");
                    playerTurn = false;
                } else {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    PlayerHp -= CPUDmg;
                    Console.WriteLine($"{PlayerName} took {Math.Round(CPUDmg, 2)} and is at {Math.Round(PlayerHp, 2)}");
                    playerTurn = true;
                    Console.ResetColor();
                }
                Thread.Sleep(1000); 

                if (CPUHp <= 0) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"********\n{PlayerName} Has won!\nPlayer wins round {i+1}\n********\n");
                    Thread.Sleep(2000); 
                    Console.ResetColor();
                    return true;
                }
                if (PlayerHp <= 0) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"********\n{CPUName} Has won!\nCPU wins round {i+1}\n********\n");
                    Thread.Sleep(2000); 
                    Console.ResetColor();
                    return false;
                }
            }
                

            
        }

        // Creates a single rat object with randomized stats
        Rat ratCreator(int level)
        {
            Random rnd = new Random();
            int adjRndNum = rnd.Next(ratAdjNameList.Length);//grabs rnd adj from list
            int nounRndNum = rnd.Next(ratNounNameList.Length);//grabs rnd noun from list
            string newName = ratAdjNameList[adjRndNum]+" "+ratNounNameList[nounRndNum];//Combines adj & noun
            switch (level) {
                case 1:
                    Rat RatLv1 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(5, 11)//hp
                                            ,rnd.Next(1, 11)//stam
                                            ,rnd.Next(1, 11)//atk
                                            ,rnd.Next(1, 11)//def
                                            ,rnd.Next(1, 11)//spd
                                        );
                    Console.WriteLine("**************\nLv1 Rat created\n*******************");
                    return RatLv1;
                case 2:
                    // Random rnd = new Random();
                    Rat RatLv2 = new Rat(  newName//Name :P
                                            ,level
                                            ,rnd.Next(15, 21)//hp
                                            ,rnd.Next(10, 21)//stam
                                            ,rnd.Next(10, 21)//atk
                                            ,rnd.Next(10, 21)//def
                                            ,rnd.Next(10, 21)//spd
                                        );
                    //Console.WriteLine(RatLv2.ToString());
                    return RatLv2;
                case 3:
                    //Random rnd = new Random();
                    Rat RatLv3 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(25, 31)//hp
                                            ,rnd.Next(15, 31)//stam
                                            ,rnd.Next(15, 31)//atk
                                            ,rnd.Next(15, 31)//def
                                            ,rnd.Next(15, 31)//spd
                                        );
                    //Console.WriteLine(RatLv3.ToString());
                    return RatLv3;
                case 4:
                    //Random rnd = new Random();
                    Rat RatLv4 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(35, 36)//hp
                                            ,rnd.Next(25, 36)//stam
                                            ,rnd.Next(25, 36)//atk
                                            ,rnd.Next(25, 36)//def
                                            ,rnd.Next(25, 36)//spd
                                        );
                    //Console.WriteLine(RatLv3.ToString());
                    return RatLv4;
            }
            return givePlayerDefaultRat();
        }

        // Creates a single CAT object with randomized stats
        Rat catCreator(int level) {
            Random rnd = new Random();
            int adjRndNum = rnd.Next(ratAdjNameList.Length);//grabs rnd adj from list
            int nounRndNum = rnd.Next(ratNounNameList.Length);//grabs rnd noun from list
            string newName = ratAdjNameList[adjRndNum]+" "+ratNounNameList[nounRndNum];//Combines adj & noun
            switch (level) {
                case 1:
                    // Random rnd = new Random();
                    Rat CatLv1 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(5, 11)//hp
                                            ,rnd.Next(1, 11)//stam
                                            ,rnd.Next(1, 11) + 2//atk
                                            ,rnd.Next(1, 11)//def
                                            ,rnd.Next(1, 11)//spd
                                        );
                    Console.WriteLine("**************\nLv1 Rat created\n*******************");
                    return CatLv1;
                case 2:
                    // Random rnd = new Random();
                    Rat CatLv2 = new Rat(  newName//Name :P
                                            ,level
                                            ,rnd.Next(15, 21)//hp
                                            ,rnd.Next(10, 21)//stam
                                            ,rnd.Next(10, 21) + 3//atk
                                            ,rnd.Next(10, 21)//def
                                            ,rnd.Next(10, 21)//spd
                                        );
                    //Console.WriteLine(RatLv2.ToString());
                    return CatLv2;
                case 3:
                    //Random rnd = new Random();
                    Rat CatLv3 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(25, 31)//hp
                                            ,rnd.Next(15, 31)//stam
                                            ,rnd.Next(15, 31) + 4//atk
                                            ,rnd.Next(15, 31)//def
                                            ,rnd.Next(15, 31)//spd
                                        );
                    //Console.WriteLine(RatLv3.ToString());
                    return CatLv3;
                case 4:
                    //Random rnd = new Random();
                    Rat CatLv4 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(35, 36)//hp
                                            ,rnd.Next(25, 36)//stam
                                            ,rnd.Next(25, 36) + 5//atk
                                            ,rnd.Next(25, 36)//def
                                            ,rnd.Next(25, 36)//spd
                                        );
                    //Console.WriteLine(RatLv3.ToString());
                    return CatLv4;
            }
            return givePlayerDefaultRat();
        }

        // Creates a single BIRD object with randomized stats
        Rat birdCreator(int level) {
            Random rnd = new Random();
            int adjRndNum = rnd.Next(ratAdjNameList.Length);//grabs rnd adj from list
            int nounRndNum = rnd.Next(ratNounNameList.Length);//grabs rnd noun from list
            string newName = ratAdjNameList[adjRndNum]+" "+ratNounNameList[nounRndNum];//Combines adj & noun
            switch (level) {
                case 1:
                    // Random rnd = new Random();
                    Rat BirdLv1 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(5, 11)//hp
                                            ,rnd.Next(1, 11)//stam
                                            ,rnd.Next(1, 11)//atk
                                            ,rnd.Next(1, 11)//def
                                            ,rnd.Next(1, 11) + 2//spd
                                        );
                    //Console.WriteLine(RatLv1.ToString());
                    return BirdLv1;
                case 2:
                    // Random rnd = new Random();
                    Rat BirdLv2 = new Rat(  newName//Name :P
                                            ,level
                                            ,rnd.Next(15, 21)//hp
                                            ,rnd.Next(10, 21)//stam
                                            ,rnd.Next(10, 21)//atk
                                            ,rnd.Next(10, 21)//def
                                            ,rnd.Next(10, 21) + 3//spd
                                        );
                    //Console.WriteLine(RatLv2.ToString());
                    return BirdLv2;
                case 3:
                    //Random rnd = new Random();
                    Rat BirdLv3 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(25, 31)//hp
                                            ,rnd.Next(15, 31)//stam
                                            ,rnd.Next(15, 31)//atk
                                            ,rnd.Next(15, 31)//def
                                            ,rnd.Next(15, 31) + 4//spd
                                        );
                    //Console.WriteLine(RatLv3.ToString());
                    return BirdLv3;
                case 4:
                    //Random rnd = new Random();
                    Rat RatLv4 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(35, 36)//hp
                                            ,rnd.Next(25, 36)//stam
                                            ,rnd.Next(25, 36)//atk
                                            ,rnd.Next(25, 36)//def
                                            ,rnd.Next(25, 36) + 5//spd
                                        );
                    //Console.WriteLine(RatLv3.ToString());
                    return RatLv4;
            }
            return givePlayerDefaultRat();
        }

        // Creates a single rat object with randomized stats
        Rat dogCreator(int level) {
            Random rnd = new Random();
            int adjRndNum = rnd.Next(ratAdjNameList.Length);//grabs rnd adj from list
            int nounRndNum = rnd.Next(ratNounNameList.Length);//grabs rnd noun from list
            string newName = ratAdjNameList[adjRndNum]+" "+ratNounNameList[nounRndNum];//Combines adj & noun
            switch (level) {
                case 1:
                    // Random rnd = new Random();
                    Rat DogLv1 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(5, 11)//hp
                                            ,rnd.Next(1, 11)//stam
                                            ,rnd.Next(1, 11)//atk
                                            ,rnd.Next(1, 11) + 2//def
                                            ,rnd.Next(1, 11)//spd
                                        );
                    //Console.WriteLine(RatLv1.ToString());
                    return DogLv1;
                case 2:
                    // Random rnd = new Random();
                    Rat DogLv2 = new Rat(  newName//Name :P
                                            ,level
                                            ,rnd.Next(15, 21)//hp
                                            ,rnd.Next(10, 21)//stam
                                            ,rnd.Next(10, 21)//atk
                                            ,rnd.Next(10, 21) + 3//def
                                            ,rnd.Next(10, 21)//spd
                                        );
                    //Console.WriteLine(RatLv2.ToString());
                    return DogLv2;
                case 3:
                    //Random rnd = new Random();
                    Rat DogLv3 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(25, 31)//hp
                                            ,rnd.Next(15, 31)//stam
                                            ,rnd.Next(15, 31)//atk
                                            ,rnd.Next(15, 31) + 4//def
                                            ,rnd.Next(15, 31)//spd
                                        );
                    //Console.WriteLine(RatLv3.ToString());
                    return DogLv3;
                case 4:
                    //Random rnd = new Random();
                    Rat DogLv4 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(35, 36)//hp
                                            ,rnd.Next(25, 36)//stam
                                            ,rnd.Next(25, 36)//atk
                                            ,rnd.Next(25, 36) + 5//def
                                            ,rnd.Next(25, 36)//spd
                                        );
                    //Console.WriteLine(RatLv3.ToString());
                    return DogLv4;
            }
            return givePlayerDefaultRat();
        }


        // Gives Player half decent rat to start out w/
        Rat givePlayerDefaultRat (){
            Rat RatBase = new Rat(  "Rattus Norvegicus"//Name
                                    ,1
                                    ,8//hp
                                    ,10//stam
                                    ,5//atk
                                    ,5//def
                                    ,7//spd
                                    );
            return RatBase;
        }

        // // Creates Territorys
        // Territory Byway         = new Territory(1, "The Byway"         , 75.00, 25.00, 00.00, 00.00);
        // Territory Thoroughfare  = new Territory(2, "The Thoroughfare"  , 25.00, 75.00, 00.00, 00.00);
        // Territory Highroad      = new Territory(2, "The Highroad"      , 25.00, 00.00, 75.00, 00.00);
        // Territory Swamp         = new Territory(2, "The Swamp"         , 25.00, 00.00, 00.00, 75.00);
        // Territory NoMansLand    = new Territory(3, "No Man's Land"     , 10.00, 45.00, 45.00, 00.00);
        // Territory Cage          = new Territory(3, "The Cage"          , 00.00, 00.00, 50.00, 50.00);
        // Territory HellsKitchen  = new Territory(3, "Hell's Kitchen"    , 00.00, 50.00, 00.00, 50.00);
        // Territory Backalley     = new Territory(4, "The Backalley"     , 00.00, 100.00, 00.00, 00.00);
        // Territory Aviary        = new Territory(4, "The Aviary"        , 00.00, 00.00, 100.00, 00.00);
        // Territory Pound         = new Territory(4, "The Pound"         , 00.00, 00.00, 00.00, 100.00);

        // Creates factions
        // Faction DogCop = new Faction("The Barky", "Police", 50.00, [Swamp, HellsKitchen, Cage, Pound]);
        // Faction TheivesCat = new Faction("Theive's Cat", "Gang", 50.00, [Thoroughfare, HellsKitchen, NoMansLand, Backalley]);
        // Faction BirdMafia = new Faction("Bird Mafia", "Gang", 50.00, [Highroad, Cage, NoMansLand, Aviary]);
        


        mainPlayer.addToPlayerRatRoster(givePlayerDefaultRat()); // :D
        MainMenu();
        Console.WriteLine("Remember, second mouse gets the cheese. Till next time...\n");
        Thread.Sleep(2000); 
        Console.Clear();
    }
}
