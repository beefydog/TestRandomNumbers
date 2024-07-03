using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using MathNet.Numerics.Statistics;

namespace TestNumbers
{
    public partial class Form1 : Form
    {
        public ushort Bits { get; set; } = 8;

        public Form1()
        {
            InitializeComponent();
            ComputeRange(Bits);
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            ResetForm();
            ToggleButtons();
            Bits = ushort.Parse(txtBits.Text.Trim());
            int iterations = int.Parse(txtIterations.Text.Trim());

            var progress = new Progress<int>(value => progressBar1.Value = value);
            progressBar1.Maximum = iterations;
            progressBar1.Value = 0;

            Results results;
            if (radCPURandom.Checked)
            {
                results = await Task.Run(() => CPURandom(iterations, Bits, progress));
            }
            else
            {
                results = await Task.Run(() => AlgoRandom(iterations, Bits, progress));
            }
            DisplayResults(results);
            progressBar1.Value = progressBar1.Maximum;
            ToggleButtons();
        }

        private void DisplayResults(Results results)
        {
            IEnumerable<double> nums = results.Numbers ?? [];
            txtResults.Text = results.TextResults;
            txtAverage.Text = Statistics.Mean(nums).ToString("N2");
            txtMedian.Text = Statistics.Median(nums).ToString("N2");
            txtOther.Text = Statistics.PopulationSkewness(nums).ToString("N3");
            lblOther.Text = "Population Skewness:";
        }

        public static Results AlgoRandom(int iterations, ushort bits, IProgress<int> progress)
        {
            List<double> Numbers = [];
            if (bits > 64) bits = 64;

            BigInteger HighNumber = (BigInteger.One << bits) - 1;

            StringBuilder sbResults = new();
            Random rnd = new();

            for (int j = 0; j < iterations; j++)
            {
                BigInteger RandomNumber = GetRandomBigInteger(rnd, bits);
                Numbers.Add((double)RandomNumber);
                sbResults.AppendLine(RandomNumber.ToString());
                progress.Report(j + 1);
            }

            return new() { Numbers = Numbers, TextResults = sbResults.ToString() };
        }

        private static BigInteger GetRandomBigInteger(Random rnd, ushort bits)
        {
            byte[] bytes = new byte[(bits + 7) / 8]; // determine bytes needed
            rnd.NextBytes(bytes);

            BigInteger result = new(bytes);
            return result & ((BigInteger.One << bits) - 1); // Mask to ensure the correct number of bits
        }

        public static Results CPURandom(int iterations, ushort bits, IProgress<int> progress)
        {
            List<double> Numbers = [];
            StringBuilder sbResults = new();

            for (int j = 0; j < iterations; j++)
            {
                StringBuilder sbBits = new(bits);
                for (ushort i = 0; i < bits; i++)
                {
                    sbBits.Append(GenerateBoolean() ? '1' : '0');
                }
                UInt64 number = Convert.ToUInt64(sbBits.ToString(), 2);
                Numbers.Add(Convert.ToDouble(number));
                sbResults.AppendLine(number.ToString());
                progress.Report(j + 1);
            }

            return new() { Numbers = Numbers, TextResults = sbResults.ToString() };
        }

        public static bool GenerateBoolean()
        {
            long gen1 = 0;
            long gen2 = 0;
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
        private void TxtBits_Leave(object sender, EventArgs e)
        {
            if (txtBits.Text.Length == 0)
            {
                txtBits.Text = "8";
                Bits = 8;
            }
            else
            {
                Bits = ushort.Parse(txtBits.Text.Trim());
            }
            ComputeRange(Bits);
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Txt_TextChanged(object sender, EventArgs e)
        {
            if (sender is not TextBox textBox) return;

            string newText = new(textBox.Text.Where(char.IsDigit).ToArray());
            if (textBox.Text != newText)
            {
                textBox.Text = newText;
                textBox.SelectionStart = newText.Length;
            }
        }

        private void txtIterations_Leave(object sender, EventArgs e)
        {
            if (txtIterations.Text.Length == 0) txtIterations.Text = "100";
            if (txtIterations.Text.Length > 6) txtIterations.Text = "999999";
        }
    }

    public class Results
    {
        public List<double>? Numbers { get; set; }
        public string? TextResults { get; set; }
    }
}
