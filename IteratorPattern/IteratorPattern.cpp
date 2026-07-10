#include <iostream>
#include <string>
#include <vector>
#include <unordered_map>
#include <unordered_set>

using namespace std;

class Profile {
public:
    string name;
    string Url;

    vector<Profile*> friends;
    unordered_map<Profile*, bool> IsFamily;

    Profile(string name, string Url) {
        this->name = name;
        this->Url = Url;
    }
};

class IProfileIterator {
public:
    virtual ~IProfileIterator() = default;

    virtual Profile* getNext() = 0;

    virtual bool hasNext() = 0;
};

class FriendsIterator : public IProfileIterator {
private:
    vector<Profile*> _friends;
    int idx = 0;

public:
    FriendsIterator(const vector<Profile*>& friends) {
        _friends = friends;
    }

    Profile* getNext() override {
        if (!hasNext()) {
            return nullptr;
        }

        return _friends[idx++];
    }

    bool hasNext() override {
        return idx < _friends.size();
    }
};

class FamilyIterator : public IProfileIterator {
private:
    int idx = 0;
    vector<Profile*> _friends;
    unordered_map<Profile*, bool> _isFamily;

public:
    FamilyIterator(
        const vector<Profile*>& friends,
        const unordered_map<Profile*, bool>& isFamily
    ) {
        _friends = friends;
        _isFamily = isFamily;
    }

    Profile* getNext() override {
        if (!hasNext()) {
            return nullptr;
        }

        return _friends[idx++];
    }

    bool hasNext() override {
        while (idx < _friends.size()) {
            Profile* currentProfile = _friends[idx];

            bool isFamilyMember =
                _isFamily.find(currentProfile) != _isFamily.end() &&
                _isFamily[currentProfile];

            if (isFamilyMember) {
                return true;
            }

            idx++;
        }

        return false;
    }
};

class MutualFriendsIterator : public IProfileIterator {
private:
    int idx = 0;
    vector<Profile*> _firstProfileFriends;
    unordered_set<Profile*> _secondProfileFriends;

public:
    MutualFriendsIterator(
        const vector<Profile*>& firstProfileFriends,
        const vector<Profile*>& secondProfileFriends
    ) {
        _firstProfileFriends = firstProfileFriends;

        for (Profile* profile : secondProfileFriends) {
            if (profile != nullptr) {
                _secondProfileFriends.insert(profile);
            }
        }
    }

    Profile* getNext() override {
        if (!hasNext()) {
            return nullptr;
        }

        return _firstProfileFriends[idx++];
    }

    bool hasNext() override {
        while (
            idx < _firstProfileFriends.size() &&
            _secondProfileFriends.find(_firstProfileFriends[idx]) == _secondProfileFriends.end()
        ) {
            idx++;
        }

        return idx < _firstProfileFriends.size();
    }
};

class IProfileIterable {
public:
    virtual ~IProfileIterable() = default;

    virtual IProfileIterator* GetProfileFriends(Profile* profile) = 0;

    virtual IProfileIterator* GetProfileFamily(Profile* profile) = 0;

    virtual IProfileIterator* GetProfileMutualFriends(
        Profile* firstProfile,
        Profile* secondProfile
    ) = 0;
};

class SocialMedia : public IProfileIterable {
public:
    IProfileIterator* GetProfileFriends(Profile* profile) override {
        if (profile == nullptr) {
            return nullptr;
        }

        return new FriendsIterator(profile->friends);
    }

    IProfileIterator* GetProfileFamily(Profile* profile) override {
        if (profile == nullptr) {
            return nullptr;
        }

        return new FamilyIterator(profile->friends, profile->IsFamily);
    }

    IProfileIterator* GetProfileMutualFriends(
        Profile* firstProfile,
        Profile* secondProfile
    ) override {
        if (firstProfile == nullptr || secondProfile == nullptr) {
            return nullptr;
        }

        return new MutualFriendsIterator(
            firstProfile->friends,
            secondProfile->friends
        );
    }
};

void PrintProfiles(IProfileIterator* iterator, const string& title) {
    cout << title << endl;

    if (iterator == nullptr) {
        cout << "No profiles found." << endl;
        cout << endl;
        return;
    }

    while (iterator->hasNext()) {
        Profile* profile = iterator->getNext();

        if (profile != nullptr) {
            cout << "- " << profile->name << " | " << profile->Url << endl;
        }
    }

    cout << endl;

    delete iterator;
}

int main() {
    Profile mustafa("Mustafa", "linkedin.com/in/mustafa");
    Profile ahmed("Ahmed", "linkedin.com/in/ahmed");
    Profile sara("Sara", "linkedin.com/in/sara");
    Profile omar("Omar", "linkedin.com/in/omar");
    Profile ali("Ali", "linkedin.com/in/ali");
    Profile nada("Nada", "linkedin.com/in/nada");

    mustafa.friends.push_back(&ahmed);
    mustafa.friends.push_back(&sara);
    mustafa.friends.push_back(&omar);
    mustafa.friends.push_back(&nada);

    ahmed.friends.push_back(&sara);
    ahmed.friends.push_back(&omar);
    ahmed.friends.push_back(&ali);

    mustafa.IsFamily[&ahmed] = true;
    mustafa.IsFamily[&sara] = false;
    mustafa.IsFamily[&omar] = true;
    mustafa.IsFamily[&nada] = false;

    SocialMedia socialMedia;

    IProfileIterator* friendsIterator =
        socialMedia.GetProfileFriends(&mustafa);

    PrintProfiles(friendsIterator, "Mustafa's Friends:");

    IProfileIterator* familyIterator =
        socialMedia.GetProfileFamily(&mustafa);

    PrintProfiles(familyIterator, "Mustafa's Family:");

    IProfileIterator* mutualFriendsIterator =
        socialMedia.GetProfileMutualFriends(&mustafa, &ahmed);

    PrintProfiles(
        mutualFriendsIterator,
        "Mutual Friends Between Mustafa and Ahmed:"
    );

    return 0;
}