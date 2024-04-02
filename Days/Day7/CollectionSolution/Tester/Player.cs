namespace TFLCollection;
using System.Collections;
using System.Collections.Generic;

public class Player:IComparable {
    private string name;
    private int runs;
    private int matches;
    private int age;

    public Player(string name, int age, int matches, int runs){
        this.name=name;
        this.age=age;
        this.matches=matches;
        this.runs=runs;
    }

    public int CompareTo(object? obj)
    {
        Player anotherPlayer=(Player) obj;

        if(this.runs > anotherPlayer.runs){
            return 1;
        }
        else if (this.runs < anotherPlayer.runs){
            return -1;
        }
        else
        return 0;

    }

}
