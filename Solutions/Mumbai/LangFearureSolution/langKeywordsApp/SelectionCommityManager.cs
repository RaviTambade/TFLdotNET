
namespace Sports
{

    //comparision logic is outsourced to a separate class
    public class SelectionCommityManager:IComparer<Player>{
        public int Compare(Player? x, Player? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Rank.CompareTo(y.Rank);
        }
    }   
}
