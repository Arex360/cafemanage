using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafeManagment
{
   


    public partial class Client : MetroSet_UI.Forms.MetroSetForm
    {
        public class Food
        {
            public string id;
            public string name;
            public int ammount;
            public float price;
    
        }
        public class Tea: Food
        {
            public Tea(string id,string name, int ammount, float price)
            {
                this.id = id;
                this.name = name;
                this.ammount = ammount;
                this.price = price;
            }
        }
        public class Cake : Food
        {
            public Cake(string id,string name, int ammount , float price)
            {
                this.id = id;
                this.name = name;
                this.ammount = ammount;
                this.price = price;
            }
        }
        
        public class Sandwich : Food
        {
            public Sandwich(string id,string name, int ammount, float price)
            {
                this.id = id;
                this.name = name;
                this.ammount = ammount;
                this.price = price;
            }
        }

        public class Coffee : Food
        {
            public Coffee(string id,string name, int ammount, float price)
            {
                this.id = id;
                this.name = name;
                this.ammount = ammount;
                this.price = price;
            }
        }

        public class Bread:Food
        {
            public Bread(string id,string name, int ammount, float price)
            {
                this.id = id;
                this.name = name;
                this.ammount = ammount;
                this.price = price;
            }
        }

        private List<Tea> _tea = new List<Tea>();
        private List<Coffee> _coffee = new List<Coffee>();
        private List<Cake> _cake = new List<Cake>();
        private List<Sandwich> _sandwich = new List<Sandwich>();
        private List<Bread> _bread = new List<Bread>();

        private int _cartIndex;

        public Client()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            _cartIndex = 0;
            cartGrid.Visible = false;
            optionPanel.Visible = false;
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = int.Parse(normalTeaAmmount.Text);
            if(n > 0)
            {
                _tea.Add(new Tea("t1","Normal Tea",n, 30f));
            }
            else
            {
                return;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int n = int.Parse(blackTea.Text);
            if(n > 0)
            {
                _tea.Add(new Tea("t2","Black Tea", n, 60f));
            }
            else
            {
                return;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = int.Parse(iceTea.Text);
            if(n > 0)
            {
                _tea.Add(new Tea("t3","Ice Tea", n, 100f));
            }
            else
            {
                return;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n = int.Parse(kChai.Text);
            if(n > 0)
            {
                _tea.Add(new Tea("t4","Kashmiri Tea", n, 100f));
            }
            else
            {
                return;
            }
            
        }

        private void metroSetSetTabPage6_Click(object sender, EventArgs e)
        {
            ResetCart();
        }
        private void ResetCart()
        {
            loadCart.Visible = true;
            cartGrid.Visible = false;
            optionPanel.Visible = false;
        }
        private void loadCart_Click(object sender, EventArgs e)
        {
            cartGrid.Rows.Clear();
            _cartIndex = 0;
            if (_tea.Count > 0)
            {

                foreach (Tea t in _tea)
                {
                    float _totalPrice = t.ammount * t.price;
                    cartGrid.Rows.Add();
                    cartGrid.Rows[_cartIndex].Cells[0].Value = t.name;
                    cartGrid.Rows[_cartIndex].Cells[1].Value = t.ammount.ToString();
                    cartGrid.Rows[_cartIndex].Cells[2].Value = t.price.ToString();
                    cartGrid.Rows[_cartIndex].Cells[3].Value = _totalPrice;

                    _cartIndex++;

                }
            }
            if(_coffee.Count > 0)
            {
                foreach (Coffee c in _coffee)
                {
                    float _totalPrice = c.ammount * c.price;
                    cartGrid.Rows.Add();
                    cartGrid.Rows[_cartIndex].Cells[0].Value = c.name;
                    cartGrid.Rows[_cartIndex].Cells[1].Value = c.ammount.ToString();
                    cartGrid.Rows[_cartIndex].Cells[2].Value = c.price.ToString();
                    cartGrid.Rows[_cartIndex].Cells[3].Value = _totalPrice;

                    _cartIndex++;

                }
            }
            if(_bread.Count > 0)
            {
                foreach (Bread c in _bread)
                {
                    float _totalPrice = c.ammount * c.price;
                    cartGrid.Rows.Add();
                    cartGrid.Rows[_cartIndex].Cells[0].Value = c.name;
                    cartGrid.Rows[_cartIndex].Cells[1].Value = c.ammount.ToString();
                    cartGrid.Rows[_cartIndex].Cells[2].Value = c.price.ToString();
                    cartGrid.Rows[_cartIndex].Cells[3].Value = _totalPrice;

                    _cartIndex++;

                }
            }
            if(_cake.Count > 0)
            {
                foreach (Cake c in _cake)
                {
                    float _totalPrice = c.ammount * c.price;
                    cartGrid.Rows.Add();
                    cartGrid.Rows[_cartIndex].Cells[0].Value = c.name;
                    cartGrid.Rows[_cartIndex].Cells[1].Value = c.ammount.ToString();
                    cartGrid.Rows[_cartIndex].Cells[2].Value = c.price.ToString();
                    cartGrid.Rows[_cartIndex].Cells[3].Value = _totalPrice;

                    _cartIndex++;

                }
            }


            float totaAmmount = 0;
            for(int i = 0; i < _cartIndex; i++)
            {
                float tPrice = int.Parse(cartGrid.Rows[i].Cells[3].FormattedValue.ToString());
                totaAmmount += tPrice;
            }
            cPrice.From = 0;
            cPrice.To = totaAmmount;
            cPrice.Value = totaAmmount;

            loadCart.Visible = false;
            cartGrid.Visible = true;
            optionPanel.Visible = true;
        }

        private void metroSetSetTabPage1_Click(object sender, EventArgs e)
        {
            ResetCart();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int n = int.Parse(greenTea.Text);
            if(n > 0)
            {
                _tea.Add(new Tea("t5","Green Tea", n, 50f));
            }else
            {
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int n = int.Parse(ltea.Text);
            if (n > 0)
            {
                _tea.Add(new Tea("t6","Leamon Tea", n, 50f));
            }
            else
            {
                return;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int n = int.Parse(nCoffee.Text);
            if(n > 0)
            {
                _coffee.Add(new Coffee("c1","Normal coffee", n, 60f));
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int n = int.Parse(coldCoffee.Text);
            if (n > 0)
            {
                _coffee.Add(new Coffee("c3","Cold coffee", n, 110f));
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int n = int.Parse(hotCoffee.Text);
            if (n > 0)
            {
                _coffee.Add(new Coffee("c4","Hot coffee", n, 130f));
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int n = int.Parse(ausCoffee.Text);
            if (n > 0)
            {
                _coffee.Add(new Coffee("c5","Australian coffee", n, 200f));
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n = int.Parse(tiktok.Text);
            if (n > 0)
            {
                _coffee.Add(new Coffee("c6","Tiktok special", n, 1000f));
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int n = int.Parse(sCold.Text);
            if (n > 0)
            {
                _coffee.Add(new Coffee("c7","special cold coffee", n, 200f));
            }
        }

        private void metroSetSetTabPage2_Click(object sender, EventArgs e)
        {
            ResetCart();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int n = int.Parse(nSandwich.Text);
            if(n > 0)
            {
                _sandwich.Add(new Sandwich("s1", "Normal Sandwich", n, 60));
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int n = int.Parse(chSandwich.Text);
            if(n > 0)
            {
                _sandwich.Add(new Sandwich("s2", "Chicken Sandwich", n, 110f));
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int n = int.Parse(cSandwich.Text);
            if(n > 0)
            {
                _sandwich.Add(new Sandwich("s3", "Cheese Sandwich", n, 130f));
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int n = int.Parse(ccSandwich.Text); 
            if(n > 0)
            {
                _sandwich.Add(new Sandwich("s4", "Chicken cheese sandwich", n, 200f));
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int n = int.Parse(sSandwich.Text);
            if(n > 0)
            {
                _sandwich.Add(new Sandwich("s5", "Special Sandwich", n, 100f));
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int n = int.Parse(spSandwich.Text);
            if(n > 0)
            {
                _sandwich.Add(new Sandwich("s6", "Spicy sandwich", n, 130f));
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            int n = int.Parse(nCake.Text);
            if(n > 0)
            {
                _cake.Add(new Cake("c1", "normal cake", n, 200f));
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int n = int.Parse(cCake.Text);
            if(n > 0)
            {
                _cake.Add(new Cake("c2", "cheese cake", n, 400f));
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            int n = int.Parse(chCake.Text);
            if(n > 0)
            {
                _cake.Add(new Cake("c3", "Choclate cake", n, 400f));
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int n = int.Parse(cLava.Text);
            if(n > 0)
            {
                _cake.Add(new Cake("c4", "Choclate lava", n, 600f));
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int n = int.Parse(pcake.Text);
            if(n > 0)
            {
                _cake.Add(new Cake("c5", "Pineapple cake", n, 300f));
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            int n = int.Parse(fCake.Text);
            if(n > 0)
            {
                _cake.Add(new Cake("c6", "Fruit Cake", n, 250f));
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            int n = int.Parse(nBread.Text);
            if(n > 0)
            {
                _bread.Add(new Bread("b1", "Normal bread", n, 30f));
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            int n = int.Parse(sBread.Text);
            if(n > 0)
            {
                _bread.Add(new Bread("b2", "Sweet Bread", n, 40f));
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            int n = int.Parse(gBread.Text);
            if(n > 0)
            {
                _bread.Add(new Bread("b3", "Garlic Bread", n, 40f));
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            int n = int.Parse(aBread.Text);
            if(n > 0)
            {
                _bread.Add(new Bread("b4", "Special Bread", n, 60f));
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            int n = int.Parse(cBread.Text);
            if(n > 0)
            {
                _bread.Add(new Bread("b5", "Chicken Bread", n, 60f));
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            int n = int.Parse(oBread.Text);
            if(n > 0)
            {
                _bread.Add(new Bread("b6", "Onion Bread", n, 40f));
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {

        }

        private void button30_Click_1(object sender, EventArgs e)
        {
            _tea.Clear();
            _coffee.Clear();
            _bread.Clear();
            _cake.Clear();
            cartGrid.Rows.Clear();
            cPrice.To = 100;
            cPrice.From = 0;
            cPrice.Value = 0;
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
    }
}
