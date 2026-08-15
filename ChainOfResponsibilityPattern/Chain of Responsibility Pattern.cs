using System;

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

public interface IHandler
{
    IHandler SetNext(IHandler nextHandler);

    bool Handle(Data data);
}

public abstract class BaseDataHandler : IHandler
{
    private IHandler? _nextHandler;

    public IHandler SetNext(IHandler nextHandler)
    {
        _nextHandler = nextHandler;
        return nextHandler;
    }

    public virtual bool Handle(Data data)
    {
        if (_nextHandler != null)
        {
            return _nextHandler.Handle(data);
        }

        return true;
    }
}

public class ValidationChecks : BaseDataHandler
{
    public override bool Handle(Data data)
    {
        Console.WriteLine("Running Validation Check...");

        if (string.IsNullOrWhiteSpace(data.Info))
        {
            Console.WriteLine("Validation failed: data is empty.");
            return false;
        }

        Console.WriteLine("Validation passed.");
        return base.Handle(data);
    }
}

public class FormattingChecks : BaseDataHandler
{
    public override bool Handle(Data data)
    {
        Console.WriteLine("Running Formatting Check...");

        if (data.Format != "CSV" && data.Format != "JSON")
        {
            Console.WriteLine("Formatting failed: unsupported format.");
            return false;
        }

        Console.WriteLine("Formatting passed.");
        return base.Handle(data);
    }
}

public class DataSizeCheck : BaseDataHandler
{
    private readonly int _maxSizeInMB;

    public DataSizeCheck(int maxSizeInMB)
    {
        _maxSizeInMB = maxSizeInMB;
    }

    public override bool Handle(Data data)
    {
        Console.WriteLine("Running Data Size Check...");

        if (data.SizeInMB > _maxSizeInMB)
        {
            Console.WriteLine("Data size failed: data is too large.");
            return false;
        }

        Console.WriteLine("Data size passed.");
        return base.Handle(data);
    }
}

public class PersonalInformationChecks : BaseDataHandler
{
    public override bool Handle(Data data)
    {
        Console.WriteLine("Running Personal Information Check...");

        if (data.ContainsPersonalInformation)
        {
            Console.WriteLine("Personal information check failed: sensitive data found.");
            return false;
        }

        Console.WriteLine("Personal information check passed.");
        return base.Handle(data);
    }
}

public class BatchJob
{
    private readonly IHandler _pipeline;

    public BatchJob(IHandler pipeline)
    {
        _pipeline = pipeline;
    }

    public void Process(Data data)
    {
        Console.WriteLine("Starting batch job pipeline...");
        Console.WriteLine("-----------------------------");

        bool passed = _pipeline.Handle(data);

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

        validation
            .SetNext(formatting)
            .SetNext(dataSize)
            .SetNext(personalInfo);

        BatchJob batchJob = new BatchJob(validation);

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