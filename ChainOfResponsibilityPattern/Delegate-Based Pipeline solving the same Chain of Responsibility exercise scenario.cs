using System;
using System.Collections.Generic;
using System.Linq;

public class Data
{
    public string Info { get; set; }
    public string Format { get; set; }
    public int SizeInMB { get; set; }
    public bool ContainsPersonalInformation { get; set; }

    public Data(
        string info,
        string format,
        int sizeInMB,
        bool containsPersonalInformation
    )
    {
        Info = info;
        Format = format;
        SizeInMB = sizeInMB;
        ContainsPersonalInformation = containsPersonalInformation;
    }
}

public delegate bool DataPipelineDelegate(Data data);

public interface IHandler
{
    bool Handle(Data data);
}

public class ValidationChecks : IHandler
{
    public bool Handle(Data data)
    {
        Console.WriteLine("Running Validation Check...");

        if (string.IsNullOrWhiteSpace(data.Info))
        {
            Console.WriteLine("Validation failed: data is empty.");
            return false;
        }

        Console.WriteLine("Validation passed.");
        return true;
    }
}

public class FormattingChecks : IHandler
{
    public bool Handle(Data data)
    {
        Console.WriteLine("Running Formatting Check...");

        if (data.Format != "CSV" && data.Format != "JSON")
        {
            Console.WriteLine("Formatting failed: unsupported format.");
            return false;
        }

        Console.WriteLine("Formatting passed.");
        return true;
    }
}

public class DataSizeCheck : IHandler
{
    private readonly int _maxSizeInMB;

    public DataSizeCheck(int maxSizeInMB)
    {
        _maxSizeInMB = maxSizeInMB;
    }

    public bool Handle(Data data)
    {
        Console.WriteLine("Running Data Size Check...");

        if (data.SizeInMB > _maxSizeInMB)
        {
            Console.WriteLine("Data size failed: data is too large.");
            return false;
        }

        Console.WriteLine("Data size passed.");
        return true;
    }
}

public class PersonalInformationChecks : IHandler
{
    public bool Handle(Data data)
    {
        Console.WriteLine("Running Personal Information Check...");

        if (data.ContainsPersonalInformation)
        {
            Console.WriteLine("Personal information check failed: sensitive data found.");
            return false;
        }

        Console.WriteLine("Personal information check passed.");
        return true;
    }
}

public class BatchJob
{
    private readonly DataPipelineDelegate _pipeline;

    public BatchJob(DataPipelineDelegate pipeline)
    {
        _pipeline = pipeline;
    }

    public void Process(Data data)
    {
        Console.WriteLine("Starting batch job pipeline...");
        Console.WriteLine("-----------------------------");

        bool passed = _pipeline(data);

        Console.WriteLine("-----------------------------");

        if (passed)
        {
            Console.WriteLine("All checks passed. Processing data...");
        }
        else
        {
            Console.WriteLine("Pipeline failed. Data processing stopped.");
        }

        Console.WriteLine();
    }
}

public class Program
{
    public static void Main()
    {
        IHandler validation = new ValidationChecks();
        IHandler formatting = new FormattingChecks();
        IHandler dataSize = new DataSizeCheck(100);
        IHandler personalInfo = new PersonalInformationChecks();

        List<DataPipelineDelegate> checks = new List<DataPipelineDelegate>
        {
            validation.Handle,
            formatting.Handle,
            dataSize.Handle,
            personalInfo.Handle
        };

        DataPipelineDelegate dataPipelineDelegate = data =>
            checks.All(check => check(data));

        BatchJob batchJob = new BatchJob(dataPipelineDelegate);

        Data validData = new Data(
            "Big data content",
            "CSV",
            50,
            false
        );

        Data invalidFormatData = new Data(
            "Some content",
            "TXT",
            20,
            false
        );

        Data largeData = new Data(
            "Huge data content",
            "JSON",
            150,
            false
        );

        Data personalData = new Data(
            "Customer private data",
            "CSV",
            30,
            true
        );

        Console.WriteLine("Test Case 1: Valid Data");
        batchJob.Process(validData);

        Console.WriteLine("Test Case 2: Invalid Format Data");
        batchJob.Process(invalidFormatData);

        Console.WriteLine("Test Case 3: Large Data");
        batchJob.Process(largeData);

        Console.WriteLine("Test Case 4: Personal Information Data");
        batchJob.Process(personalData);
    }
}