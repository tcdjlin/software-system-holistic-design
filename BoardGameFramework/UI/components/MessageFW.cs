namespace BoardGameFramework;

class MessageFW{
    private Dictionary<string, string> messages = new Dictionary<string, string>();

    public void addMessage(string title, string content){
        messages[title] = content;
    }

    public void displayMessage(string title){
        Console.WriteLine(messages[title]);
    }
}