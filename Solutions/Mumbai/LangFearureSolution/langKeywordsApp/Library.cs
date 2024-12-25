namespace LMS{
    public class Library{
        //Collection: a group of objects
        //Collection namespace:
        //System.Colllections:not recommended to use this namepace classes
        // ArrayList, HashTable, Queue, Stack, SortedList, BitArray

        //System.Collections.Generic : List<>, Dictionary<>, Queue<>, Stack<>, LinkedList<>
        //SOA: Service Oriented Architecture
        //collection interface:
        //interface is a contract
        //interface is used to define specifications
        //interface is used to define capabilities
        //IEnumerable, ICollection, IList, IDictionary, ISet, IOrderedDictionary  
        //IList : Add, Remove, Indexer, Contains, Clear, Insert, RemoveAt, IndexOf
        //ICollection: Count, IsReadOnly, Add, Remove, Contains, Clear, CopyTo
        //IEnumerable: GetEnumerator
        //IDictionary: Add, Remove, Contains, Clear, TryGetValue
        //ISet: Add, Remove, Contains, Clear, UnionWith, IntersectWith, ExceptWith, SymmetricExceptWith
        //IOrderedDictionary: Add, Remove, Contains, Clear, Insert, RemoveAt, IndexOf, GetByIndex, GetKey, GetIndex


        //DRY: Don't Repeat Yourself
        //don't reinvent the wheel
        // collection classes: List, Dictionary, Queue, Stack, LinkedList,
        //                     HashSet, SortedSet, SortedList, SortedDictionary
        private List<Book> books;
        public Library(){
            books = new List<Book>();
        }
        public void AddBook(Book book){
            books.Add(book);
        }
        public void RemoveBook(Book book){
            books.Remove(book);
        }
        public void IssueBook( Book theBook ){
            this.RemoveBook(theBook);
            Console.WriteLine("Book Issued");
        }

        //Indexer
        //Smart Array   
        public Book this[int index]{
            get{
                return books[index];
            }
            set{
                books[index]=value;
            }
        }
    }
}

