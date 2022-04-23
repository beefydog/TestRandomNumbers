using System.Text;
using MathNet.Numerics.Statistics;
namespace TestNumbers;


public partial class Form1 : Form
{
    public UInt64 bits { get; set; }

    public Form1()
    {
        InitializeComponent();
        ComputeRange();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Int64 iterations = Int64.Parse(txtIterations.Text.Trim());

        List<double> Numbers = new();
        StringBuilder sbFinal = new();

        for (int j = 0; j < iterations; j++)
        {
            StringBuilder sb = new();
            for (UInt64 i = 0; i < bits; i++)
            {
                sb.Append(GenerateBoolean() ? '1' : '0');
            }
            Int64 number = Convert.ToInt64(sb.ToString(), 2);
            Numbers.Add(Convert.ToDouble(number));
            sbFinal.AppendLine(number.ToString());
        }

        txtResults.Text = sbFinal.ToString();
        txtAverage.Text = Statistics.Mean((IEnumerable<double>)Numbers).ToString("N2");
        txtMedian.Text = Statistics.Median((IEnumerable<double>)Numbers).ToString("N2");
    }

    public static bool GenerateBoolean()
    {
        UInt64 gen1 = 0;
        UInt64 gen2 = 0;
        Task.Run(() =>
        {
            while (gen1 < 1 || gen2 < 1) Interlocked.Increment(ref gen1);
        });
        while (gen1 < 1 || gen2 < 1) Interlocked.Increment(ref gen2);
        return (gen1 + gen2) % 2 == 0;
    }

    private void ComputeRange()
    {
        bits = UInt64.Parse(txtBits.Text.Trim());
        UInt64 topOfRange;

        if (bits > 63)
        {
            topOfRange = 18446744073709551615;
            bits = 64;
        }
        else
        {         
            topOfRange = Convert.ToUInt64(Math.Pow(2, bits)) - 1;
        }

        txtRange.Text = "0 - " + topOfRange.ToString();
        if (bits == 64) txtBits.Text = "64";
    }

    private void txtBits_TextChanged(object sender, EventArgs e)
    {
        //ComputeRange();
    }

    private void txtBits_Leave(object sender, EventArgs e)
    {
        ComputeRange();
    }
}
