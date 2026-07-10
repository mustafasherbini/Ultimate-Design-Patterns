using System;
using System.Collections.Generic;

public class Profile
{
    public string Name { get; set; }
    public string Url { get; set; }

    public List<Profile> Friends { get; set; } = new List<Profile>();

    public Dictionary<Profile, bool> IsFamily { get; set; } = new Dictionary<Profile, bool>();

    public Profile(string name, string url)
    {
        Name = name;
        Url = url;
    }
}

public interface IProfileIterator
{
    Profile? GetNext();

    bool HasNext();
}

public class FriendsIterator : IProfileIterator
{
    private readonly List<Profile> _friends;
    private int _index = 0;

    public FriendsIterator(List<Profile> friends)
    {
        _friends = friends;
    }

    public Profile? GetNext()
    {
        if (!HasNext())
        {
            return null;
        }

        return _friends[_index++];
    }

    public bool HasNext()
    {
        return _index < _friends.Count;
    }
}

public class FamilyIterator : IProfileIterator
{
    private readonly List<Profile> _friends;
    private readonly Dictionary<Profile, bool> _isFamily;
    private int _index = 0;

    public FamilyIterator(List<Profile> friends, Dictionary<Profile, bool> isFamily)
    {
        _friends = friends;
        _isFamily = isFamily;
    }

    public Profile? GetNext()
    {
        if (!HasNext())
        {
            return null;
        }

        return _friends[_index++];
    }

    public bool HasNext()
    {
        while (_index < _friends.Count)
        {
            Profile currentProfile = _friends[_index];

            bool isFamilyMember =
                _isFamily.TryGetValue(currentProfile, out bool value) && value;

            if (isFamilyMember)
            {
                return true;
            }

            _index++;
        }

        return false;
    }
}

public class MutualFriendsIterator : IProfileIterator
{
    private readonly List<Profile> _firstProfileFriends;
    private readonly HashSet<Profile> _secondProfileFriends;
    private int _index = 0;

    public MutualFriendsIterator(List<Profile> firstProfileFriends, List<Profile> secondProfileFriends)
    {
        _firstProfileFriends = firstProfileFriends;
        _secondProfileFriends = new HashSet<Profile>(secondProfileFriends);
    }

    public Profile? GetNext()
    {
        if (!HasNext())
        {
            return null;
        }

        return _firstProfileFriends[_index++];
    }

    public bool HasNext()
    {
        while (_index < _firstProfileFriends.Count)
        {
            Profile currentProfile = _firstProfileFriends[_index];

            if (_secondProfileFriends.Contains(currentProfile))
            {
                return true;
            }

            _index++;
        }

        return false;
    }
}

public interface IProfileIterable
{
    IProfileIterator GetProfileFriends(Profile profile);

    IProfileIterator GetProfileFamily(Profile profile);

    IProfileIterator GetProfileMutualFriends(Profile firstProfile, Profile secondProfile);
}

public class SocialMedia : IProfileIterable
{
    public IProfileIterator GetProfileFriends(Profile profile)
    {
        return new FriendsIterator(profile.Friends);
    }

    public IProfileIterator GetProfileFamily(Profile profile)
    {
        return new FamilyIterator(profile.Friends, profile.IsFamily);
    }

    public IProfileIterator GetProfileMutualFriends(Profile firstProfile, Profile secondProfile)
    {
        return new MutualFriendsIterator(firstProfile.Friends, secondProfile.Friends);
    }
}

public class Program
{
    public static void PrintProfiles(IProfileIterator iterator, string title)
    {
        Console.WriteLine(title);

        while (iterator.HasNext())
        {
            Profile? profile = iterator.GetNext();

            if (profile != null)
            {
                Console.WriteLine($"- {profile.Name} | {profile.Url}");
            }
        }

        Console.WriteLine();
    }

    public static void Main()
    {
        Profile mustafa = new Profile("Mustafa", "linkedin.com/in/mustafa");
        Profile ahmed = new Profile("Ahmed", "linkedin.com/in/ahmed");
        Profile sara = new Profile("Sara", "linkedin.com/in/sara");
        Profile omar = new Profile("Omar", "linkedin.com/in/omar");
        Profile ali = new Profile("Ali", "linkedin.com/in/ali");
        Profile nada = new Profile("Nada", "linkedin.com/in/nada");

        mustafa.Friends.Add(ahmed);
        mustafa.Friends.Add(sara);
        mustafa.Friends.Add(omar);
        mustafa.Friends.Add(nada);

        ahmed.Friends.Add(sara);
        ahmed.Friends.Add(omar);
        ahmed.Friends.Add(ali);

        mustafa.IsFamily[ahmed] = true;
        mustafa.IsFamily[sara] = false;
        mustafa.IsFamily[omar] = true;
        mustafa.IsFamily[nada] = false;

        SocialMedia socialMedia = new SocialMedia();

        IProfileIterator friendsIterator = socialMedia.GetProfileFriends(mustafa);
        PrintProfiles(friendsIterator, "Mustafa's Friends:");

        IProfileIterator familyIterator = socialMedia.GetProfileFamily(mustafa);
        PrintProfiles(familyIterator, "Mustafa's Family:");

        IProfileIterator mutualFriendsIterator = socialMedia.GetProfileMutualFriends(mustafa, ahmed);
        PrintProfiles(mutualFriendsIterator, "Mutual Friends Between Mustafa and Ahmed:");
    }
}