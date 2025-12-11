using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System.Data.SqlTypes;

/*



*/





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
        string[] catNounNameList = {"Cat", "Furball", "Felis", "Catus", "Kitten", "Kitty", "Kat", "Sylvester", "Tom", "Putty-Tat", "Jiji", "Meowth", "Catbus", "Binx", "Scar", "Katze"};
        string[] dogNounNameList = {"Dog", "Flee Bag", "Canine", "Lupus", "Doggy", "Bark", "Hound", "Hund", "Yapper", "Lassie", "Bolt", "Scooby", "Beethoven", "Petey", "Zero", "Indiana", "Toto", "Lady", "Snoopy", "Pluto", "Dug", "Slinky"};
        string[] birdNounNameList = {"Bird", "Aves", "Daffy", "Donald", "Archimedes", "Woodstock", "Tweety", "Kevin", "McDuck", "Huey", "Dewey", "Louie", "Foghorn", "Blu", "Mordecai", "Darkwing", "Pidgey", "Pidgeotto", "Pidgeot", "Howard"};


        // Too complex :(
        // string[,] MAP = {   {"# |            | #"}, {"# | The   | #"},
        //                     {"# | The Aviary | #"}, {"# |__Cage_| #"},
        //                     {"# |____________| #"}, {"#############"},
        //                     {"##################"}, {"# "},
        //                     {"# |      |  # # #"},{"# | The   | #"},
        //                     {"# | No   |  # # #"},{"# | High- | #"},
        //                     {"# | Mans |____# #"},{"# | road  | #"},
        //                     {"# | Land      | #"},{"# "},{"# "},
        //                     {"# |___________| #"},{"# "},
        //                     {"#################"},
        //                     {"# |           | # # #"},
        //                     {"# |           | # # #"},{"# |     "},
        //                     {"# | Backalley |___# #"},{"# |     "},
        //                     {"# |_______________| #"},{"# |_____"}}; 

        string[,] simpleMap = 
        { 
            {" ______________ "},   {" ______________ "},   {" ______________ "},
            {"| Thoroughfare |"},   {"|     Byway    |"},   {"|   Highroad   |"},
            {"|______________|"},   {"|______________|"},   {"|______________|"},

            {" ______________ "},   {" ______________ "},   {" ______________ "},
            {"|   Backalley  |"},   {"| No Mans Land |"},   {"|    Aviary    |"},
            {"|______________|"},   {"|______________|"},   {"|______________|"},

            {" ______________ "},   {" ______________ "},   {" ______________ "}, 
            {"| HellsKitchen |"},   {"|  The Pound   |"},   {"|     Cage     |"},
            {"|______________|"},   {"|______________|"},   {"|______________|"}
        };

        string[,] simpleMapIndexed = 
        { 
            {"0 ______[1]_____ "},    {"1 ______[2]_____ "},    {"2 ______[3]_____ "},
            {"3| Thoroughfare |"},    {"4|     Byway    |"},    {"5|   Highroad   |"},
            {"6|______________|"},    {"7|______________|"},    {"8|______________|"},

            {"9 ______[4]_____ " },   {"10 ______[5]_____ "},   {"11 ______[6]_____ "},
            {"12|   Backalley  |"},   {"13| No Mans Land |"},   {"14|    Aviary    |"},
            {"15|______________|"},   {"16|______________|"},   {"17|______________|"},

            {"18 ______[7]_____ "},   {"19 ______[8]_____ "},   {"20 ______[9]_____ "}, 
            {"21| HellsKitchen |"},   {"22|  The Pound   |"},   {"23|     Cage     |"},
            {"24|______________|"},   {"25|______________|"},   {"26|______________|"}
        };

        //Main

        // Creates Territorys
        Territory Byway         = new Territory(1, "The Byway"         , 2, 75.00, 25.00, 00.00, 00.00);
        Territory Thoroughfare  = new Territory(2, "The Thoroughfare"  , 1, 25.00, 75.00, 00.00, 00.00);
        Territory Highroad      = new Territory(2, "The Highroad"      , 3, 25.00, 00.00, 75.00, 00.00);
        Territory Swamp         = new Territory(2, "The Swamp"         , 9999, 25.00, 00.00, 00.00, 75.00); //Cut Content, would need new map
        Territory NoMansLand    = new Territory(3, "No Man's Land"     , 5, 10.00, 45.00, 45.00, 00.00);
        Territory Cage          = new Territory(3, "The Cage"          , 9, 00.00, 00.00, 50.00, 50.00);
        Territory HellsKitchen  = new Territory(3, "Hell's Kitchen"    , 7, 00.00, 50.00, 00.00, 50.00);
        Territory Backalley     = new Territory(4, "The Backalley"     , 4, 00.00, 100.00, 00.00, 00.00);
        Territory Aviary        = new Territory(4, "The Aviary"        , 6, 00.00, 00.00, 100.00, 00.00);
        Territory Pound         = new Territory(4, "The Pound"         , 8, 00.00, 00.00, 00.00, 100.00);
        List<Territory> allTerrys = new List<Territory> {Byway, Thoroughfare, Highroad, Swamp, NoMansLand, Cage, HellsKitchen, Backalley, Aviary, Pound};

        Player mainPlayer = new Player();
        CombatCPU Catsby = new CombatCPU("Theives' Cat", "gang", 50.00,"ThievesCat", [Byway, Thoroughfare, HellsKitchen, NoMansLand, Backalley]); 
        CombatCPU Vincenzo = new CombatCPU("Bird Mafia", "gang", 50.00, "BirdMafia", [Highroad, Cage, NoMansLand, Aviary]);
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

        void speechDisplay (string dialog) {
            for (int i =0; i < dialog.Length; i++) {
                Console.Write(dialog[i]);
                Thread.Sleep(0025); 
            }
            Console.WriteLine("\n");
        }

        void MainMenu () {
            Console.ForegroundColor = ConsoleColor.White;
            speechDisplay("\nWelcome to ...Derrr RrrATHaus...");
            int selector;
            Thread.Sleep(300); 
            while (true) {
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(500); 
                Console.WriteLine("*** Main Menu *** ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(mainPlayer.getBasicPlayerInfo());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[1] - Shop\n[2] - Territory Control\n[3] - Your Inventory\n[4] - Save/Load Data\n[0] - Close Program\nEnter: ");
                selector = numCheck(Console.ReadLine());
                if (selector == 1) {
                    ShopMenu();
                } else if (selector == 2) {
                    mainPlayer.money = mainPlayer.money + TerritoryMenu();
                } else if (selector == 3) {
                    InventoryMenu();
                } else if (selector == 4) {
                    SaveLoadMenu();
                } else if (selector == 0) {
                    return;
                } 
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////
        ///                         MAP
        ///////////////////////////////////////////////////////////////////////////////////////

        void mapMenu () 
        {
            for (int i = 0; i < 27; i++) 
            {
                if (i%3 == 0) {
                    Console.WriteLine();
                } 
                Console.Write(simpleMap[i, 0]);
            }
            Console.WriteLine();
        }
        
        void mapAreaDisplay (int areaCode)
        {
            if  (areaCode < 4) {
                for (int i = 0; i<3; i++){
                        Console.WriteLine(simpleMap[ (areaCode-1) + 3*i, 0 ]);
                }
            } else if (areaCode < 7) {
                for (int i = 0; i<3; i++) {
                   Console.WriteLine(simpleMap[ (areaCode+5) + 3*i, 0 ]);
                }
            } else if (areaCode < 10) {
                for (int i = 0; i<3; i++) {
                    Console.WriteLine(simpleMap[ (areaCode+11) + 3*i, 0 ]);
                }
            } else {
                Console.WriteLine ("If you're seeing this I fucked up");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        void mapAreaControlDisplay (int areaCode, Territory terry)
        {
            terry.returnHighestFactionControl();
            switch (terry.highestControlPrecent) {
                case "Player":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "ThievesCat":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "BirdMafia":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "TheFuzz":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
            }            
            mapAreaDisplay(areaCode);


        }

        //////////////////////////////////////////////////////////////////////////////////////
        ///                         SAVE/LOAD MENU
        //////////////////////////////////////////////////////////////////////////////////////

        void SaveLoadMenu () {
            int selector;
            while (true) {
                Console.ForegroundColor = ConsoleColor.White;
                speechDisplay("*** Save/Load Menu *** ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(mainPlayer.getBasicPlayerInfo());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[1] - Save Game\n[2] - Load Game\n[0] - Return\nEnter: ");
                selector = numCheck(Console.ReadLine());
                
                if (selector == 1) {
                    DataManager.SavePlayer("PlayerData.json", mainPlayer);
                    DataManager.SaveTerrys("TerritoryData.json", allTerrys);
                    //foreach (Territory terry in allTerrys){ DataManager.SaveTerry("TerritoryData.json", terry); } 
                    Console.WriteLine("Game Saved!\n");
                } else if (selector == 2) {
                    mainPlayer = DataManager.LoadPlayer("PlayerData.json");
                    loadTerryData(DataManager.LoadTerrys("TerritoryData.json"));
                    Console.WriteLine("Game Loaded!\n");
                } else if (selector == 0) {
                    return;
                } 
            }
        }

        // // Loops through TerryData.json and overwrites allTerrys data
        // void loadTerryData(List<Territory> newTerrysData)
        // {
        //     for(int i=0;i<allTerrys.Count;i++)
        //     {
        //         allTerrys[i] = newTerrysData[i];
        //     }
        // }
        void loadTerryData(List<Territory> newTerrysData)
        {
            for(int i=0;i<allTerrys.Count;i++)
            {
                allTerrys[i].level = newTerrysData[i].level;
                allTerrys[i].name = newTerrysData[i].name;
                allTerrys[i].areaCode = newTerrysData[i].areaCode;
                allTerrys[i].playerControlPrecent = newTerrysData[i].playerControlPrecent;
                allTerrys[i].catControl = newTerrysData[i].catControl;
                allTerrys[i].birdControl = newTerrysData[i].birdControl;
                allTerrys[i].dogControl = newTerrysData[i].dogControl;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////
        ///                         SHOP
        //////////////////////////////////////////////////////////////////////////////////////
        
        // CAN be better
        // Creates Rats then display in text
        List<Rat> ShopDisplay (int numOfRatsToCreate, int RatLv) {
            int costOfRat;
            List<Rat> shopRatList = new List<Rat>();
            
            switch (RatLv) {
                case 1:
                    costOfRat = 100;
                    //Console.WriteLine("Buy Statis: " + (mainPlayer.money > costOfRat));
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + RatLv + " rat\n");
                        canBuy = false;
                        return shopRatList;
                    } canBuy = true;
                    break;
                case 2:
                    costOfRat = 500;
                    //Console.WriteLine("Buy Statis: " + (mainPlayer.money < costOfRat));
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + RatLv + " rat\n");
                        canBuy = false;
                        return shopRatList;
                    } canBuy = true;
                    break;
                case 3: 
                    costOfRat = 1000;
                    //Console.WriteLine("Buy Statis: " + (mainPlayer.money < costOfRat));
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + RatLv + " rat\n");
                        canBuy = false;
                        return shopRatList;
                    } canBuy = true;
                    break;
                case 4:
                    costOfRat = 1500;
                    Console.WriteLine("Buy Statis: " + (mainPlayer.money < costOfRat));
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + RatLv + " rat\n");
                        canBuy = false;
                        return shopRatList;
                    } canBuy = true;
                    break;
            }
            
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
        void ShopBuy (int selector, List<Rat> shopRatList, int LvToCost) {
            int costOfRat = 1000;
            while (true){
                switch (LvToCost) {
                    case 1:
                        costOfRat = 100;
                        break;
                    case 2:
                        costOfRat = 500;
                        break;
                    case 3:
                        costOfRat = 1000;
                        break;
                    case 4:
                        costOfRat = 2000;
                        break;
                }

                if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + LvToCost + " rat\n");
                        canBuy = false;
                        return;
                    }
                    break;

            }
            while (canBuy) {    
                if (selector == 0) {
                    Console.WriteLine("Returning\n");
                    break;
                } else if (selector > 0 && selector < 7) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[selector - 1]);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Buying {shopRatList[selector - 1]}");
                    Console.ResetColor();
                    mainPlayer.money -= costOfRat;
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
            } else if (rat.level == 4){
                return 1000;
            } else {
                return 0;
            }
        }

        // Start of Shop Menu
        void ShopMenu (){
            canBuy = true;
            int input;
            while (true) { 
                
                // Text formatting
                Thread.Sleep(500); 
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n");
                speechDisplay("***** What'a looking for today Boss *****");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(mainPlayer.getBasicPlayerInfo());
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[1] Buy Rat\n[2] Sell Rat\n[0] Return\nEnter: ");
                input = numCheck(Console.ReadLine());
                if (input == 1) { // To Buy Rat
                    Console.Write("Lvl of Rats: ");
                    int LvOfRats = numCheck(Console.ReadLine());
                    List<Rat> shopRatList = new List<Rat>();
                    shopRatList = ShopDisplay(4, LvOfRats); // Creates new rats 
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
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////
        ///                         INVENTORY
        ///////////////////////////////////////////////////////////////////////////////////////

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

        //////////////////////////////////////////////////////////////////////////////////////
        ///                         COMBAT/COMBAT MENU
        ///////////////////////////////////////////////////////////////////////////////////////


        int TerritoryMenu (){
            Console.WriteLine("\n");
            mapMenu();
            speechDisplay("Who's on the choppin' block today:");
            Console.WriteLine("[1] - Cats\n[2] - Birds\n[3] - Dogs\n[0] - Return\nEnter: ");
            int selector = numCheck(Console.ReadLine());
            switch (selector) {
                case 1:
                    Console.WriteLine("**Cats**");
                    return CombatMenu(Catsby);
                case 2:
                    Console.WriteLine("**Birds**");
                    return CombatMenu(Vincenzo);
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
            Console.Write("[0] - Return\nEnter: ");
            int selector = numCheck(Console.ReadLine());

            while (true) {  
                if (selector == 0) {
                    Console.WriteLine("Returning..."); // Could add effect
                    return 0;
                } else if (selector > 0 && selector <= opponent.getTerritoryList().Count){
                    // Edits here for terry. control display
                    mapAreaDisplay(opponent.getTerritoryList()[selector - 1].areaCode);
                    opponent.getTerritoryList()[selector - 1].displayControlPercents(); 
                    return ChallengeSetupCombatMenu(opponent.getTerritoryList()[selector - 1].level, opponent, opponent.getTerritoryList()[selector - 1]);
                } else {
                    Console.WriteLine("Please input valid number: ");
                    selector = numCheck(Console.ReadLine());
                }
            }
        }

        // Can be optimized
        int ChallengeSetupCombatMenu (int LvSet, CombatCPU opponent, Territory terry){
            Console.WriteLine("\nWhat Challenge do you want to face\n[1] - 1 Enemy\n[2] - 3 Enemies\n[3] - 5 Enemies\n[0] - Return\nEnter: ");
            int selector = numCheck(Console.ReadLine());
            mainPlayer.clearActivePlayerRatRoster();
            while (true) {
                

                // case 1
                if (selector == 1) { //Player should always have at least one rat
                    cpuRatRosterSetup(1, LvSet, opponent);
                    playerRatRosterSetup(1);
                    return ACTUALFUCKINGRATFIGHT(mainPlayer.activePlayerRatRoster, opponent.getCPURatRoster(), 1, LvSet, opponent, terry);
                // case 2
                } else if (selector == 2) {
                    if (mainPlayer.getRatRosterCount() >= 3) {
                        cpuRatRosterSetup(3, LvSet, opponent);
                        playerRatRosterSetup(3);
                        return ACTUALFUCKINGRATFIGHT(mainPlayer.activePlayerRatRoster, opponent.getCPURatRoster(), 3, LvSet, opponent, terry);
                    } else {
                        Console.WriteLine("You do not have enough rats");
                        return 0;
                    }
                // case 3
                } else if (selector == 3) {
                    if (mainPlayer.getRatRosterCount() >= 5) {
                        cpuRatRosterSetup(5, LvSet, opponent);
                        playerRatRosterSetup(5);
                        return ACTUALFUCKINGRATFIGHT(mainPlayer.activePlayerRatRoster, opponent.getCPURatRoster(), 5, LvSet, opponent, terry);
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

                // failsafe for failsafe, otherwise editor yells at me
                Console.WriteLine("Returning...\n");
                return 0;
            }
        }

        // sets up cpu roster based on who's who
        // This could be done way better
        void cpuRatRosterSetup (int numOfRatsToCreate, int Lv, CombatCPU opponent) 
        {
            Console.WriteLine("Setting up CPU Cats");
            opponent.clearCPURatRoster();
            switch (opponent.allegiance) {
                case "ThievesCat":
                    for (int i = 0; i < numOfRatsToCreate; i++) {
                        Catsby.addToCPURatRoster( catCreator(Lv) );
                    } break;
                case "BirdMafia":
                    for (int i = 0; i < numOfRatsToCreate; i++) {
                        Vincenzo.addToCPURatRoster( birdCreator(Lv) );
                    } break;
                case "TheFuzz":
                    for (int i = 0; i < numOfRatsToCreate; i++) {
                        Theodog.addToCPURatRoster( dogCreator(Lv) );
                    } break;
            }
        }
        
        void playerRatRosterSetup(int numOfRatsAllowed)
        {
            Console.WriteLine($"{numOfRatsAllowed} Rat(s) are allowed, and you get to choose who fights and the order.");
            Console.WriteLine("\nDisplaying your Rats\n");
            mainPlayer.InfoDumpOfRatRoster();

            for (int i = 0; i < numOfRatsAllowed; i++)
            {
                // Checks if active roster is full
                if (mainPlayer.getActiveRatRosterCount() >= 5)
                {
                    Console.WriteLine("Active Roster Full");
                    break;
                }

                Console.WriteLine("Choose Rat: ");
                int selector = numCheck(Console.ReadLine());

                // Checks if inputted index is valid, if not "restarts iteration of loop"
                if (selector < 1 || selector > mainPlayer.getPlayerRatRoster().Count)
                {
                    Console.WriteLine("Invalid selection. Try again.");
                    i--;  // Retry this iteration
                    continue;
                }

                var selectedRat = mainPlayer.getPlayerRat(selector - 1); // Logs inputted index to a variable for convinetence

                // Check for duplicates
                if (mainPlayer.getActivePlayerRatRoster().Contains(selectedRat))
                {
                    Console.WriteLine("Rat already in active roster");
                    i--;
                    continue;
                }

                // Add rat to active roster
                Console.WriteLine($"Adding {selectedRat.name}");
                mainPlayer.addToActivePlayerRatRoster(selectedRat);

                for (int j = 0; j < mainPlayer.getActiveRatRosterCount() - 1; j++)
                {
                    Console.WriteLine(mainPlayer.getActivePlayerRat(j).name);
                }
                Console.WriteLine($"Current Active Roster:"); // you know
                mainPlayer.NameDumpOfActiceRatRoster();
                Console.WriteLine("Conitueing");
            }
        }

        int ACTUALFUCKINGRATFIGHT (List<Rat> playersRats, List<Rat> enemysRats, int numOfFighters, int fightLv, CombatCPU opponent, Territory terry)
        {
            Console.WriteLine("FUCKING FIGHTING");
            if (RatFightHandler(numOfFighters, opponent)) {
                Console.WriteLine("Player Wins");
                if (fightLv == 1) {
                    terry.terryControlChange(opponent, .5*numOfFighters, true);
                    return 40*numOfFighters;
                } else if (fightLv == 2) {
                    terry.terryControlChange(opponent, .5*numOfFighters, true);
                    return 200*numOfFighters;
                } else if (fightLv == 3) {
                    terry.terryControlChange(opponent, .5*numOfFighters, true);
                    return 400*numOfFighters;
                } else if (fightLv == 4) {
                    terry.terryControlChange(opponent, .5*numOfFighters, true);
                    return 800*numOfFighters;  
                } else {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("If you are seeing this I did something wrong :P");
                    Console.ResetColor();
                    terry.terryControlChange(opponent, (fightLv*0.5*numOfFighters), false);
                    return 0*numOfFighters;
                }
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CPU Wins");
                Console.ResetColor();
                terry.terryControlChange(opponent, .5*numOfFighters, false);
                return 0;
            }
        }

        // Atk/(2^(Atk/Def))
        bool RatFightHandler(int numOfFighters, CombatCPU opponent) 
        {
            int wins = 1;
            int loses = 1;
            for (int i = 0; i < numOfFighters; i++) 
            {
                Console.WriteLine($"\n*******************************************\nRound {i+1}, Fight!");
                if (RatFight(i, opponent, mainPlayer.activePlayerRatRoster)) {
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

        bool RatFight (int i, CombatCPU opponent, List<Rat> playerRat) 
        {
            Random rnd = new Random();
            // player rat vars
            string PlayerName = playerRat[i].name;
            float PlayerAtk = playerRat[i].atk;
            float PlayerDef = playerRat[i].def;
            float PlayerSpd = playerRat[i].spd;
            float PlayerHp = playerRat[i].hp;
            bool playerTurn = true;

            // CPU rat vars
            string CPUName = opponent.getCPURat(i).name;
            float CPUAtk = opponent.getCPURat(i).atk;
            float CPUDef = opponent.getCPURat(i).def;
            float CPUSpd = opponent.getCPURat(i).spd;
            float CPUHp = opponent.getCPURat(i).hp;

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
            while (true) 
            {
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
                    return RatLv1;
                case 2:
                    Rat RatLv2 = new Rat(  newName//Name :P
                                            ,level
                                            ,rnd.Next(15, 21)//hp
                                            ,rnd.Next(10, 21)//stam
                                            ,rnd.Next(10, 21)//atk
                                            ,rnd.Next(10, 21)//def
                                            ,rnd.Next(10, 21)//spd
                                        );
                    return RatLv2;
                case 3:
                    Rat RatLv3 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(25, 31)//hp
                                            ,rnd.Next(15, 31)//stam
                                            ,rnd.Next(15, 31)//atk
                                            ,rnd.Next(15, 31)//def
                                            ,rnd.Next(15, 31)//spd
                                        );
                    return RatLv3;
                case 4:
                    Rat RatLv4 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(35, 36)//hp
                                            ,rnd.Next(25, 36)//stam
                                            ,rnd.Next(25, 36)//atk
                                            ,rnd.Next(25, 36)//def
                                            ,rnd.Next(25, 36)//spd
                                        );
                    return RatLv4;
            }
            return givePlayerDefaultRat();
        }

        // Creates a single Rat object with randomized stats w/ boosted Atk
        Rat catCreator(int level) {
            Random rnd = new Random();
            string newName = ratAdjNameList[ rnd.Next(ratAdjNameList.Length) ] +
                            " " + 
                            catNounNameList[ rnd.Next(catNounNameList.Length) ];//Combines adj & noun
                            
            switch (level) 
            {
                case 1:
                    Rat CatLv1 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(5, 11)//hp
                                            ,rnd.Next(1, 11)//stam
                                            ,rnd.Next(1, 11) + 2//atk
                                            ,rnd.Next(1, 11)//def
                                            ,rnd.Next(1, 11)//spd
                                        );
                    return CatLv1;
                case 2:
                    Rat CatLv2 = new Rat(  newName//Name :P
                                            ,level
                                            ,rnd.Next(15, 21)//hp
                                            ,rnd.Next(10, 21)//stam
                                            ,rnd.Next(10, 21) + 3//atk
                                            ,rnd.Next(10, 21)//def
                                            ,rnd.Next(10, 21)//spd
                                        );
                    return CatLv2;
                case 3:
                    Rat CatLv3 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(25, 31)//hp
                                            ,rnd.Next(15, 31)//stam
                                            ,rnd.Next(15, 31) + 4//atk
                                            ,rnd.Next(15, 31)//def
                                            ,rnd.Next(15, 31)//spd
                                        );
                    return CatLv3;
                case 4:
                    Rat CatLv4 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(35, 36)//hp
                                            ,rnd.Next(25, 36)//stam
                                            ,rnd.Next(25, 36) + 5//atk
                                            ,rnd.Next(25, 36)//def
                                            ,rnd.Next(25, 36)//spd
                                        );
                    return CatLv4;
            }
            return givePlayerDefaultRat();
        }

        // Creates a single Rat object with randomized stats w/ boosted Spd
        Rat birdCreator(int level) {
            Random rnd = new Random();
            // int adjRndNum = rnd.Next(ratAdjNameList.Length);//grabs rnd adj from list
            // int nounRndNum = rnd.Next(ratNounNameList.Length);//grabs rnd noun from list
            string newName = ratAdjNameList[ rnd.Next(ratAdjNameList.Length) ] +
                            " " + 
                            birdNounNameList[ rnd.Next(birdNounNameList.Length) ];//Combines adj & noun
            switch (level) {
                case 1:
                    Rat BirdLv1 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(5, 11)//hp
                                            ,rnd.Next(1, 11)//stam
                                            ,rnd.Next(1, 11)//atk
                                            ,rnd.Next(1, 11)//def
                                            ,rnd.Next(1, 11) + 2//spd
                                        );
                    return BirdLv1;
                case 2:
                    Rat BirdLv2 = new Rat(  newName//Name :P
                                            ,level
                                            ,rnd.Next(15, 21)//hp
                                            ,rnd.Next(10, 21)//stam
                                            ,rnd.Next(10, 21)//atk
                                            ,rnd.Next(10, 21)//def
                                            ,rnd.Next(10, 21) + 3//spd
                                        );
                    return BirdLv2;
                case 3:
                    Rat BirdLv3 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(25, 31)//hp
                                            ,rnd.Next(15, 31)//stam
                                            ,rnd.Next(15, 31)//atk
                                            ,rnd.Next(15, 31)//def
                                            ,rnd.Next(15, 31) + 4//spd
                                        );
                    return BirdLv3;
                case 4:
                    Rat RatLv4 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(35, 36)//hp
                                            ,rnd.Next(25, 36)//stam
                                            ,rnd.Next(25, 36)//atk
                                            ,rnd.Next(25, 36)//def
                                            ,rnd.Next(25, 36) + 5//spd
                                        );
                    return RatLv4;
            }
            return givePlayerDefaultRat();
        }

        // Creates a single rat object with randomized stats w/ boosted Def
        Rat dogCreator(int level) {
            Random rnd = new Random();
            // int adjRndNum = rnd.Next(ratAdjNameList.Length);//grabs rnd adj from list
            // int nounRndNum = rnd.Next(ratNounNameList.Length);//grabs rnd noun from list
            string newName = ratAdjNameList[ rnd.Next(ratAdjNameList.Length) ] +
                            " " + 
                            dogNounNameList[ rnd.Next(dogNounNameList.Length) ];//Combines adj & noun
            switch (level) {
                case 1:
                    Rat DogLv1 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(5, 11)//hp
                                            ,rnd.Next(1, 11)//stam
                                            ,rnd.Next(1, 11)//atk
                                            ,rnd.Next(1, 11) + 2//def
                                            ,rnd.Next(1, 11)//spd
                                        );
                    return DogLv1;
                case 2:
                    Rat DogLv2 = new Rat(  newName//Name :P
                                            ,level
                                            ,rnd.Next(15, 21)//hp
                                            ,rnd.Next(10, 21)//stam
                                            ,rnd.Next(10, 21)//atk
                                            ,rnd.Next(10, 21) + 3//def
                                            ,rnd.Next(10, 21)//spd
                                        );
                    return DogLv2;
                case 3:
                    Rat DogLv3 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(25, 31)//hp
                                            ,rnd.Next(15, 31)//stam
                                            ,rnd.Next(15, 31)//atk
                                            ,rnd.Next(15, 31) + 4//def
                                            ,rnd.Next(15, 31)//spd
                                        );
                    return DogLv3;
                case 4:
                    Rat DogLv4 = new Rat(   newName//Name :P
                                            ,level
                                            ,rnd.Next(35, 36)//hp
                                            ,rnd.Next(25, 36)//stam
                                            ,rnd.Next(25, 36)//atk
                                            ,rnd.Next(25, 36) + 5//def
                                            ,rnd.Next(25, 36)//spd
                                        );
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


        mainPlayer.addToPlayerRatRoster(givePlayerDefaultRat()); // :D
        //mapMenu();
        MainMenu();
        Console.WriteLine("Remember, second mouse gets the cheese. Till next time...\n");
        Thread.Sleep(2000); 
        Console.Clear();
    }
}