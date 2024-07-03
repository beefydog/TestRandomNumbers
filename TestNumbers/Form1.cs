using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using MathNet.Numerics.Statistics;

namespace TestNumbers
{
    public partial class Form1 : Form
    {
        public ushort bits { get; set; } = 8;

        public Form1()
        {
            InitializeComponent();
            ComputeRange(bits);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            ToggleButtons();
            ResetForm();
            bits = ushort.Parse(txtBits.Text.Trim());
            int iterations = int.Parse(txtIterations.Text.Trim());

            var progress = new Progress<int>(value => progressBar1.Value = value);
            progressBar1.Maximum = iterations;
            progressBar1.Value = 0;

            if (radCPURandom.Checked)
            {
                DisplayResults(await Task.Run(() => CPURandom(iterations, bits, progress)));
            }
            else
            {
                DisplayResults(await Task.Run(() => AlgoRandom(iterations, bits, progress)));
            }
            ToggleButtons();
        }

        private void DisplayResults(Results results)
        {
            IEnumerable<double> nums = results.Numbers;
            txtResults.Text = results.TextResults;
            txtAverage.Text = Statistics.Mean(nums).ToString("N2");
            txtMedian.Text = Statistics.Median(nums).ToString("N2");
            txtOther.Text = Statistics.PopulationSkewness(nums).ToString("N3");
            lblOther.Text = "PopulationSkewness:";
        }

        public static Results AlgoRandom(int iterations, ushort bits, IProgress<int> progress)
        {
            List<double> Numbers = new();
            if (bits > 64) bits = 64;

            BigInteger HighNumber = (BigInteger.One << bits) - 1;

            StringBuilder sbResults = new();
            Random rnd = new Random();

            for (int j = 0; j < iterations; j++)
            {
                BigInteger RandomNumber = GetRandomBigInteger(rnd, bits);
                Numbers.Add((double)RandomNumber);
                sbResults.AppendLine(RandomNumber.ToString());
                progress.Report(j + 1); 
            }

            return new() { Numbers = Numbers, TextResults = sbResults.ToString() };
        }

        private static BigInteger GetRandomBigInteger(Random rnd, int bits)
        {
            byte[] bytes = new byte[(bits + 7) / 8]; // Number of bytes needed
            rnd.NextBytes(bytes);

            BigInteger result = new BigInteger(bytes);
            return result & ((BigInteger.One << bits) - 1); // Mask to ensure the correct number of bits
        }

        public static Results CPURandom(int iterations, ushort bits, IProgress<int> progress)
        {
            List<double> Numbers = new();
            StringBuilder sbResults = new();

            for (int j = 0; j < iterations; j++)
            {
                StringBuilder sb = new();
                for (ushort i = 0; i < bits; i++)
                {
                    sb.Append(GenerateBoolean() ? '1' : '0');
                }
                string numberStr = sb.ToString();
                if (GenerateBoolean()) numberStr.Reverse();

                UInt64 number = Convert.ToUInt64(numberStr, 2);
                Numbers.Add(Convert.ToDouble(number));
                sbResults.AppendLine(number.ToString());
                progress.Report(j + 1); 
            }

            return new() { Numbers = Numbers, TextResults = sbResults.ToString() };
        }

        public static bool GenerateBoolean()
        {
            int gen1 = 0;
            int gen2 = 0;
            Task.Run(() =>
            {
                while (gen1 < 1 || gen2 < 1) Interlocked.Increment(ref gen1);
            });
            while (gen1 < 1 || gen2 < 1) Interlocked.Increment(ref gen2);
            return (gen1 + gen2) % 2 == 0;
        }

        private void ComputeRange(ushort bits)
        {
            UInt64 topOfRange;
            if (bits > 63)
            {
                topOfRange = ulong.MaxValue;
                bits = 64;
            }
            else
            {
                topOfRange = (1UL << bits) - 1UL;
            }

            txtRange.Text = "0 - " + topOfRange.ToString();
            if (bits == 64) txtBits.Text = "64";
        }

        private void txtBits_Leave(object sender, EventArgs e)
        {
            bits = ushort.Parse(txtBits.Text.Trim());
            ComputeRange(bits);
        }

        private void ResetForm()
        {
            txtResults.Text = string.Empty;
            txtAverage.Text = string.Empty;
            txtMedian.Text = string.Empty;
            txtOther.Text = string.Empty;   
            lblOther.Text = string.Empty;           
        }

        private void ToggleButtons()
        {
            btnStart.Enabled = !btnStart.Enabled;
        }
    }

    public class Results
    {
        public List<double>? Numbers { get; set; }
        public string? TextResults { get; set; }
    }
}
