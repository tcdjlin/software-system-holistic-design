using System.Text.Json;

namespace BoardGameFramework;

class GameHistory{
    private List<Dictionary<string, string>> history = new List<Dictionary<string, string>>();
    private int currentMoveIndex = 0;

    private int undoThreshold = -1;

    public List<Dictionary<string, string>> getHistoryState(){
        return this.history;
    }

    public int getCurrentMoveIndex(){
        return this.currentMoveIndex;
    }

    public void updateHistory(Dictionary<string, string> newMove, bool isPlayerMove = false){
        if(this.currentMoveIndex < this.history.Count){
            this.history[this.currentMoveIndex] = newMove;
            if(isPlayerMove){
                this.history.RemoveRange(this.currentMoveIndex + 1, this.history.Count - this.currentMoveIndex - 1);
            }
        }else{
            this.history.Add(newMove);
        }
        this.currentMoveIndex += 1;
    }

    public bool onUndo(){
        if(this.currentMoveIndex == 0){
            return false;
        }else{
            if(this.undoThreshold == this.currentMoveIndex){
                return false;
            }
            this.currentMoveIndex -= 1;
            return true;
        }
    }

    public bool onRedo(){
        if(this.currentMoveIndex == this.history.Count){
            return false;
        }else{
            this.currentMoveIndex += 1;
            return true;
        }
    }

    public List<Dictionary<string, string>>? loadFromFile(){
        string pwd = Directory.GetCurrentDirectory();
        string dataFolder = Path.Combine(pwd, "gameData");

        if(Directory.Exists(dataFolder)){
            string[] files = Directory.GetFiles(dataFolder);
            if(files.Length > 0){
                Console.WriteLine("Select a game record:");
                for(int i = 0; i < files.Length; i++){
                    Console.WriteLine((i + 1) + ". " + Path.GetFileName(files[i]));
                }
                Console.WriteLine();

                while(true){
                    Console.WriteLine("Select a game file by entering the number");
                    Console.WriteLine(" - To cancel, press <enter>");
                    Console.Write(" ");
                    string selectedFileNumber = Console.ReadLine() ?? "";

                    if(string.IsNullOrWhiteSpace(selectedFileNumber)){
                        return null;
                    }else{
                        if(int.TryParse(selectedFileNumber, out int selectedNumber)){
                            int selectedIndex = selectedNumber - 1;
                            Console.WriteLine("selectedIndex: " + selectedIndex);
                            if(selectedIndex >= files.Length){
                                Console.WriteLine("Invalid selection, enter again.");
                                continue;
                            }else if(selectedIndex < 0){
                                Console.WriteLine("Invalid selection, enter again.");
                                continue;
                            }else{
                                Console.WriteLine("Selected file: " + Path.GetFileName(files[selectedIndex]));
                                string filePath = Path.Combine(dataFolder, Path.GetFileName(files[selectedIndex]));
                                List<Dictionary<string, string>> gameData; 
                                string jsonData = File.ReadAllText(filePath);
                                gameData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonData)!;
                                return gameData;
                                
                            }
                        }else{
                            Console.WriteLine("Please enter a valid file number.");
                            continue;
                        }
                    }
                }
            }else{
                Console.WriteLine("No previous data available.");
                return null;
            }
        }else{
            Console.WriteLine("No previous data available.");
            return null;
        }
        
    }

    public bool exportToFile(){
        if(this.history.Count() > 1){
            string pwd = Directory.GetCurrentDirectory();
            string dataFolder = Path.Combine(pwd, "gameData");
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = "game_" + timestamp + ".json";
            string filePath = Path.Combine(dataFolder, fileName);
            string jsonHistory = JsonSerializer.Serialize(this.history);

            if(!Directory.Exists(dataFolder)){
                Directory.CreateDirectory(dataFolder);
            }
            File.WriteAllText(filePath, jsonHistory);
            return true;
        }else{
            Console.WriteLine("At least 2 moves are required to save the file!");
        }
        return false;
    }

    public void loadGameHistory(List<Dictionary<string, string>> history){
        this.history = history;
        this.currentMoveIndex = history.Count;
        this.undoThreshold = history.Count;
    }
}