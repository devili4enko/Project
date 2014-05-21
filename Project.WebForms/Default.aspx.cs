using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project.Domain;

namespace Project.WebForms
{
    public partial class _Default : Page
    {
        private TranslitWithXMLOptions tr;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        protected void Translit_Click1(object sender, EventArgs e)
        {
            using (tr = new TranslitWithXMLOptions())
            {
                try
                {
                    translitlable.Text = tr.Translit(TextBox1.Text);
                }
                catch (EmptyInputException)
                {
                    translitlable.Text = "Please Enter text";
                }

            }
        }
    }
}