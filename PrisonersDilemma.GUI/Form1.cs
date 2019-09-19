using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Logic.Services;
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
        public class MongoTestConventions : IConventionPack
        {
            private static bool conventionsRegistred = false;
            
            public IEnumerable<IConvention> Conventions => new List<IConvention>()
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };

            public static void RegisterConventions()
            {
                if (!conventionsRegistred)
                {
                    ConventionRegistry.Register("CustomConventions", new MongoTestConventions(), x => true);
                    conventionsRegistred = true;
                }                
            }
        }

        ConnectionStringProvider connectionStringProvider = new ConnectionStringProvider();
        public Dictionary<string, int> StrategiesPerSimulation { get; set; }
        List<Strategy> Strategies = new List<Strategy>();
        public Form1()
        {
            InitializeComponent();
            StrategiesPerSimulation = new Dictionary<string, int>();

            ConventionRegistry.Register("CustomConventions", new MongoTestConventions(), x => true);
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
            StrategyRepository repo = new StrategyRepository(connectionStringProvider);
            Strategies = await new StrategyService(repo).GetAllStrategies();
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

        private async void button6_Click(object sender, EventArgs e)
        {
            var simRepo = new SimulationRepository(connectionStringProvider);
            var gameSettings = new GameSettingsProvider();

            var strategyService = new StrategyService(new StrategyRepository(connectionStringProvider));
            var popService = new PopulationService(new GameService(strategyService, gameSettings));
            SimulationService simService = new SimulationService(simRepo, popService, strategyService, new SimulationSettingsProvider());
            List<Player> players = GetPlayersForSimulation();
            try
            {
                Simulation sim = await simService.Run(players);
                string message = sim.Winner != null ? $"{sim.Winner} : {sim.Winner.StrategyName}" : "No winner";
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {

            }
        }

        private List<Player> GetPlayersForSimulation()
        {
            var list = new List<Player>();
            foreach(var kvp in StrategiesPerSimulation)
            {
                string name = kvp.Key;
                int strategiesCount = kvp.Value;
                Strategy strategy = Strategies.Where(s => s.Name == name).FirstOrDefault();
                for(int i = 0; i < strategiesCount; i++)
                {
                    list.Add(new Player() { Id = Guid.NewGuid().ToString(), StrategyName = strategy.Name, StrategyId = strategy.Id });
                }
            }
            return list;
        }
    }
}
