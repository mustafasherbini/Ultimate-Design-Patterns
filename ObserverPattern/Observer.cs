using System;
using System.Collections.Generic;

public class BlogPost {
    public string Data { get; set; }
    public BlogPost(string data) => Data = data;
}

public class WeeklyNewsletter {
    public string Data { get; set; }
    public WeeklyNewsletter(string data) => Data = data;
}

public enum NotifyEvents {
    BlogPost,
    WeeklyNewsletter
}

public interface ISubscriber {
    void Update(NotifyEvents notifyEvent, string msg);
}

public class Subscriber : ISubscriber {
    public string Name { get; set; }
    public Subscriber(string name) => Name = name;

    public void Update(NotifyEvents notifyEvent, string msg) {
        Console.WriteLine($"Notifying {Name} about a new {notifyEvent}: {msg}");
    }
}

public class BlogManagement {
    private List<BlogPost> _blogPosts = new();
    private List<WeeklyNewsletter> _weeklyNewsletters = new();
    private Dictionary<NotifyEvents, List<ISubscriber>> _eventSubscribers;

    public BlogManagement() {
        _eventSubscribers = new Dictionary<NotifyEvents, List<ISubscriber>> {
            { NotifyEvents.BlogPost,         new List<ISubscriber>() },
            { NotifyEvents.WeeklyNewsletter, new List<ISubscriber>() }
        };
    }

    public void Subscribe(NotifyEvents notifyEvent, ISubscriber person) {
        _eventSubscribers[notifyEvent].Add(person);
    }

    public void Unsubscribe(NotifyEvents notifyEvent, ISubscriber person) {
        _eventSubscribers[notifyEvent].Remove(person);
    }

    public void AddPost(BlogPost post) {
        _blogPosts.Add(post);
        Notify(NotifyEvents.BlogPost, post.Data);
    }

    public void AddWeeklyNewsletter(WeeklyNewsletter newsletter) {
        _weeklyNewsletters.Add(newsletter);
        Notify(NotifyEvents.WeeklyNewsletter, newsletter.Data);
    }

    private void Notify(NotifyEvents notifyEvent, string msg) {
        foreach (var person in _eventSubscribers[notifyEvent]) {
            person.Update(notifyEvent, msg);
        }
    }
}

class Program {
    static void Main() {
        var management = new BlogManagement();

        var ahmed = new Subscriber("Ahmed");
        var sara  = new Subscriber("Sara");

        management.Subscribe(NotifyEvents.BlogPost, ahmed);
        management.Subscribe(NotifyEvents.BlogPost, sara);
        management.Subscribe(NotifyEvents.WeeklyNewsletter, ahmed);

        management.AddPost(new BlogPost("Design Patterns 101"));
        management.AddWeeklyNewsletter(new WeeklyNewsletter("Weekly digest #1"));

        management.Unsubscribe(NotifyEvents.BlogPost, sara);
        management.AddPost(new BlogPost("Observer Pattern deep dive"));
    }
}
