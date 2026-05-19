#include <iostream>
#include <vector>
#include <map>
#include <algorithm>
#include <string>
using namespace std;

class BlogPost {
public:
    string data;
    BlogPost(string data) : data(data) {}
};

class WeeklyNewsletter {
public:
    string data;
    WeeklyNewsletter(string data) : data(data) {}
};

enum NotifyEvents {
    BlogPostEvent,
    WeeklyNewsletterEvent
};

class ISubscriber {
public:
    virtual ~ISubscriber() = default;
    virtual void update(NotifyEvents event, string msg) = 0;
};

class Subscriber : public ISubscriber {
public:
    string name;
    Subscriber(string name) : name(name) {}

    void update(NotifyEvents event, string msg) override {
        cout << "Notifying " << name << " about: " << msg << endl;
    }
};

class BlogManagement {
    vector<BlogPost> blogPosts;
    vector<WeeklyNewsletter> weeklyNewsLetters;
    map<NotifyEvents, vector<ISubscriber*>> eventSubscribers;

public:

    void subscribe(NotifyEvents event, ISubscriber* person) {
        eventSubscribers[event].push_back(person);
    }

    void unsubscribe(NotifyEvents event, ISubscriber* person) {
        auto& list = eventSubscribers[event];
        list.erase(remove(list.begin(), list.end(), person), list.end());
    }

    void addPost(BlogPost newPost) {
        blogPosts.push_back(newPost);
        notify(BlogPostEvent, "New blog post: " + newPost.data);
    }

    void addWeeklyNewsletter(WeeklyNewsletter newsletter) {
        weeklyNewsLetters.push_back(newsletter);
        notify(WeeklyNewsletterEvent, "New newsletter: " + newsletter.data);
    }

private:
    void notify(NotifyEvents event, string msg) {
        for (auto person : eventSubscribers[event]) {
            person->update(event, msg);
        }
    }
};

int main() {
    BlogManagement management;

    Subscriber ahmed("Ahmed");
    Subscriber sara("Sara");

    management.subscribe(BlogPostEvent, &ahmed);
    management.subscribe(BlogPostEvent, &sara);
    management.subscribe(WeeklyNewsletterEvent, &ahmed);

    management.addPost(BlogPost("Design Patterns 101"));
    management.addWeeklyNewsletter(WeeklyNewsletter("Weekly digest #1"));

    management.unsubscribe(BlogPostEvent, &sara);
    management.addPost(BlogPost("Observer Pattern deep dive"));

    return 0;
}
