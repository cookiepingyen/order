using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


using order;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace order
{
    public partial class Form1 : Form
    {
        String[] foods = { "魚排飯$110", "雞排飯$95", "烤肉飯$90", "雞腿飯$100", "滷肉飯$50" };
        String[] meals = { "蘿蔔湯$30", "餛飩湯$40", "玉米濃湯$35" };
        String[] drinks = { "紅茶$20", "烏龍茶$25", "清茶$30" };
        List<Subtotal> subtotal_list = new List<Subtotal>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            discount_method();
            flowLayoutPanel4.createpanel4(subtotal_list, label2, comboBox1);

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            foods.createfood(flowLayoutPanel1, NumericUpDown_ValueChanged, CheckBox_CheckedChanged);
            meals.createfood(flowLayoutPanel2, NumericUpDown_ValueChanged, CheckBox_CheckedChanged);
            drinks.createfood(flowLayoutPanel3, NumericUpDown_ValueChanged, CheckBox_CheckedChanged);
            comboBox1.Items.Add("打八折");
            comboBox1.Items.Add("打九折");
            comboBox1.Items.Add("雞腿飯送蘿蔔湯");
            comboBox1.Items.Add("烤肉飯買二送一");
            flowLayoutPanel4.create_subpanel_title(comboBox1);


        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown Numberic = (NumericUpDown)sender;
            CheckBox checkBox = (CheckBox)Numberic.Tag;

            String food = checkBox.Text.Split('$')[0];
            int price = int.Parse(checkBox.Text.Split('$')[1]);
            int count = 0;
            Subtotal subtotal = new Subtotal(food, price, count, 0);
            if (Numberic.Value == 0)
            {
                checkBox.Checked = false;
                subtotal.count = (int)Numberic.Value;
                Subtotal item = subtotal_list.Where(x => x.Name == food).FirstOrDefault();
                if (item != null)
                {
                    subtotal_list.Remove(item);
                    discount_method();
                }

            }
            if (Numberic.Value >= 1)
            {
                checkBox.Checked = true;

                Subtotal item = subtotal_list.Where(x => x.Name == food).FirstOrDefault();
                if (item != null)
                {
                    item.count = (int)Numberic.Value;
                    discount_method();
                }
                else
                {
                    subtotal.count = 1;
                    subtotal_list.Add(subtotal);
                    discount_method();
                }
            }

            flowLayoutPanel4.createpanel4(subtotal_list, label2, comboBox1);
        }


        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            NumericUpDown Numberic = (NumericUpDown)checkBox.Tag;
            String food = checkBox.Text.Split('$')[0];
            int price = int.Parse(checkBox.Text.Split('$')[1]);
            int count = 0;
            int total = 0;
            Subtotal subtotal = new Subtotal(food, price, count, total);

            if (checkBox.Checked)
            {
                Numberic.Value = 1;

                Subtotal item = subtotal_list.Where(x => x.Name == food).FirstOrDefault();
                if (item != null)
                {
                    subtotal.count = (int)Numberic.Value;
                    discount_method();
                }
                else
                {
                    subtotal.count = 1;
                    subtotal_list.Add(subtotal);
                    discount_method();
                }
            }
            else
            {
                Numberic.Value = 0;

                Subtotal item = subtotal_list.Where(x => x.Name == food).FirstOrDefault();
                if (item != null)
                {
                    subtotal.count = (int)Numberic.Value;
                    subtotal_list.Remove(item);
                    discount_method();
                }
            }

            flowLayoutPanel4.createpanel4(subtotal_list, label2, comboBox1);
        }



        private void discount_method()
        {
            subtotal_list.RemoveAll(x => (x.Name.Contains("贈送")));

            subtotal_list.ForEach(eachFood =>
            {
                eachFood.total = eachFood.price * eachFood.count;
            });

            if (comboBox1.Text == "雞腿飯送蘿蔔湯")
            {
                Subtotal item = subtotal_list.Where(x => x.Name == "雞腿飯").FirstOrDefault();
                if ((item != null) && (item.count > 0))
                {
                    Subtotal free = new Subtotal("蘿蔔湯(贈送)", 0, item.count, 0);
                    subtotal_list.Add(free);
                }
                else
                {
                    Subtotal free = subtotal_list.Where(x => x.Name == "蘿蔔湯(贈送)").FirstOrDefault();
                    if (free != null)
                    {
                        subtotal_list.Remove(free);
                    }
                }
            }
            else if (comboBox1.Text == "烤肉飯買二送一")
            {
                Subtotal item = subtotal_list.Where(x => x.Name == "烤肉飯").FirstOrDefault();
                if ((item != null) && (item.count >= 2))
                {
                    Subtotal free = new Subtotal("烤肉飯(贈送)", 0, item.count / 2, 0);
                    subtotal_list.Add(free);
                }
                else
                {
                    Subtotal free = subtotal_list.Where(x => x.Name == "烤肉飯(贈送)").FirstOrDefault();
                    if (free != null)
                    {
                        subtotal_list.Remove(free);
                    }
                }
            }
            else if (comboBox1.Text == "打八折")
            {
                subtotal_list.ForEach(eachFood =>
                {
                    eachFood.total = eachFood.price * eachFood.count * 8 / 10;
                });

            }
            else if (comboBox1.Text == "打九折")
            {
                subtotal_list.ForEach(eachFood =>
                {
                    eachFood.total = eachFood.price * eachFood.count * 9 / 10;
                });
            }


        }




        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void panel4_subpanel_content()
        {

        }
    }
}

