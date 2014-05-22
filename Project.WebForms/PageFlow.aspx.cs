using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project.WebForms
{
    public partial class PageFlow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (Control item in Controls)
            {
                Response.Write(item.GetType().ToString() + " " + item.ID + "<br/> ");
            }
            Response.Write("<hr/>");
            TestLable.Text += "PageLoad " + Environment.NewLine;

            if (Page.IsPostBack)
            {
                TestLable.Text += "            123                              " + Environment.NewLine;
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            TestLable.Text += "Page_Init " + Environment.NewLine;

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            TestLable.Text += "Page_PreRender " + Environment.NewLine;

        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            TestLable.Text += "Unload " + Environment.NewLine;

        }

        protected void TestBtn_Click(object sender, EventArgs e)
        {
            TestLable.Text += "TestBtn_Click " + Environment.NewLine;
        }
    }
}