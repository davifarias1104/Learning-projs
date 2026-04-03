# FileSearcher.py
import os

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')

class Searcher:
    def __init__(self, Entry: str, FileName: str):
        print("Searching...")
        for root, dirs, files in os.walk(Entry):
            for file in files:
                if FileName in file:
                    print("\nFound: ")
                    print(file)
                    print(root, "\n")

if __name__ == "__main__":
    Clear()
    Entry: str = input("Entry (Default: C:\\): ")
    if not Entry:
        Entry = "C:\\"
    FileName: str = input("File: ")
    Searcher(Entry, FileName)
