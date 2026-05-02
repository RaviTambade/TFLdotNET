namespace TFLCollection;
class EmpComparer:IComparer<Employee>{
    public int Compare(Employee e1, Employee e2){
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
