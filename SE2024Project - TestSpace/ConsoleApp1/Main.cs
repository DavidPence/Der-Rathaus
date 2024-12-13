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
        void ShopBuy (int selector, List<object> shopRatList) {
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
                } else if (selector == 1) {
                    mainPlayer.addToPlayerRatRoster(shopRatList[5]);
                    Console.WriteLine("Buying Rat 6");
                    break;
                } else {
                    Console.WriteLine("Please input valid #: ");
                    selector = Convert.ToInt32(Console.ReadLine());
                }
            }
        }

        void ShopMenu (){
            Console.WriteLine("***** Welcome to the Rats R Us*****\n");
            Console.Write("[1] Buy Rat\n[2] Sell Rat\n[3] Return\nEnter: ");
            int input = numCheck(Console.ReadLine());
            while (true) { 
                    if (input == 1) {
                        Console.Write("Lvl of Rats: ");
                        int LvOfRats = numCheck(Console.ReadLine());
                        List<object> shopRatList = new List<object>();
                        shopRatList = ShopDisplay(5, Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine("Enter [1-6] To buy respective Rat, [0] to close: ");
                        int input2 = Convert.ToInt32(Console.ReadLine());
                        ShopBuy(input2, shopRatList);

                    } else if (input == 2) {
                        Console.WriteLine("Selling Rat is not available ");
                    } else if (input == 3) {
                        Console.WriteLine("Exitting");
                        break;
                    } else {
                        Console.WriteLine("Please input valid #: ");
                        input = Convert.ToInt32(Console.ReadLine());
                    }
                    Console.Write("[1] Buy Rat\n[2] Sell Rat\n[3] Return\nEnter: ");
                    input = Convert.ToInt32(Console.ReadLine());
            }
        }

        void MainMenu () {
            Console.WriteLine("Welcome to ...Derrr RrrATHaus");
            Console.WriteLine("");
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
            }
            else if (level == 2) 
            {
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
            }
            else
            {
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

        void cpuRatRosterSetup (int numOfRatsToCreate) {
            CPU.addToCPURatRoster( ratCreator(1) );
        }

        cpuRatRosterSetup(5);
        ShopMenu();


    
        
    }
}
