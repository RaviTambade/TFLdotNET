namespace WebApi.Entities
{

    // ProductCatalog
    // Shopping Cart
    // OrderProcessing
    // PaymentProcessing
    // Shipment
    // CRM
    // Membership------------Core logic for 
                             //authentication
                             

    // backend for Users Roles Management

    // Define Database for Users and Roles management
    // Permissions Table
                //permisssionId, description,
    // Roles Table
                //RoleId, Role

    //RolesPermission Table
                //id, roleId, permissionId

    // Users Table
                 //userid, username, password,firstname, lastname, RoleId

    // Model:
                    //POCO : User, Role, Persmission

     // Using MemebershipContenxt
                        // DBSet<Persmission>
                        // DBSet<Role>
                        // DBSet<User>

     // MembershipManager
                        // CRUD 
                        // GetPermsissionforRole
                        // CheckRole(userid)/
                        //..............
    //  MembershipRepository
                        //................
                        //..............

    //MembershipService//
                        ///.


    public static class Role
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Manager = "Manager";
        public const string Distributor = "Distributor";
        public const string Shoppper = "Shopper";
    }
}