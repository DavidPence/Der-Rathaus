using System.Collections;
using System.Collections.Generic;

class CombatCPU {
    List<object> cpuRatRoster = new List<object>();

    public List<object> getCPURatRoster (){
        return cpuRatRoster;
    }
    public Rat getCPURat(int i){
            Rat tempRat = (Rat)cpuRatRoster[i];
            return tempRat;
    }

    public void addToCPURatRoster(object newRat) {
        cpuRatRoster.Add(newRat);
    }

    public void clearCPURatRoster() {
        cpuRatRoster.Clear();
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
}
 