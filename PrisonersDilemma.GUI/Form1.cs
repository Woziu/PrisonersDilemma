using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrisonersDilemma.GUI
{
    public partial class Form1 : Form
    {
        public Dictionary<string, int> StrategiesPerSimulation { get; set; }
        public Form1()
        {
            InitializeComponent();
            StrategiesPerSimulation = new Dictionary<string, int>();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void AddLogLine(string line)
        {
            richTextBox1.Text += DateTime.Now.ToString() + ": " + line + Environment.NewLine;
        }

        private string GetNameAndCount(string strategyName)
        {
            return StrategiesPerSimulation.ContainsKey(strategyName) ?
                strategyName + ": " + StrategiesPerSimulation[strategyName] : null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //get strategies names from db
            List<Strategy> strategies = new List<Strategy>();
            //clear listbox
            listBox1.Items.Clear();
            AddLogLine("Updating strategies list...");
            //add names
            strategies.Add(new Strategy() { Name = "Simple Cooperator" });
            strategies.Add(new Strategy() { Name = "Simple Cheater" });
            strategies.ForEach(s => listBox1.Items.Add(s.Name));
            AddLogLine("Updated strategies list");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > 0 && listBox2.SelectedIndex >= 0)
            {                
                string selectedName = listBox2.Items[listBox2.SelectedIndex].ToString().Split(':')[0];//Simple Cooperator: 5
                if (StrategiesPerSimulation[selectedName] > 1)
                {
                    StrategiesPerSimulation[selectedName]--;
                    listBox2.Items[listBox2.SelectedIndex] = GetNameAndCount(selectedName);
                }
                else
                {
                    StrategiesPerSimulation.Remove(selectedName);
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            StrategiesPerSimulation.Clear();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.Items.Count > 0 && listBox1.SelectedIndex >= 0)
            {
                string selectedName = listBox1.Items[listBox1.SelectedIndex].ToString();
                if (StrategiesPerSimulation.ContainsKey(selectedName))
                {                    
                    int currentIndex = listBox2.Items.IndexOf(GetNameAndCount(selectedName));
                    StrategiesPerSimulation[selectedName]++;
                    listBox2.Items[currentIndex] = GetNameAndCount(selectedName);
                }
                else
                {
                    StrategiesPerSimulation.Add(selectedName, 1);
                    listBox2.Items.Add(selectedName + ": 1");
                }
                
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
