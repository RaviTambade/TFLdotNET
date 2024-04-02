namespace EStoreWebApp.Models;

public class RevenueData{

        public static List<RevenueModel> GetCityRevenueList(){
            var list = new List<RevenueModel>();  
            list.Add(new RevenueModel { Name = "Nashik", RevenueYear2020 = 28000, RevenueYear2010 = 45000, RevenueYear2000 = 22000, RevenueYear1990 = 50000 });  
            list.Add(new RevenueModel { Name = "Satara", RevenueYear2020 = 30000, RevenueYear2010 = 49000, RevenueYear2000 = 24000, RevenueYear1990 = 39000 });  
            list.Add(new RevenueModel { Name = "Satara", RevenueYear2020 = 35000, RevenueYear2010 = 56000, RevenueYear2000 = 26000, RevenueYear1990 = 41000 });  
            list.Add(new RevenueModel { Name = "Pune", RevenueYear2020 = 37000,RevenueYear2010 = 44000, RevenueYear2000 = 28000 , RevenueYear1990 = 48000 });  
            list.Add(new RevenueModel { Name = "Mumbai",RevenueYear2020 = 40000, RevenueYear2010 = 38000, RevenueYear2000 = 30000 , RevenueYear1990 = 54000 });  
             return list;    
        }

         public static List<RevenueModel> GetStateRevenueList(){
            var list = new List<RevenueModel>();  

            list.Add(new RevenueModel { Name = "Chennai", RevenueYear2020 = 28000, RevenueYear2010 = 15000, RevenueYear2000 = 22000, RevenueYear1990 = 50000 });  
            list.Add(new RevenueModel { Name = "Delhi", RevenueYear2020 = 30000, RevenueYear2010 = 19000, RevenueYear2000 = 24000, RevenueYear1990 = 39000 });  
            list.Add(new RevenueModel { Name = "Kochi", RevenueYear2020 = 35000, RevenueYear2010 = 16000, RevenueYear2000 = 26000, RevenueYear1990 = 41000 });  
            list.Add(new RevenueModel { Name = "Kolkata", RevenueYear2020 = 37000, RevenueYear2010 = 14000, RevenueYear2000 = 28000 , RevenueYear1990 = 48000 });  
            list.Add(new RevenueModel { Name = "Goa", RevenueYear2020 = 40000, RevenueYear2010 = 18000, RevenueYear2000 = 30000 , RevenueYear1990 = 54000 });  

             return list;    
        }
    }
