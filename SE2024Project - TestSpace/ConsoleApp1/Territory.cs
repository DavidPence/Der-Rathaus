using System;
using System.Collections;
using System.Collections.Generic;
class Territory {
    private static int lastID = 0;
    private int ID { get; set; }
    public int level { get; set; }
    public string name { get; set;}
    public int areaCode { get; set; }
    public string highestControlPrecent { get; set;}
    private double playerControlPrecent { get; set;}
    private double catControl { get; set;}
    private double birdControl { get; set;}
    private double dogControl { get; set;}

    public Territory (int level, string name, int areaCode, double playerControlPrecent, double catControl, double birdControl, double dogControl) 
    {
        this.ID = lastID++;
        this.level = level;
        this.name = name;
        this.areaCode = areaCode;
        this.playerControlPrecent = playerControlPrecent; 
        this.catControl = catControl;
        this.birdControl = birdControl;
        this.dogControl = dogControl;
    }

    public void displayControlPercents (){
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Player Control: " + playerControlPrecent + "%");
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (catControl != 0) {
            Console.WriteLine("Cat Contol: " + catControl + "%");
        }
        if (birdControl != 0) {
            Console.WriteLine("Bird Contol: " + birdControl + "%");
        }
        if (dogControl != 0) {
            Console.WriteLine("Dog Contol: " + dogControl + "%");
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    // Will need to rework, because I'm dumb :P
    // exchanges control percentages to keep total to 100%
    public void terryControlChange(CombatCPU cpu, double percent, bool win)
    {
        switch (win) {
            case true:
                playerControlPrecent = territoryPercentCheck(playerControlPrecent + percent);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("CPU is " + cpu.name);
                switch (cpu.name){
                    case "Theives' Cat":
                        catControl = territoryPercentCheck(catControl - percent);
                        break;
                    case "Bird Mafia":
                        birdControl = territoryPercentCheck(birdControl - percent);
                        break;
                    case "The Fuzz":
                        dogControl = territoryPercentCheck(dogControl - percent);
                        break;
                }
                break;
            case false:
                playerControlPrecent = territoryPercentCheck(playerControlPrecent - percent);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("CPU is " + cpu.name);
                switch (cpu.name){
                    case "Theives' Cat":
                        catControl = territoryPercentCheck(catControl + percent);
                        break;
                    case "Bird Mafia":
                        birdControl = territoryPercentCheck(catControl + percent);
                        break;
                    case "The Fuzz":
                        catControl = territoryPercentCheck(catControl + percent);
                        break;
                }
                break;
        }
    }

    public double territoryPercentCheck (double newTerryPercent) 
    {
        if (newTerryPercent <= 0) {
            return 00.00;
        } else if (newTerryPercent >= 100.00){
            return 100.00;
        } else {
            return newTerryPercent;
        }
    }

    
    public void returnHighestFactionControl() {
        List<double> controlList = new List<double>();
        controlList = [playerControlPrecent, catControl, birdControl, dogControl];
        int index = 0;
        for (int i = 0; i < 3; i++) {
            if (controlList[i] == controlList[i+1]) {
                index = 4;
            } else if (controlList[i] > controlList[i+1]){
                index = i;
            } else {
                index = i+1;
            }
        }
        switch (index) {
            case 0:
                highestControlPrecent = "Player";
                break;
            case 1:
                highestControlPrecent = "ThievesCat";
                break;
            case 2:
                highestControlPrecent =  "BirdMafia";
                break;
            case 3:
                highestControlPrecent = "TheFuzz";
                break;
            case 4:
                highestControlPrecent = "Tied";
                break;
        }
        highestControlPrecent = "fuck";
    } 
}