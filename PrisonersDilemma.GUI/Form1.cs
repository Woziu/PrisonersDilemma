using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Logic.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PrisonersDilemma.GUI
{
    public partial class Form1 : Form
    {
        private readonly ISimulationService _simulationService;
        private readonly IStrategyService _strategyService;

        private Dictionary<string, int> StrategiesPerSimulation { get; set; }
        private List<Strategy> Strategies { get; set; }

        public Form1(ISimulationService simulationService, IStrategyService strategyService)
        {
            InitializeComponent();
            _simulationService = simulationService;
            _strategyService = strategyService;

            StrategiesPerSimulation = new Dictionary<string, int>();
            Strategies = new List<Strategy>();


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

        private async void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            AddLogLine("Updating strategies list...");
            Strategies = await _strategyService.GetAllStrategies();
            //strategies.Add(new Strategy() { Name = "Simple Cooperator" });
            //strategies.Add(new Strategy() { Name = "Simple Cheater" });
            Strategies.ForEach(s => listBox1.Items.Add(s.Name));
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
            if (listBox1.Items.Count > 0 && listBox1.SelectedIndex >= 0)
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

        private async void button6_Click(object sender, EventArgs e)
        {
            try
            {
                List<Player> players = GetPlayersForSimulation();
                Simulation sim = await _simulationService.Run(players);
                string message = sim.Winner != null ? $"{sim.Winner.StrategyName} : {sim.Winner.Score}" : "No winner";
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {

            }
        }

        private List<Player> GetPlayersForSimulation()
        {
            var list = new List<Player>();
            try
            {
                foreach (var kvp in StrategiesPerSimulation)
                {
                    string name = kvp.Key;
                    int strategiesCount = kvp.Value;
                    Strategy strategy = Strategies.Where(s => s.Name == name).FirstOrDefault();
                    for (int i = 0; i < strategiesCount; i++)
                    {
                        var player = new Player()
                        {
                            Id = Guid.NewGuid().ToString(),
                            StrategyName = strategy.Name,
                            StrategyId = strategy.Id
                        };
                        list.Add(player);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Couldnt get players for simulation", ex);
            }
            return list;
        }
    }
}
