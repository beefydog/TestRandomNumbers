using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

using MathNet.Numerics.Statistics;
namespace TestNumbers;


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
        bits = ushort.Parse(txtBits.Text.Trim());
        int iterations = int.Parse(txtIterations.Text.Trim());
        if (radCPURandom.Checked)
        {
            DisplayResults(await Task.Run(() => CPURandom(iterations, bits)));
        }
        else
        {
            DisplayResults(await Task.Run(() => AlgoRandom(iterations, bits)));
        }
    }

    /// <summary>
    /// Due to limitations in the Statistics library (which uses double), any list of numbers having powers greater than 2^53 will be
    /// computed inaccurately for the Mean, Median, and PopulationSkewness. This is a mantissa limitation with double datatype.
    /// </summary>
    /// <param name="results"></param>
    private void DisplayResults(Results results)
    {
        IEnumerable<double> nums = (IEnumerable<double>)(results.Numbers);
        txtResults.Text = results.TextResults;
        txtAverage.Text = Statistics.Mean(nums).ToString("N2");
        txtMedian.Text = Statistics.Median(nums).ToString("N2");
        txtOther.Text = Statistics.PopulationSkewness(nums).ToString("N3");
        lblOther.Text = "PopulationSkewness:";
    }

    /// <summary>
    /// Generates random numbers via the algorithm. a.k.a. pseudo-random
    /// Utilized BigIntegers to be able to compute random numbers up to 2^64 -1 
    /// The rnd.NextInt64 only works with signed longs, not ulongs, so 2^63-1 is the highest positive
    /// </summary>
    /// <param name="iterations"></param>
    /// <param name="bits"></param>
    /// <returns></returns>
    public static Results AlgoRandom(int iterations, ushort bits)
    {
        List<double> Numbers = new();
        if (bits > 64) bits = 64; //hard limit to 64bit

        BigInteger HighNumber = (BigInteger.One << bits) - 1;

        StringBuilder sbResults = new();
        Random rnd = new Random();

        for (int j = 0; j < iterations; j++)
        {
            BigInteger RandomNumber = GetRandomBigInteger(rnd, bits);
            Numbers.Add((double)RandomNumber);
            sbResults.AppendLine(RandomNumber.ToString());
        }

        return new() { Numbers = Numbers, TextResults = sbResults.ToString() };
    }

    /// <summary>
    /// Helper function for AlgoRandom uses rnd.NextBytes instead 
    /// </summary>
    /// <param name="rnd"></param>
    /// <param name="bits"></param>
    /// <returns></returns>
    private static BigInteger GetRandomBigInteger(Random rnd, int bits)
    {
        byte[] bytes = new byte[(bits + 7) / 8]; // Number of bytes needed
        rnd.NextBytes(bytes);

        BigInteger result = new BigInteger(bytes);
        return result & ((BigInteger.One << bits) - 1); // Mask to ensure the correct number of bits
    }


    /// <summary>
    /// Generates random numbers from CPU Noise - no algorithm, more cores = more randomness
    /// utilizes GenerateBoolean for random binaries, then assembles into string
    /// </summary>
    /// <param name="iterations"></param>
    /// <param name="bits"></param>
    /// <returns></returns>
    public static Results CPURandom(int iterations, ushort bits)
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
        }

        return new() { Numbers = Numbers, TextResults = sbResults.ToString() };
    }

    /// <summary>
    /// Helper function for CPURandom 
    /// This is the secret sauce for generating random bits via threading interlocks (which are random in nature)
    /// </summary>
    /// <returns></returns>
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
            topOfRange = (1UL << bits) - 1UL; //instead of Math.Pow, I use the bitwise left shift operator
        }

        txtRange.Text = "0 - " + topOfRange.ToString();
        if (bits == 64) txtBits.Text = "64";
    }

    private void txtBits_Leave(object sender, EventArgs e)
    {
        bits = ushort.Parse(txtBits.Text.Trim());
        ComputeRange(bits);
    }
}

public class Results
{
    public List<double>? Numbers { get; set; }
    public string? TextResults { get; set; }
}
