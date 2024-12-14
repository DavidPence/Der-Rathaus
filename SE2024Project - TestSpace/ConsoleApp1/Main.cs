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


        string[] ratAdjNameList = {"Cheesy", "Cheezy" , "The" , "Der"
                                    , "Slimey", "Terrible", "Hairy", "Nasty", "Awful", "Hideous", "Lousy", "Vuglar", "Rotten", "Detestable", "Stupid"
                                    , "Spiffy", "Clean", "Shining", "Fancy", "Dapper", "Dashing", "Chique", "Snazzy", "Neat", "Respectful"
                                    , "Muslce-Bound", "Strong", "Big", "Firm", "Powerful", "Brawny", "Mighty", "Stout", "Beefy", "Rugged", "Stalwart" 
                                    , "Zippy", "Blistering", "Quick", "Hasty", "Nippy", "Rapid", "Energetic", "Rushing"}; 

        string[] ratNounNameList = {"Rat", "Cheese", "Rattus", "Remy", "Maus", "Mouse", "Nezumi", "Mickey", "Minnie", "Suzy", "Jerry", "Mordax", "Mortimer", "Rizzo", "Stuart", "Fucker", "Pence", "Morse", "Dugan", "Pinky", "Brain", "Ratsby", "Ratsputin"};
        
        
        //Main

        Player mainPlayer = new Player();
        CombatCPU CPU = new CombatCPU();

        int numCheck (string num){
            while (true){
                try {
                    return Convert.ToInt32(num);
                } catch {
                    Console.WriteLine("Please input valid #: ");
                    num = Console.ReadLine();
                }
            }
        }

        void MainMenu () {
            Console.WriteLine("Welcome to ...Derrr RrrATHaus...");
            //Console.WriteLine("[1] - Shop\n[2] - Fight\n[3] - Your Inventory\n[0] - Close Program\n Enter: ");
            int selector /*= numCheck(Console.ReadLine())*/;
            while (true) {
                Console.WriteLine("[1] - Shop\n[2] - Fight\n[3] - Your Inventory\n[0] - Close Program\nEnter: ");
                selector = numCheck(Console.ReadLine());
                if (selector == 1) {
                    ShopMenu();
                } else if (selector == 2) {
                    mainPlayer.money = mainPlayer.money + CombatMenu();
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
            // Console.WriteLine("Welcome to the Rats R Us\n");
            List<object> shopRatList = new List<object>();
            for (int i = 0; i < numOfRatsToCreate+1; i++) {
                //Console.WriteLine((i+1) + " Rat created!");
                shopRatList.Add( ratCreator(RatLv) );
            }
            int count = 0;
            Console.WriteLine(""); //For Formatting
            foreach (object shopRat in shopRatList) {
                count++;
                Console.WriteLine("vvv Displaying Rat [" + count + "] vvv");
                Console.WriteLine(shopRat.ToString());
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
                        return;
                    }
                    break;
                } else if (LvToCost == 2) {
                    costOfRat = 500;
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + LvToCost + " rat\n");
                        return;
                    }
                    break;
                } else if (LvToCost == 3) {
                    costOfRat = 1000;
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + LvToCost + " rat\n");
                        return;
                    }
                    break;
                }
            }
            while (true) {    
                if (selector == 0) {
                    Console.WriteLine("Returning\n");
                    break;
                } else if (selector == 1) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[0]);
                    Console.WriteLine("Buying Rat 1");
                    mainPlayer.money = mainPlayer.money - costOfRat;
                    break;
                } else if (selector == 2) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[1]);
                    Console.WriteLine("Buying Rat 2");
                    mainPlayer.money = mainPlayer.money - costOfRat;
                    break;
                } else if (selector == 3) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[2]);
                    Console.WriteLine("Buying Rat 3");
                    mainPlayer.money = mainPlayer.money - costOfRat;
                    break;
                } else if (selector == 4) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[3]);
                    Console.WriteLine("Buying Rat 4");
                    mainPlayer.money = mainPlayer.money - costOfRat;
                    break;
                } else if (selector == 5) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[4]);
                    Console.WriteLine("Buying Rat 5");
                    mainPlayer.money = mainPlayer.money - costOfRat;
                    break;
                } else if (selector == 6) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[5]);
                    Console.WriteLine("Buying Rat 6");
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
            Console.WriteLine("Are you sure you want to sell " + mainPlayer.getPlayerRat(selector-1).ratName + " for " + ratSellPrice + "G Enter [Y/N]: ");
            comfirm = Console.ReadLine();
            if (comfirm == "Y") {
                mainPlayer.money = mainPlayer.money + ratSellPrice;
                Console.WriteLine("Selling " + mainPlayer.getPlayerRat(selector-1).ratName + "\n");
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

        void ShopMenu (){
            //Console.WriteLine("***** Welcome to the Rats R Us*****");
            //Console.Write("[1] Buy Rat\n[2] Sell Rat\n[3] Return\nEnter: ");
            int input /*= numCheck(Console.ReadLine())*/;
            while (true) { 
                Console.WriteLine("\n***** Welcome to the Rats R Us*****");
                Console.Write("[1] Buy Rat\n[2] Sell Rat\n[0] Return\nEnter: ");
                input = numCheck(Console.ReadLine());
                if (input == 1) {
                    Console.Write("Lvl of Rats: ");
                    int LvOfRats = numCheck(Console.ReadLine());
                    List<object> shopRatList = new List<object>();
                    shopRatList = ShopDisplay(4, LvOfRats);
                    Console.WriteLine("Enter [1-5] To buy respective Rat, [0] to close: ");
                    int input2 = numCheck(Console.ReadLine());
                    ShopBuy(input2, shopRatList, LvOfRats);

                } else if (input == 2) {
                    if ( mainPlayer.getRatRosterCount() > 1) {
                        mainPlayer.InfoDumpOfRatRoster();
                        Console.WriteLine("What Rat would you like to sell\nEnter: ");
                        ShopSell( numCheck(Console.ReadLine()) );
                    } else {
                        Console.WriteLine("Cannot sell any rats. You must have ONE at all times\n");
                    }

                } else if (input == 0) {
                    Console.WriteLine("Exitting");
                    break;

                } else {
                    Console.WriteLine("Please input valid #: ");
                    input = Convert.ToInt32(Console.ReadLine());
                }
                // Console.Write("[1] Buy Rat\n[2] Sell Rat\n[3] Return\nEnter: ");
                // input = Convert.ToInt32(Console.ReadLine());
            }
        }



        // ************* Inventory *************

        void InventoryMenu() {
            //Console.WriteLine("***** Your Inventory *****");
            //Console.WriteLine("[1] - Your Rat(s)\n[2] - Your Stats\n[0] - Return\nEnter: ");
            int selector /*= numCheck(Console.ReadLine())*/;
            while (true) {
                Console.WriteLine("\n***** Your Inventory *****");
                Console.WriteLine("[1] - Your Rat(s)\n[2] - Your Stats\n[0] - Return\nEnter: ");
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
                    // Console.WriteLine("[1] - Your Rat(s)\n[2] - Your Stats\n[0] - Return");
                    // selector = numCheck(Console.ReadLine());
            }
            Console.WriteLine("Returning...");
        }

        // ************* Combat/Combat Menu *************

        int CombatMenu (){
            Console.WriteLine("Entering the RatClurb...");
            Console.WriteLine("What Level of Rats do you want to fight\n[1] - Level 1\n[2] - Level 2\n[3] - Level 3\n[0] - Return\nEnter: ");
            int selector = numCheck(Console.ReadLine());
            while (true) {   
                if (selector == 1) {
                    return ChallengeSetupCombatMenu(1);
                } else if (selector == 2) {
                    return ChallengeSetupCombatMenu(2);
                } else if (selector == 3) {
                    return ChallengeSetupCombatMenu(3);
                } else if (selector == 0) {
                    Console.WriteLine("Returning...");
                    return 0;
                } else {
                    Console.WriteLine("Please input valid number: ");
                    selector = numCheck(Console.ReadLine());
                }
            }
        }

        int ChallengeSetupCombatMenu (int LvSet){
            Console.WriteLine("What Challenge do you want to face\n[1] - 1 Enemy Rat\n[2] - 3 Enemy Rats\n[3] - 5 Enemy Rats\n[0] - Return\n Enter: ");
            int selector = numCheck(Console.ReadLine());
            mainPlayer.clearActivePlayerRatRoster();
            while (true) {
                if (selector == 1) { //Player should always have at least one rat
                    cpuRatRosterSetup(1, LvSet);
                    playerRatRosterSetup(1);
                    return ACTUALFUCKINGRATFIGHT(mainPlayer.getActivePlayerRatRoster(), CPU.getCPURatRoster(), 1, LvSet);
                } else if (selector == 2) {
                    if (mainPlayer.getRatRosterCount() >= 3) {
                        cpuRatRosterSetup(3, LvSet);
                        playerRatRosterSetup(3);
                        return ACTUALFUCKINGRATFIGHT(mainPlayer.getActivePlayerRatRoster(), CPU.getCPURatRoster(), 3, LvSet);

                    } else {
                        Console.WriteLine("You do not have enough rats");
                        return 0;
                    }
                } else if (selector == 3) {
                    if (mainPlayer.getRatRosterCount() >= 5) {
                        cpuRatRosterSetup(5, LvSet);
                        playerRatRosterSetup(5);
                        return ACTUALFUCKINGRATFIGHT(mainPlayer.getActivePlayerRatRoster(), CPU.getCPURatRoster(), 5, LvSet);
                    } else {
                        Console.WriteLine("You do not have enough rats");
                        return 0;
                    }
                } else if (selector == 0) {
                    return 0;
                } else {
                    Console.WriteLine("Please input valid number: ");
                    selector = numCheck(Console.ReadLine());
                }
                Console.WriteLine("Returning...\n");
                return 0;
            }
        }

        void cpuRatRosterSetup (int numOfRatsToCreate, int Lv) {
            CPU.clearCPURatRoster();
            for (int i = 0; i < numOfRatsToCreate; i++) {
                CPU.addToCPURatRoster( ratCreator(Lv) );
            }
        }
        
        void playerRatRosterSetup(int numOfRatsAllowed)
{
    Console.WriteLine(numOfRatsAllowed + " Rat(s) are allowed, and you get to choose who fights and the order.");
    Console.WriteLine("\nDisplaying your Rats\n");
    mainPlayer.InfoDumpOfRatRoster();

    for (int i = 0; i < numOfRatsAllowed; i++)
    {
        if (mainPlayer.getActiveRatRosterCount() == 5)
        {
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
        Console.WriteLine($"Adding {selectedRat.ratName}");
        mainPlayer.addToActivePlayerRatRoster(selectedRat);

        Console.WriteLine("Current Active Roster:");
        // foreach (var rat in mainPlayer.getActivePlayerRatRoster())
        //     Console.WriteLine(rat.ratName);
        for (int j = 0; i<mainPlayer.getActiveRatRosterCount()-1; j++){
            Console.WriteLine("I AM HERE");
            Console.WriteLine(mainPlayer.getActivePlayerRat(j).ratName);
        }
        Console.WriteLine("Conitueing");
    }
}
        // void playerRatRosterSetup (int numOfRatsAllowed) {
        //     Console.WriteLine(numOfRatsAllowed + " Rat(s) are allowed, and you get to chose who fights and the order ");
        //     Console.WriteLine("\nDisplaying your Rats\n");
        //     mainPlayer.InfoDumpOfRatRoster();
        //     for (int i=0; i<numOfRatsAllowed; i++) {
        //         Console.WriteLine("Chose Rat: ");
        //         int selector = numCheck(Console.ReadLine());
        //         if ( mainPlayer.getActiveRatRosterCount() == 5) {
        //             Console.WriteLine("Active Roster Full");
        //             break;
        //         }

        //         bool ratInListAlready = false;
        //         Console.WriteLine("Checking if rat is in list");
        //         if (mainPlayer.getActivePlayerRatRoster().Contains(mainPlayer.getPlayerRat(selector - 1))) {
        //             Console.WriteLine("Rat already in active roster");
        //             ratInListAlready = true;
        //         }

        //         Console.WriteLine("Done checking");
        //         if (!ratInListAlready) {
        //             Console.WriteLine($"Adding { mainPlayer.getPlayerRat(selector-1).ratName }");
        //             mainPlayer.addToActivePlayerRatRoster(mainPlayer.getPlayerRat(selector-1));
        //         }


        //     }
            
        // }

        int ACTUALFUCKINGRATFIGHT (List<object> playersRats, List<object> enemysRats, int numOfFighters, int fightLv){
            Console.WriteLine("FUCKING FIGHTING");
            if (RatFightHandler(numOfFighters)){
                Console.WriteLine("Player Wins");
                if (fightLv == 1) {
                    return 50*numOfFighters;
                } else if (fightLv == 2) {
                    return 250*numOfFighters;
                } else if (fightLv == 3) {
                    return 500*numOfFighters;
                } else {
                    Console.WriteLine("If you are seeing this I did something wrong :P");
                    return 0*numOfFighters;
                }
            } else {
                Console.WriteLine("CPU Wins");
                return 0;
            }
            
            

        }

        // Atk/(2^(Atk/Def))
        bool RatFightHandler(int numOfFighters) {
            int wins = 1;
            int loses = 1;
            for (int i = 0; i < numOfFighters; i++) {
                Console.WriteLine($"\n*******************************************\nRound {i+1}, Fight!");
                if (RatFight(i)) {
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

        bool RatFight (int i) {
            Random rnd = new Random();
            // player rat vars
            string PlayerName = mainPlayer.getActivePlayerRat(i).ratName;
            float PlayerAtk = mainPlayer.getActivePlayerRat(i).atk;
            float PlayerDef = mainPlayer.getActivePlayerRat(i).def;
            float PlayerSpd = mainPlayer.getActivePlayerRat(i).spd;
            float PlayerHp = mainPlayer.getActivePlayerRat(i).hp;
            bool playerTurn = true;

            // CPU rat vars
            string CPUName = CPU.getCPURat(i).ratName;
            float CPUAtk = CPU.getCPURat(i).atk;
            float CPUDef = CPU.getCPURat(i).def;
            float CPUSpd = CPU.getCPURat(i).spd;
            float CPUHp = CPU.getCPURat(i).hp;
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
                    Console.WriteLine($"{CPUName} took {playerDmg} and is at {CPUHp}");
                    playerTurn = false;
                } else {
                    PlayerHp -= CPUDmg;
                    Console.WriteLine($"{PlayerName} took {CPUDmg} and is at {PlayerHp}");
                    playerTurn = true;
                }

                if (CPUHp <= 0) {
                    Console.WriteLine($"********\n{PlayerName} Has won!\nPlayer wins round {i+1}\n********\n");
                    return true;
                }
                if (PlayerHp <= 0) {
                    Console.WriteLine($"********\n{CPUName} Has won!\nCPU wins round {i+1}\n********\n");
                    return false;
                }
            }
                

            
        }

        // Creates a single rat object with randomized stats
        object ratCreator(int level)
        {
            Random rnd = new Random();
            int adjRndNum = rnd.Next(ratAdjNameList.Length);//grabs rnd adj from list
            int nounRndNum = rnd.Next(ratNounNameList.Length);//grabs rnd noun from list
            string newName = ratAdjNameList[adjRndNum]+" "+ratNounNameList[nounRndNum];//Combines adj & noun
            if (level == 3) {
                //Random rnd = new Random();
                Rat RatLv3 = new Rat (  newName//Name :P
                                        ,3
                                        ,rnd.Next(25, 31)//hp
                                        ,rnd.Next(15, 31)//stam
                                        ,rnd.Next(15, 31)//atk
                                        ,rnd.Next(15, 31)//def
                                        ,rnd.Next(15, 31)//spd
                                    );
                //Console.WriteLine(RatLv3.ToString());
                return RatLv3;
            } else if (level == 2) {
                // Random rnd = new Random();
                Rat RatLv2 = new Rat (  newName//Name :P
                                        ,2
                                        ,rnd.Next(15, 21)//hp
                                        ,rnd.Next(10, 21)//stam
                                        ,rnd.Next(10, 21)//atk
                                        ,rnd.Next(10, 21)//def
                                        ,rnd.Next(10, 21)//spd
                                    );
                //Console.WriteLine(RatLv2.ToString());
                return RatLv2;
            } else {
                // Random rnd = new Random();
                Rat RatLv1 = new Rat(   newName//Name :P
                                        ,1
                                        ,rnd.Next(5, 11)//hp
                                        ,rnd.Next(1, 11)//stam
                                        ,rnd.Next(1, 11)//atk
                                        ,rnd.Next(1, 11)//def
                                        ,rnd.Next(1, 11)//spd
                                    );
                //Console.WriteLine(RatLv1.ToString());
                return RatLv1;
            }
        }



        // Gives Player half decent rat to start out w/
        object givePlayerDefaultRat (){
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
        MainMenu();
        Console.WriteLine("Remember, second mouse gets the cheese. Till next time...\n");
    }
}
