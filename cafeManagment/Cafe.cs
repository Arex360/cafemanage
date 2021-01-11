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
    public partial class Cafe : MetroSet_UI.Forms.MetroSetForm

    {
        public class Emp
        {
            public static string id;
            public static float dprogress;
            public static float mprogress;
            public static float dtask;
            public static float mtask;
        }
        public class Connection
        {
            public static string conString = "Server=DESKTOP-UUEDE1F;port=3306;Database=cafe;uid=root;pwd=123";
            public static void Connect()
            {
                MySqlConnection connection;
               
                try
                {
                    connection = new MySql.Data.MySqlClient.MySqlConnection();
                    connection.ConnectionString = conString;
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        MessageBox.Show("COnnected");
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error");

                }
            }
        }
        public class Item
        {
            public int sr;
            public string id;
            public string name;
            public int units;
            public float price;
            public float discount;
            public float totalPrice;
            // sum up
            public string sUnits;
            public string sDiscount;
            public string sPrice;
            public string sTotalPrice;
            public Item(int sr,string id, string name, int units, float price, float discount)
            {
                this.sr = sr;
                this.id = id;
                this.name = name;
                this.units = units;
                this.price = price;
                this.discount = discount;

                this.totalPrice = this.units * this.price;
                this.totalPrice = this.totalPrice - ((this.totalPrice/100) * this.discount);

                // String conversions;
                this.sUnits = this.units.ToString();
                this.sPrice = this.price.ToString();
                this.sDiscount = this.price.ToString();
                this.sTotalPrice = this.totalPrice.ToString();
            }
                
        }
        // login details
        private string _username;
        private string _password;
        private string _accountType;

        //Employee

        private int saleIndex;
        private List<Item> _items = new List<Item>();
        private int sIndex = 0;
        private string _orderID;
        
        DataTable _table = new DataTable();
        private void ResetView()
        {
            rGrid.Visible = false;
            recentBtn.Visible = true;
        }
        public Cafe()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            Connection.Connect();

            loginPanel.Visible = true;
            employepanel.Visible = false;
            this.saleIndex = 0;
            float _dPerc = (Emp.dprogress / Emp.dtask) * 100;
            float _mPerc = (Emp.dprogress / Emp.dtask) * 100;
            dailyPerformance.From = 0;
            dailyPerformance.To = 100;
            dailyPerformance.Value = _dPerc;

            monthlyPerformance.From = 0;
            monthlyPerformance.To = 100;
            monthlyPerformance.Value = _mPerc;

            //ResetView();
            Console.WriteLine("Debugging state");

            proceedPanel.Visible = false;

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _id = pID.Text;
            string _name = pName.Text;
            int _unit = int.Parse(pUnit.Text);
            float _price = float.Parse(pPrice.Text);
            float _discount = float.Parse(pDiscount.Text);
            float _total = _price * _unit;
            _total = _total - ((_total/100) * _discount);
            sGrid.Rows.Add();
            _items.Add(new Item(sIndex, _id, _name, _unit, _price, _discount));
            sGrid.Rows[saleIndex].Cells[0].Value = _id;
            sGrid.Rows[saleIndex].Cells[1].Value = _name;
            sGrid.Rows[saleIndex].Cells[2].Value = _unit.ToString();
            sGrid.Rows[saleIndex].Cells[3].Value = _price.ToString();
            sGrid.Rows[saleIndex].Cells[4].Value = _discount.ToString();
            sGrid.Rows[saleIndex].Cells[5].Value = _total.ToString();

            saleIndex++;



        }

        private void button3_Click(object sender, EventArgs e)
        {
            float _total = 0;
            float _dtotal = (Emp.dtask/100) * Emp.dprogress;
            //MessageBox.Show(_dtotal.ToString());
            float _mtotal = (Emp.mtask / 100) * Emp.mprogress;
            foreach(Item item in _items)
            {
                _total += item.totalPrice;
                _dtotal += item.totalPrice;
                _mtotal += item.totalPrice;
            }
            //   setProgress(_dtotal.ToString(), _mtotal.ToString(), Emp.dtask.ToString(), Emp.mtask.ToString());
            
            Emp.dprogress = (_dtotal / Emp.dtask) * 100;
            Emp.mprogress = (_mtotal / Emp.mtask) * 100;
            string pdaily = Emp.dprogress.ToString() + "%";
            MessageBox.Show(pdaily);
            string pmonthly = Emp.mprogress.ToString() + "%";
            int index = pmonthly.IndexOf("%");
            Emp.mprogress = float.Parse(pmonthly.Substring(0, index));
            //MessageBox.Show(Emp.id);
            sIndex++;
            saleIndex = 0;
            MetroFramework.MetroMessageBox.Show(this, "OK", $"total price is {_total}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sGrid.Rows.Clear();
            DataTable table = new DataTable();
            bool isValid = false;
            string conString = "Server=DESKTOP-UUEDE1F;port=3306;Database=cafe;uid=root;pwd=123";
            MySqlConnection connection = new MySqlConnection();
            try
            {
                connection.ConnectionString = conString;
                connection.Open();
                MySqlCommand updateCMD = connection.CreateCommand();
                updateCMD.CommandText = $"Update employee set dprogress = '{pdaily}' where id = {Emp.id} ";
                updateCMD.ExecuteNonQuery();
                updateCMD.CommandText = $"Update employee set mprogress = '{pmonthly}' where id = {Emp.id} ";
                updateCMD.ExecuteNonQuery();
                updateCMD.CommandText = "insert into saleiteration(total,employeID) values (@total,@id)";
                updateCMD.Prepare();
                updateCMD.Parameters.AddWithValue("@total", _total);
                updateCMD.Parameters.AddWithValue("@id", Emp.id);
                updateCMD.ExecuteNonQuery();
                foreach (Item item in _items)
                {
                    MySqlCommand cmd = connection.CreateCommand();
                //    cmd.Connection = connection;
                    cmd.CommandText = "insert into recent (itemID, itemName, unit, price,discount,totalPrice, employeID) values(@itemid,@itemname,@unit,@price,@discount,@totalprice,@employe)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@itemid", item.id);
                    cmd.Parameters.AddWithValue("@itemname", item.name);
                    cmd.Parameters.AddWithValue("@unit", item.sUnits);
                    cmd.Parameters.AddWithValue("@price", item.sPrice);
                    cmd.Parameters.AddWithValue("@discount", item.sDiscount);
                    cmd.Parameters.AddWithValue("@totalprice", item.sTotalPrice);
                    cmd.Parameters.AddWithValue("@employe", Emp.id);
                    MessageBox.Show("Item added to database");
                    cmd.ExecuteNonQuery();
                }

                connection.Close();

                _items.Clear();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void totalPerformance_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _type = accType.SelectedItem.ToString();
            if(_type == "Customer")
            {
                //Client c = new Client();
                // c.Show();
                // c.StartPosition = this.StartPosition;
                //this.Hide();
                DataTable table = new DataTable();
                bool isValid = false;
                string conString = "Server=DESKTOP-UUEDE1F;port=3306;Database=cafe;uid=root;pwd=123";
                MySqlConnection connection = new MySqlConnection();
                try
                {
                    connection.ConnectionString = conString;
                    connection.Open();
                    MySqlDataAdapter sql = new MySqlDataAdapter("select username,password from customer", connection);
                    sql.Fill(table);
                    for (int i = 0; i < table.Rows.Count; i++)
                     {
                        DataRow row = table.Rows[i];
                        string tempName = row["username"].ToString();
                        string tempPass = row["password"].ToString();
                        if(tempName == username.Text)
                        {
                            if(tempPass == password.Text)
                            {
                                MessageBox.Show("Logged in");
                                isValid = true;
                            }
                            else
                            {
                                MessageBox.Show("wrong password");
                            }
                            break;
                        }
                       
                       
                    }
                    if (isValid)
                    {
                        Client c = new Client();
                        c.Show();
                        c.StartPosition = this.StartPosition;
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Try again");
                    }

                    connection.Close();
                }
                catch(MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //sql.CommandType = CommandType.StoredProcedure;

            }
            else if(_type == "Employee")
            {
               // employepanel.Visible = true;
               // loginPanel.Visible = false;
                DataTable table = new DataTable();
                bool isValid = false;
                string conString = "Server=DESKTOP-UUEDE1F;port=3306;Database=cafe;uid=root;pwd=123";
                MySqlConnection connection = new MySqlConnection();
                try
                {
                    connection.ConnectionString = conString;
                    connection.Open();
                    MySqlDataAdapter sql = new MySqlDataAdapter("select id,name,password,dprogress,mprogress,dtarget,mtarget from employee", connection);
                    sql.Fill(table);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        string tempName = row["name"].ToString();
                        string tempPass = row["password"].ToString();
                        if (tempName == username.Text)
                        {
                            if (tempPass == password.Text)
                            {
                                setProgress(row["dprogress"].ToString(),row["mprogress"].ToString(), row["dtarget"].ToString(), row["mtarget"].ToString());
                                Emp.id = row["id"].ToString();
                                float _dPerc = (Emp.dprogress / Emp.dtask) * 100;
                                float _mPerc = (Emp.mprogress / Emp.mtask) * 100;
                                dailyPerformance.From = 0;
                                dailyPerformance.To = 100;
                                dailyPerformance.Value = (Math.Floor(_dPerc) <= 100) ? Math.Floor(_dPerc): 100;

                                monthlyPerformance.From = 0;
                                monthlyPerformance.To = 100;
                                monthlyPerformance.Value = (Math.Floor(_mPerc) <= 100) ? Math.Floor(_mPerc) : 100;


                                MessageBox.Show($"logged in daily {Emp.dprogress}");
                                isValid = true;
                            }
                            else
                            {
                                MessageBox.Show("wrong password");
                            }
                            break;
                        }


                    }
                    if (isValid)
                    {
                        employepanel.Visible = true;
                        loginPanel.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Try again");
                    }

                    connection.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void setProgress(string daily, string monthly, string dtask, string mtask)
        {
            int i1 = daily.IndexOf("%");
            int i2 = monthly.IndexOf("%");
            int i3 = dtask.IndexOf("%");
            int i4 = mtask.IndexOf("%");
            Emp.dtask = float.Parse(dtask);
            Emp.mtask = float.Parse(mtask);
            Emp.dprogress = float.Parse(daily.Substring(0,i1));
            Emp.mprogress = float.Parse(monthly.Substring(0, i2));
            Emp.dprogress = (Emp.dtask / 100) * Emp.dprogress;
            Emp.mprogress = (Emp.mtask / 100) * Emp.mprogress;
          
        }
        private void metroSetSetTabPage2_Click(object sender, EventArgs e)
        {
          
        }

        private void recentBtn_Click(object sender, EventArgs e)
        {
            //recentBtn.Visible = false;
            /*rGrid.Rows.Clear();
            int index = 0;
            monthlyPerformance.Value = 0;
            dailyPerformance.Value = 0;
            foreach (Item item in _items)
            {
                rGrid.Rows.Add();
                rGrid.Rows[index].Cells[0].Value = item.sr.ToString();
                rGrid.Rows[index].Cells[1].Value = item.id;
                rGrid.Rows[index].Cells[2].Value = item.name;
                rGrid.Rows[index].Cells[3].Value = item.sUnits;
                rGrid.Rows[index].Cells[4].Value = item.sPrice;
                rGrid.Rows[index].Cells[5].Value = item.sDiscount;
                rGrid.Rows[index].Cells[6].Value = item.sTotalPrice;
              //  monthlyPerformance.Value += item.totalPrice;
              //  dailyPerformance.Value = monthlyPerformance.Value;
                index++;

            }*/
            DataTable table = new DataTable();
            bool isValid = false;
            string conString = "Server=DESKTOP-UUEDE1F;port=3306;Database=cafe;uid=root;pwd=123";
            MySqlConnection connection = new MySqlConnection();
            try
            {
                connection.ConnectionString = conString;
                connection.Open();
                MySqlDataAdapter sql = new MySqlDataAdapter("select id, itemID, itemName, unit , price, discount , totalPrice from recent", connection);
                sql.Fill(table);
                rGrid.DataSource = table;

                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            int index = 0;
            rGrid.Visible = true;
            
            

        }

        private void metroSetSetTabPage1_Click(object sender, EventArgs e)
        {
            ResetView();
        }

        private void employepanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void loginPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            float _dPerc = (Emp.dprogress / Emp.dtask) * 100;
          //  float _mPerc = (Emp.mprogress / Emp.mtask) * 100;
            dailyPerformance.From = 0;
            dailyPerformance.To = 100;
            dailyPerformance.Value = (_dPerc <= 100) ? _dPerc : 100 ;

            monthlyPerformance.From = 0;
            monthlyPerformance.To = 100;
            monthlyPerformance.Value = (Emp.mprogress <= 100) ? Emp.mprogress : 100;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            _orderID = oID.Text;
            orderPanel.Visible = false;
            proceedPanel.Visible = true;
        }

        private void metroSetTextBox5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            orderPanel.Visible = true;
            proceedPanel.Visible = false;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            orderPanel.Visible = true;
            proceedPanel.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            bool isValid = false;
            string conString = "Server=DESKTOP-UUEDE1F;port=3306;Database=cafe;uid=root;pwd=123";
            MySqlConnection connection = new MySqlConnection();
            try
            {
                connection.ConnectionString = conString;
                connection.Open();
                MySqlDataAdapter sql = new MySqlDataAdapter("select name,password,dprogress,mprogress,dtarget,mtarget from employee", connection);
                sql.Fill(table);
                orderGrid.DataSource = table;

                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
