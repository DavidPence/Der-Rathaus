using System.Text;
using System.Collections;
using System.Collections.Generic;
class Rat {
        private static int lastID = 0;
        private int ID { get; set; }
        public string name { get; set; }
        public int level { get; private set; }
        public int hp { get; set; }
        private int currentHp { get; set; }
        public int stam { get; set; }
        private int currentStam { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int spd { get; set; }
       
       
        // Constructor
        public Rat(string name, int level, int hp, int stam, int atk, int def, int spd) {
        this.ID = lastID++;
        this.name = name;
        this.level = level;
        this.hp = hp;
        this.currentHp = hp;
        this.stam = stam;
        this.currentStam = stam;
        this.atk = atk;
        this.def = def;
        this.spd = spd;
        }


        // ToString
        public override string ToString() {
                return GetType().GetProperties()
                        .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"))
                        .Aggregate( new StringBuilder(),
                                   (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                                    sb => sb.ToString()
                                  );
        }

        public int getLevel(){
                return level;
        }

        public int getCurrentHp(){
                return currentHp;
        }

        string getName(){
                return name;
        }

        void takeDamage (int dmg) {
                currentHp = currentHp - dmg;
        }

        public bool Equals(object obj){
                if (obj is Rat otherRat) 
                        return this.ID == otherRat.ID; // Compare based on unique ID
        
                return false;
        }

    public override int GetHashCode(){
        return ID.GetHashCode(); // Generate hash code based on ID
    }

}