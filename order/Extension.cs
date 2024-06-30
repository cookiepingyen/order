using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace order
{
    internal static class Extension
    {

        public static void createfood(this String[] foods, FlowLayoutPanel panel, EventHandler numericUpDownHandler, EventHandler checkBoxHandler)
        {
            foreach (var food in foods)
            {
                FlowLayoutPanel subpanel = new FlowLayoutPanel();
                subpanel.Height = 50;
                CheckBox checkbox = new CheckBox();
                checkbox.Text = food;
                checkbox.CheckedChanged += checkBoxHandler; // 設定 CheckBox 的事件處理方法
                NumericUpDown numeric = new NumericUpDown();
                numeric.ValueChanged += numericUpDownHandler; // 設定 NumericUpDown 的事件處理方法
                numeric.Width = 70;
                subpanel.Controls.Add(checkbox);
                subpanel.Controls.Add(numeric);
                panel.Controls.Add(subpanel);
                checkbox.Tag = numeric;
                numeric.Tag = checkbox;

            }
        }

        public static int caltotal(this String[] foods, FlowLayoutPanel panel, FlowLayoutPanel panel4)
        {
            int total = 0;

            for (int i = 0; i < panel.Controls.Count; i++)
            {
                int price = 0;
                int number = 0;
                FlowLayoutPanel subpanel = (FlowLayoutPanel)panel.Controls[i];
                CheckBox checkbox = (CheckBox)subpanel.Controls[0];


                if (checkbox.Checked == true)
                {
                    price = int.Parse(checkbox.Text.Split('$')[1]);
                    NumericUpDown numeric = (NumericUpDown)subpanel.Controls[1];
                    number = (int)numeric.Value; // 25.00

                }

                total += price * number;
            }
            return total;


        }
        public static void createpanel4(this FlowLayoutPanel panel4, List<Subtotal> subtotal_list, Label label2, ComboBox comboBox)
        {

            panel4.Controls.Clear();
            create_subpanel_title(panel4, comboBox);
            int sum = 0;


            subtotal_list.ForEach(eachFood =>
            {
                FlowLayoutPanel subpanel = new FlowLayoutPanel();
                subpanel.Width = 600;
                subpanel.Height = 20;

                Label labelfood = new Label();
                Label labelprice = new Label();
                Label labelnumber = new Label();
                Label labeltotal = new Label();
                labeltotal.Width = 50;
                Label labeldiscount = new Label();

                labelfood.Text = eachFood.Name;
                labelprice.Text = eachFood.price.ToString();
                labelnumber.Text = eachFood.count.ToString();
                labeltotal.Text = (eachFood.total).ToString();
                labeldiscount.Text = comboBox.Text;

                subpanel.Controls.Add(labelfood);
                subpanel.Controls.Add(labelprice);
                subpanel.Controls.Add(labelnumber);
                subpanel.Controls.Add(labeltotal);
                subpanel.Controls.Add(labeldiscount);
                panel4.Controls.Add(subpanel);

                sum += (int)(eachFood.total);

            });
            label2.Text = sum.ToString();
        }

        public static void create_subpanel_title(this FlowLayoutPanel panel4, ComboBox combox)
        {
            FlowLayoutPanel subpan2_title = new FlowLayoutPanel();
            panel4.Controls.Add(subpan2_title);
            subpan2_title.Width = 600;
            subpan2_title.Height = 30;
            Label foodtitle = new Label();
            Label pricetitle = new Label();
            Label numbertitle = new Label();
            Label totaltitle = new Label();
            totaltitle.Width = 50;
            Label disconutitle = new Label();
            foodtitle.Text = "品項";
            pricetitle.Text = "價格";
            numbertitle.Text = "數量";
            totaltitle.Text = "小計";
            disconutitle.Text = "打折";
            subpan2_title.Controls.Add(foodtitle);
            subpan2_title.Controls.Add(pricetitle);
            subpan2_title.Controls.Add(numbertitle);
            subpan2_title.Controls.Add(totaltitle);
            subpan2_title.Controls.Add(disconutitle);
        }


        //public static void discountfunction(this FlowLayoutPanel panel4, List<Subtotal> subtotal_list, ComboBox comboBox)
        //{
        //    int chicken_count = 0;
        //    int roast_count = 0;
        //    int price = 0;
        //    int count = 0;

        //    FlowLayoutPanel subpanel = new FlowLayoutPanel();
        //    subpanel.Width = 600;
        //    subpanel.Height = 20;

        //    Label labelfood = new Label();
        //    Label labelprice = new Label();
        //    Label labelnumber = new Label();
        //    Label labeltotal = new Label();
        //    labeltotal.Width = 50;


        //    if (comboBox.Text == "雞腿飯送蘿蔔湯")
        //    {
        //        subtotal_list.ForEach((eachFood) =>
        //        {
        //            if (eachFood.Name == "雞腿飯")
        //            {
        //                chicken_count = eachFood.count;

        //                String food = "蘿蔔湯(贈送)";
        //                price = 0;
        //                count = chicken_count;
        //                labelfood.Text = food;
        //                labelprice.Text = price.ToString();
        //                labelnumber.Text = count.ToString();
        //                labeltotal.Text = (price * count).ToString();
        //                subpanel.Controls.Add(labelfood);
        //                subpanel.Controls.Add(labelprice);
        //                subpanel.Controls.Add(labelnumber);
        //                subpanel.Controls.Add(labeltotal);
        //                panel4.Controls.Add(subpanel);

        //            }
        //        });
        //    }
        //    else if (comboBox.Text == "烤肉飯買二送一")
        //    {
        //        subtotal_list.ForEach((eachFood) =>
        //        {
        //            if (eachFood.Name == "烤肉飯")
        //            {
        //                roast_count = (int)(eachFood.count / 2);
        //                String food = "烤肉飯(贈送)";
        //                price = 0;
        //                count = roast_count;
        //                labelfood.Text = food;
        //                labelprice.Text = price.ToString();
        //                labelnumber.Text = count.ToString();
        //                labeltotal.Text = (price * count).ToString();
        //                subpanel.Controls.Add(labelfood);
        //                subpanel.Controls.Add(labelprice);
        //                subpanel.Controls.Add(labelnumber);
        //                subpanel.Controls.Add(labeltotal);
        //                panel4.Controls.Add(subpanel);

        //            }
        //        });
        //    }

        //}
    }
}
