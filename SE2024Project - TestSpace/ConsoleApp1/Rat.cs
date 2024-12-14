using System.Text;

class Rat {
        private static int lastID = 0;
        private int ID { get; set; }
        public string ratName { get; set; }
        public int level { get; private set; }
        public int hp { get; set; }
        private int currentHp { get; set; }
        public int stam { get; set; }
        private int currentStam { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int spd { get; set; }
       
       
        // Constructor
        public Rat(string ratName, int level, int hp, int stam, int atk, int def, int spd) {
        this.ID = lastID++;
        this.ratName = ratName;
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

        int getLevel(){
                return level;
        }

        public int getCurrentHp(){
                return currentHp;
        }

        void TakeDamage (int dmg) {
                currentHp = currentHp - dmg;
        }

}