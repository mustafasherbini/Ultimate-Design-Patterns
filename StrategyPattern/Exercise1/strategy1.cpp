#include <iostream>
#include <string>
#include <memory>
using namespace std;

class User {
public:
    string name;
    string email;
    string phoneNumber;
    string messengerURL;
    string slackURL;

    User(string name, string email, string phoneNumber, string messengerURL, string slackURL)
        : name(name), email(email), phoneNumber(phoneNumber),
          messengerURL(messengerURL), slackURL(slackURL) {}
};

class INotificationChannel {
public:
    virtual void send(User* user, const string& message) = 0;
    virtual ~INotificationChannel() = default;
};

class SMS : public INotificationChannel {
public:
    void send(User* user, const string& message) override {
        cout << "[SMS] Sending to " << user->phoneNumber << ": " << message << endl;
    }
};

class Email : public INotificationChannel {
public:
    void send(User* user, const string& message) override {
        cout << "[Email] Sending to " << user->email << ": " << message << endl;
    }
};

class Messenger : public INotificationChannel {
public:
    void send(User* user, const string& message) override {
        cout << "[Messenger] Sending to " << user->messengerURL << ": " << message << endl;
    }
};

class Slack : public INotificationChannel {
public:
    void send(User* user, const string& message) override {
        cout << "[Slack] Sending to " << user->slackURL << ": " << message << endl;
    }
};

class NotificationService {
    INotificationChannel* _channel;
public:
    NotificationService(INotificationChannel* channel) : _channel(channel) {}

    void setChannel(INotificationChannel* channel) {
        _channel = channel;
    }

    void sendNotification(User* user, const string& message) {
        _channel->send(user, message);
    }
};

int main() {
    User user("Ahmed", "ahmed@email.com", "+201234567", "m.me/ahmed", "slack://ahmed");

    SMS sms;
    Email email;
    Messenger messenger;
    Slack slack;

    NotificationService service(&sms);
    service.sendNotification(&user, "Your order has been placed!");

    service.setChannel(&email);
    service.sendNotification(&user, "Your order has been shipped!");

    service.setChannel(&messenger);
    service.sendNotification(&user, "Your order is out for delivery!");

    service.setChannel(&slack);
    service.sendNotification(&user, "Your order has been delivered!");

    return 0;
}
