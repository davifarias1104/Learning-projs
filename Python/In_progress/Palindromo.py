import os

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')

class Palindromo:
    def __init__(self):
        self.Names = [input("1: "), input("2: "), input("3: "), input("4: "), input("5: ")]
        for x in range(len(self.Names)):
            self.Inver = self.Names[x - 1][::-1]
            print(self.Inver[x - 1])

if __name__ == "__main__":
    Clear()
    Palindromo()