namespace TFLCollection;
class EmpComparer:IComparer<Employee>{
    public int Compare(Employee e1, Employee e2){
        //int ret = e1.name.Length.CompareTo(e2.name.Length);
        //return ret
        if(e1.salary > e2.salary){
            return 1;
        }
        else if (e1.salary < e2.salary){
            return -1;
        }
        else
        return 0;
}

}