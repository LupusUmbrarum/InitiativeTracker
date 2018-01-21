using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InitiativeTracker
{
    public partial class Form1 : Form
    {
        List<Actor> actors;
        Actor temp;

        public Form1()
        {
            InitializeComponent();
            actors = new List<Actor>();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Enter:
                    if(textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                    {
                        try
                        {
                            temp = new Actor(textBox1.Text, Convert.ToInt32(textBox2.Text));
                            actors.Add(temp);
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox1.Focus();
                        }
                        catch (Exception ex) { }
                    }
                    sortList();
                    displayList();
                    break;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (textBox1.Text.Length > 0)
                    {
                        if(textBox2.Text.Length > 0)
                        {
                            try
                            {
                                temp = new Actor(textBox1.Text, Convert.ToInt32(textBox2.Text));
                                actors.Add(temp);
                                textBox1.Text = "";
                                textBox2.Text = "";
                            }
                            catch (Exception ex) { }
                        }
                        else
                        {
                            textBox2.Focus();
                        }
                    }
                    sortList();
                    displayList();
                    break;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void sortList()
        {
            for(int x = 0; x < actors.Count()-1; x++)
            {
                for(int y = 0; y < actors.Count()-x-1; y++)
                {
                    if(actors[y].initiative < actors[y+1].initiative)
                    {
                        Actor temp = actors[y];
                        actors[y] = actors[y + 1];
                        actors[y + 1] = temp;
                    }
                }
            }
        }

        private void displayList()
        {
            label1.Text = "";
            for(int x = 0; x < actors.Count(); x++)
            {
                label1.Text += "("+(x+1) + ") " + actors[x].name + " " + actors[x].initiative + "\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                try
                {
                    Actor temp = new Actor(textBox1.Text, Convert.ToInt32(textBox2.Text));
                    actors.Add(temp);
                }
                catch (Exception ex) { }
            }
            sortList();
            displayList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            actors.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox3.Text.Length > 0)
            {
                try
                {
                    actors.RemoveAt(Convert.ToInt32(textBox3.Text)-1);
                }
                catch (Exception ex) { }
                displayList();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (textBox3.Text.Length > 0)
                {
                    try
                    {
                        actors.RemoveAt(Convert.ToInt32(textBox3.Text) - 1);
                    }
                    catch (Exception ex) { }
                    displayList();
                }
            }
        }
    }

    public class Actor
    {
        public string name;
        public int initiative;

        public Actor(String name, int initiative)
        {
            this.name = name;
            this.initiative = initiative;
        }
    }
}
