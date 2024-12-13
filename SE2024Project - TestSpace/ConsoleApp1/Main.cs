using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
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
                Console.WriteLine("[1] - Shop\n[2] - Fight\n[3] - Your Inventory\n[0] - Close Program\n Enter: ");
#pragma warning disable CS8604 // Possible null reference argument.
                selector = numCheck(Console.ReadLine());
#pragma warning restore CS8604 // Possible null reference argument.
                if (selector == 1) {
                    ShopMenu();
                } else if (selector == 2) {
                    CombatMenu();
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
        
        List<object> ShopDisplay (int numOfRatsToCreate, int RatLv) {
            // Console.WriteLine("Welcome to the Rats R Us\n");
            List<object> shopRatList = new List<object>();
            for (int i = 0; i < numOfRatsToCreate+1; i++) {
                Console.WriteLine((i+1) + " Rat created!");
                shopRatList.Add( ratCreator(RatLv) );
            }
            int count = 0;
            Console.WriteLine("Rats in List " + shopRatList.Count);
            foreach (object shopRat in shopRatList) {
                count++;
                Console.WriteLine("Displaying Rat: " + count);
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
                        Console.WriteLine("You do not have enough money to buy a Level " + LvToCost + "rat");
                        return;
                    }
                    break;
                } else if (LvToCost == 2) {
                    costOfRat = 500;
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + LvToCost + "rat");
                        return;
                    }
                    break;
                } else if (LvToCost == 3) {
                    costOfRat = 1000;
                    if (mainPlayer.money < costOfRat) {
                        Console.WriteLine("You do not have enough money to buy a Level " + LvToCost + "rat");
                        return;
                    }
                    break;
                }
            }
            while (true) {    
                if (selector == 0) {
                    Console.WriteLine("Returning");
                    break;
                } else if (selector == 1) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[0]);
                    Console.WriteLine("Buying Rat 1");
                    break;
                } else if (selector == 2) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[1]);
                    Console.WriteLine("Buying Rat 2");
                    break;
                } else if (selector == 3) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[2]);
                    Console.WriteLine("Buying Rat 3");
                    break;
                } else if (selector == 4) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[3]);
                    Console.WriteLine("Buying Rat 4");
                    break;
                } else if (selector == 5) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[4]);
                    Console.WriteLine("Buying Rat 5");
                    break;
                } else if (selector == 6) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[5]);
                    Console.WriteLine("Buying Rat 6");
                    break;
                } else {
                    Console.WriteLine("Please input valid #: ");
#pragma warning disable CS8604 // Possible null reference argument.
                    selector = numCheck(Console.ReadLine());
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
        }

        void ShopMenu (){
            Console.WriteLine("***** Welcome to the Rats R Us*****\n");
            //Console.Write("[1] Buy Rat\n[2] Sell Rat\n[3] Return\nEnter: ");
            int input /*= numCheck(Console.ReadLine())*/;
            while (true) { 
                Console.Write("[1] Buy Rat\n[2] Sell Rat\n[3] Return\nEnter: ");
                input = numCheck(Console.ReadLine());
                if (input == 1) {
                    Console.Write("Lvl of Rats: ");
                    int LvOfRats = numCheck(Console.ReadLine());
                    List<object> shopRatList = new List<object>();
                    shopRatList = ShopDisplay(5, numCheck(Console.ReadLine()));
                    Console.WriteLine("Enter [1-6] To buy respective Rat, [0] to close: ");
                    int input2 = numCheck(Console.ReadLine());
                    ShopBuy(input2, shopRatList, LvOfRats);
                } else if (input == 2) {
                   Console.WriteLine("Selling Rat is not available ");
                } else if (input == 3) {
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
            Console.WriteLine("***** Your Inventory *****");
            //Console.WriteLine("[1] - Your Rat(s)\n[2] - Your Stats\n[0] - Return\nEnter: ");
            int selector /*= numCheck(Console.ReadLine())*/;
            while (true) {
                Console.WriteLine("[1] - Your Rat(s)\n[2] - Your Stats\n[0] - Return\nEnter: ");
                selector = numCheck(Console.ReadLine());
                if (selector == 1) {
                        mainPlayer.InfoDumpOfRatRoster();
                    } else if (selector == 2) {
                        Console.WriteLine("Your Money: " + mainPlayer.money);
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

        void CombatMenu (){
            Console.WriteLine("Entering the RatClurb...");
            Console.WriteLine("What Level of Rats do you want to fight\n[1] - Level 1\n[2] - Level 2\n[3] - Level 3\n[0] - Return\nEnter: ");
            int selector = numCheck(Console.ReadLine());
            while (true) {   
                if (selector == 1) {
                    ChallengeSetupCombatMenu(1);
                } else if (selector == 2) {
                    ChallengeSetupCombatMenu(2);
                } else if (selector == 3) {
                    ChallengeSetupCombatMenu(3);
                } else if (selector == 0) {
                    break;
                } else {
                    Console.WriteLine("Please input valid number: ");
                    selector = numCheck(Console.ReadLine());
                }
            }
        }

        void ChallengeSetupCombatMenu (int LvSet){
            Console.WriteLine("What Challenge do you want to face\n[1] - 1 Enemy Rat\n[2] - 3 Enemy Rats\n[3] - 5 Enemy Rats\n[0] - Return\n Enter: ");
            int selector = numCheck(Console.ReadLine());
            mainPlayer.clearActivePlayerRatRoster();
            while (true) {
                if (selector == 1) { //Player should always have at least one rat
                    cpuRatRosterSetup(1, LvSet);
                    playerRatRosterSetup(1);
                    ACTUALFUCKINGRATFIGHT(mainPlayer.getActivePlayerRatRoster(), CPU.getCPURatRoster(), 1, LvSet);
                } else if (selector == 2) {
                    if (mainPlayer.getRatRosterCount() >= 3) {
                        cpuRatRosterSetup(3, LvSet);
                        playerRatRosterSetup(3);
                        ACTUALFUCKINGRATFIGHT(mainPlayer.getActivePlayerRatRoster(), CPU.getCPURatRoster(), 3, LvSet);
                    }
                } else if (selector == 3) {
                    if (mainPlayer.getRatRosterCount() >= 5) {
                        cpuRatRosterSetup(5, LvSet);
                        playerRatRosterSetup(5);
                        ACTUALFUCKINGRATFIGHT(mainPlayer.getActivePlayerRatRoster(), CPU.getCPURatRoster(), 5, LvSet);
                    }
                } else if (selector == 0) {
                    break;
                } else {
                    Console.WriteLine("Please input valid number: ");
                    selector = numCheck(Console.ReadLine());
                }
                Console.WriteLine("Returning...\n");
            }
        }

        void cpuRatRosterSetup (int numOfRatsToCreate, int Lv) {
            CPU.clearCPURatRoster();
            for (int i = 0; i < numOfRatsToCreate; i++) {
                CPU.addToCPURatRoster( ratCreator(Lv) );
            }
        }
        
        void playerRatRosterSetup (int numOfRatsAllowed) {
            Console.WriteLine(numOfRatsAllowed + " Rat(s) are allowed, and you get to chose the order of who fights");
            Console.WriteLine("\nDisplaying your Rats\n");
            mainPlayer.InfoDumpOfRatRoster();
            
        }

        void ACTUALFUCKINGRATFIGHT (List<object> playersRats, List<object> enemysRats, int NumOfFighters, int fightLv){
            Job RatFight = new Job(playersRats, enemysRats, 1);

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
                Rat RatLv3 = new Rat (   rnd.Next(25, 30)//hp
                                        ,rnd.Next(15, 30)//stam
                                        ,rnd.Next(15, 30)//atk
                                        ,rnd.Next(15, 30)//def
                                        ,rnd.Next(15, 30)//spd
                                        ,newName//Name :P
                                    );
                //Console.WriteLine(RatLv3.ToString());
                return RatLv3;
            } else if (level == 2) {
                // Random rnd = new Random();
                Rat RatLv2 = new Rat (   rnd.Next(15, 20)//hp
                                        ,rnd.Next(10, 20)//stam
                                        ,rnd.Next(10, 20)//atk
                                        ,rnd.Next(10, 20)//def
                                        ,rnd.Next(10, 20)//spd
                                        ,newName//Name :P
                                    );
                //Console.WriteLine(RatLv2.ToString());
                return RatLv2;
            } else {
                // Random rnd = new Random();
                Rat RatLv1 = new Rat(   rnd.Next(5, 10)//hp
                                        ,rnd.Next(1, 10)//stam
                                        ,rnd.Next(1, 10)//atk
                                        ,rnd.Next(1, 10)//def
                                        ,rnd.Next(1, 10)//spd
                                        ,newName//Name :P
                                    );
                //Console.WriteLine(RatLv1.ToString());
                return RatLv1;
            }
        }


        // Gives Player half decent rat to start out w/
        void givePlayerDefaultRat (){
            Rat RatBase = new Rat(  8//hp
                                    ,10//stam
                                    ,5//atk
                                    ,5//def
                                    ,7//spd
                                    ,"Rattus Norvegicus"//Name
                                    );
            mainPlayer.addToPlayerRatRoster(RatBase);
        }

        givePlayerDefaultRat(); // :D
        MainMenu();
        Console.WriteLine("Remember, second mouse gets the cheese. Till next time...\n");
    }
}
