using System.Text;

class Rat {
        private static int lastID = 0;
        public int ID { get; private set; }
        public int hp { get; set; }
        public int currentHp { get; set; }
        public int stam { get; set; }
        public int currentStam { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int spd { get; set; }
        public string ratName { get; set; }
        
        // Constructor
        public Rat(int hp, int stam, int atk, int def, int spd, string ratName) {
        this.ID = lastID++;
        this.hp = hp;
        this.currentHp = hp;
        this.stam = stam;
        this.currentStam = stam;
        this.atk = atk;
        this.def = def;
        this.spd = spd;
        this.ratName = ratName;
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
}