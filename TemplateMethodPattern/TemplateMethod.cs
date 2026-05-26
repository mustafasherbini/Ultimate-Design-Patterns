using System;

abstract class VideoEditingPresets
{
    // Template Method
    public void Process()
    {
        Console.WriteLine("Starting video editing process...\n");

        EnhanceVideoQuality();
        ApplyColorCorrection();
        EnhanceAudioQuality();
        ApplyFilters();
        RenderVideo();

        Console.WriteLine("\nVideo editing process completed successfully.");
    }

    protected void EnhanceVideoQuality()
    {
        Console.WriteLine("Enhancing video quality...");
    }

    protected void EnhanceAudioQuality()
    {
        Console.WriteLine("Enhancing audio quality...");
    }

    protected void ApplyFilters()
    {
        Console.WriteLine("Applying video filters...");
    }

    protected abstract void ApplyColorCorrection();
    protected abstract void RenderVideo();
}

class FHDVideoPresets : VideoEditingPresets
{
    protected override void ApplyColorCorrection()
    {
        Console.WriteLine("Applying advanced color correction for FHD...");
    }

    protected override void RenderVideo()
    {
        Console.WriteLine("Rendering video in Full HD (1080p)...");
    }
}

class HDVideoPresets : VideoEditingPresets
{
    protected override void ApplyColorCorrection()
    {
        Console.WriteLine("Applying balanced color correction for HD...");
    }

    protected override void RenderVideo()
    {
        Console.WriteLine("Rendering video in HD (720p)...");
    }
}

class SDVideoPresets : VideoEditingPresets
{
    protected override void ApplyColorCorrection()
    {
        Console.WriteLine("Applying basic color correction for SD...");
    }

    protected override void RenderVideo()
    {
        Console.WriteLine("Rendering video in SD (480p)...");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== FHD Preset =====");
        VideoEditingPresets fhd = new FHDVideoPresets();
        fhd.Process();

        Console.WriteLine("\n============================\n");

        Console.WriteLine("===== HD Preset =====");
        VideoEditingPresets hd = new HDVideoPresets();
        hd.Process();

        Console.WriteLine("\n============================\n");

        Console.WriteLine("===== SD Preset =====");
        VideoEditingPresets sd = new SDVideoPresets();
        sd.Process();
    }
}