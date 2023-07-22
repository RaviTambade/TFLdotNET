using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPXWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = this.TextBox1.Text;
            string password = this.TextBox2.Text;
            if(email =="ravi.tambade@transflower.in"
                && password == "seed123")
            {
                this.Response.Redirect("welcome.aspx");
            }

        }
    }
}