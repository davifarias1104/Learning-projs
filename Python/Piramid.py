# Piramid.py
import os

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')

class Piramid:
    def __init__(self, height: int):
        for i in range(height):
            print(" " * (height - i - 1) + "*" * ((i * 2) + 1))

if __name__ == "__main__":
    while True:
        Clear()
        i = input("Enter the height of the pyramid: ")
        if i.isdigit():
            Piramid(int(i))
            break