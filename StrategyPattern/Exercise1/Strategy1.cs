using System;

public class User {
    public string Name          { get; set; }
    public string Email         { get; set; }
    public string PhoneNumber   { get; set; }
    public string MessengerURL  { get; set; }
    public string SlackURL      { get; set; }

    public User(string name, string email, string phoneNumber, string messengerURL, string slackURL) {
        Name         = name;
        Email        = email;
        PhoneNumber  = phoneNumber;
        MessengerURL = messengerURL;
        SlackURL     = slackURL;
    }
}

public interface INotificationChannel {
    void Send(User user, string message);
}

public class SMS : INotificationChannel {
    public void Send(User user, string message) {
        Console.WriteLine($"[SMS] Sending to {user.PhoneNumber}: {message}");
    }
}

public class Email : INotificationChannel {
    public void Send(User user, string message) {
        Console.WriteLine($"[Email] Sending to {user.Email}: {message}");
    }
}

public class Messenger : INotificationChannel {
    public void Send(User user, string message) {
        Console.WriteLine($"[Messenger] Sending to {user.MessengerURL}: {message}");
    }
}

public class Slack : INotificationChannel {
    public void Send(User user, string message) {
        Console.WriteLine($"[Slack] Sending to {user.SlackURL}: {message}");
    }
}

public class NotificationService {
    private INotificationChannel _channel;

    public NotificationService(INotificationChannel channel) {
        _channel = channel;
    }

    public void SetChannel(INotificationChannel channel) {
        _channel = channel;
    }

    public void SendNotification(User user, string message) {
        _channel.Send(user, message);
    }
}

class Program {
    static void Main() {
        var user = new User("Ahmed", "ahmed@email.com", "+201234567", "m.me/ahmed", "slack://ahmed");

        var service = new NotificationService(new SMS());
        service.SendNotification(user, "Your order has been placed!");

        service.SetChannel(new Email());
        service.SendNotification(user, "Your order has been shipped!");

        service.SetChannel(new Messenger());
        service.SendNotification(user, "Your order is out for delivery!");

        service.SetChannel(new Slack());
        service.SendNotification(user, "Your order has been delivered!");
    }
}
