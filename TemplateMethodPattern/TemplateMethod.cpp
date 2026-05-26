#include <iostream>
using namespace std;

class VideoEditingPresets {
public:
    virtual ~VideoEditingPresets() = default;

    // Template Method
    void process() {
        cout << "Starting video editing process...\n\n";

        enhanceVideoQuality();
        applyColorCorrection();
        enhanceAudioQuality();
        applyFilters();
        renderVideo();

        cout << "\nVideo editing process completed successfully.\n";
    }

protected:
    void enhanceVideoQuality() {
        cout << "Enhancing video quality...\n";
    }

    void enhanceAudioQuality() {
        cout << "Enhancing audio quality...\n";
    }

    void applyFilters() {
        cout << "Applying video filters...\n";
    }

    virtual void applyColorCorrection() = 0;
    virtual void renderVideo() = 0;
};

class FHDVideoPresets : public VideoEditingPresets {
protected:
    void applyColorCorrection() override {
        cout << "Applying advanced color correction for FHD...\n";
    }

    void renderVideo() override {
        cout << "Rendering video in Full HD (1080p)...\n";
    }
};

class HDVideoPresets : public VideoEditingPresets {
protected:
    void applyColorCorrection() override {
        cout << "Applying balanced color correction for HD...\n";
    }

    void renderVideo() override {
        cout << "Rendering video in HD (720p)...\n";
    }
};

class SDVideoPresets : public VideoEditingPresets {
protected:
    void applyColorCorrection() override {
        cout << "Applying basic color correction for SD...\n";
    }

    void renderVideo() override {
        cout << "Rendering video in SD (480p)...\n";
    }
};

int main() {

    cout << "===== FHD Preset =====\n";
    VideoEditingPresets* fhd = new FHDVideoPresets();
    fhd->process();
    delete fhd;

    cout << "\n============================\n\n";

    cout << "===== HD Preset =====\n";
    VideoEditingPresets* hd = new HDVideoPresets();
    hd->process();
    delete hd;

    cout << "\n============================\n\n";

    cout << "===== SD Preset =====\n";
    VideoEditingPresets* sd = new SDVideoPresets();
    sd->process();
    delete sd;

    return 0;
}