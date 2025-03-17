using System.Collections;
using System.Collections.Generic;
class Territory {
    private static int lastID = 0;
    private int ID { get; set; }
    private int level { get; set; }
    private string name { get; set;}
    private double playerControlPrecent { get; set;}
    private double catControl { get; set;}
    private double birdControl { get; set;}
    private double dogControl { get; set;}

    public Territory (int level, string name, double playerControlPrecent, double catControl, double birdControl, double dogControl) {
        this.ID = lastID++;
        this.level = level;
        this.name = name;
        this.playerControlPrecent = playerControlPrecent; 
        this.catControl = catControl;
        this.birdControl = birdControl;
        this.dogControl = dogControl;
    }

    public double territoryPercentCheck (double newTerryPercent) {
        if (newTerryPercent <= 0) {
            return 00.00;
        } else if (newTerryPercent >= 100.00){
            return 100.00;
        } else {
            return newTerryPercent;
        }
    }

    
}