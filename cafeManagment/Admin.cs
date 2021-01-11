using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace cafeManagment
{
    public partial class Admin : MetroSet_UI.Forms.MetroSetForm
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            string conString = "Server=DESKTOP-UUEDE1F;port=3306;Database=cafe;uid=root;pwd=123";
            MySqlConnection connection = new MySqlConnection();
            try
            {
                connection.ConnectionString = conString;
                connection.Open();
                DataTable _table = new DataTable();
                MySqlDataAdapter sql = new MySqlDataAdapter("select id, itemID, itemName, unit , price, discount , totalPrice from recent", connection);
                sql.Fill(_table);
                

                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
