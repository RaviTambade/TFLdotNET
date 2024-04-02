namespace TFLCollection;
public class Stack:ICloneable{
   private int size;
   private int[] sArr;
    public Stack(int s){
        this.size=s;
        this.sArr=new int[size];
    }

    //Deep Copy implementation
    public object Clone(){
        Stack replica=new Stack(this.size);
        this.sArr.CopyTo(replica.sArr,0);
        return replica;
    }
}