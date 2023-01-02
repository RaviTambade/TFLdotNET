namespace Library;
public class Books{
    private string[] titles;
    public Books(){
        titles=new string[5];

        this.titles[0]="You can Win";
        this.titles[1]="Ignited Minds";
        this.titles[2]="Rich Dad, Poor Dad";
        this.titles[3]="Let us C";
        this.titles[4]="Effective C++";
    }


    //indexer syntax
    public string this[int index]{
        get{return titles[index];}
        set{titles[index]=value;}
    }

}