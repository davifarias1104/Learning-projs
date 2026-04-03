# Slot.py
import random
import os
import json
import sys
import msvcrt

def login():
    username = input("Enter your username: ").strip()
    if not username:
        Clear()
        print_at(1, 2, "Username cannot be empty.")
        print_at(1, 0, "")
        return login()
    return username

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')

def print_at(x: int, y: int, text: str):
    sys.stdout.write(f"\033[{y};{x}H{text}")
    sys.stdout.flush()

def get_input(prompt: str) -> str:
    print(prompt, end='', flush=True)
    chars = []
    while True:
        ch = msvcrt.getch()
        if ch in {b'\r', b'\n'}:
            print()
            break
        elif ch == b'\x1b':
            print()
            return 'esc'
        elif ch == b'\x08':
            if chars:
                chars.pop()
                sys.stdout.write('\b \b')
                sys.stdout.flush()
        else:
            try:
                decoded = ch.decode()
            except Exception:
                continue
            chars.append(decoded)
            sys.stdout.write(decoded)
            sys.stdout.flush()
    return ''.join(chars).strip()

class Slot:
    def __init__(self, balance: int, username: str, highscore: int):
        Clear()
        self.folder_name = "Python"
        self.file_name = "data.json"
        self.path = os.path.join(self.folder_name, self.file_name)
        self.balance = balance
        self.username = username
        self.highscore = highscore
        self.load_data()
        self.Menu()

    def save_data(self):
        try:
            with open(self.path, "r") as f:
                all_data = json.load(f)
        except (FileNotFoundError, json.JSONDecodeError):
            all_data = {}
        all_data[self.username] = {
            "balance": self.balance,
            "highscore": self.highscore
        }
        with open(self.path, "w") as f:
            json.dump(all_data, f, indent=4)

    def load_data(self):
        try:
            with open(self.path, "r") as f:
                all_data = json.load(f)
                user_data = all_data.get(self.username, {})
                self.balance = user_data.get("balance", 0)
                self.highscore = user_data.get("highscore", 0)
        except (FileNotFoundError, json.JSONDecodeError):
            self.balance = 0
            self.highscore = 0

    def Menu(self):
        while True:
            print_at(1, 1, "Welcome to the Slot Machine!")
            print_at(1, 2, f"Username: {self.username}")
            print_at(1, 3, f"Current Balance: {self.balance}")
            print_at(1, 4, f"Highscore: {self.highscore}")
            print_at(1, 6, "Menu:")
            print_at(3, 7, "1. Enter Balance")
            print_at(3, 8, "2. Retrieve Balance")
            print_at(3, 9, "3. Slot")
            print_at(3, 10, "4. Exit")
            print_at(1, 12, "")
            choice: str = get_input("Choose an option: ")
            match choice:
                case "1":
                    self.Enter_balance()
                case "2":
                    self.Retrive_balance()
                case "3":
                    self.Slot()
                case "4":
                    exit()
                case _:
                    Clear()
                    print_at(1, 13, "Invalid option.")
                    continue
            Clear()
            self.Menu()

    def Enter_balance(self):
        Clear()
        while True:
            print_at(1, 1, "Balance Deposit:")
            print_at(3, 2, f"Current Balance: {self.balance}")
            print_at(1, 7, "Press Esc to cancel.")
            print_at(3, 4, "")
            amount = get_input("Enter value: ")
            if amount.lower() == 'esc':
                return
            if not amount.isdigit() or int(amount) <= 0:
                Clear()
                print_at(3, 6, "Invalid input.")
                continue
            self.balance = self.balance + int(amount)
            self.save_data()
            print_at(3, 5, f"Balance updated: {self.balance}")
            input("")
            return

    def Retrive_balance(self):
        Clear()
        while True:
            print_at(1, 1, "Retrieve your balance:")
            print_at(3, 2, f"Current Balance: {self.balance}")
            print_at(1, 7, "Press Esc to cancel.")
            print_at(3, 4, "")
            amount = get_input("Enter value: ")
            if amount.lower() == 'esc':
                return
            if not amount.isdigit() or int(amount) <= 0:
                Clear()
                print_at(3, 6, "Invalid input.")
                continue
            if self.balance < int(amount):
                Clear()
                print_at(3, 6, "Insufficient balance.")
                continue
            self.balance: int = self.balance - int(amount)
            self.save_data()
            print_at(3, 5, f"Balance updated: {self.balance}")
            input("")
            return

    def Slot(self):
        def spin():
            symbols = ["🍒", "🍋", "🍊", "🍉", "🍇", "🍒", "🍋", "🍊", "🍉", "🍇", "7"]
            return [random.choice(symbols) for _ in range(3)]
        Clear()
        while True:
            print_at(1, 1, "Slot Machine Game:")
            print_at(3, 2, f"Current Balance: {self.balance}")
            print_at(3, 3, f"Highscore: {self.highscore}")
            print_at(1, 11, "Press Esc to exit.")
            print_at(3, 5,"")
            input_value = get_input("Press Enter to spin: ")
            if input_value.lower() == 'esc':
                return
            if self.balance < 10:
                print_at(3, 10, "Insufficient balance.")
                input("")
                return
            self.balance -= 10
            self.save_data()
            result = spin()
            print_at(3, 6, f"Result: [{' '.join(result)}]")
            if result.count("7") == 3:
                jackpot_amount = 1000
                self.balance += jackpot_amount
                self.save_data()
                print_at(6, 7, "JACKPOT!!!")
                print_at(3, 8, f"You won {jackpot_amount}! New balance: {self.balance}")
                if self.balance > self.highscore:
                    self.highscore = self.balance
                    self.save_data()
                    print_at(3, 9, f"New highscore: {self.highscore}")
            elif result.count(result[0]) == len(result):
                win_amount = 100
                self.balance += win_amount
                self.save_data()
                print_at(3, 8, f"You won {win_amount}! New balance: {self.balance}")
                if self.balance > self.highscore:
                    self.highscore = self.balance
                    self.save_data()
                    print_at(3, 9, f"New highscore: {self.highscore}")
            else:
                print_at(3, 8, "You lost this round.")
            print_at(3, 10, "Press Enter to continue...")
            input("")
            Clear()

if __name__ == "__main__":
    Clear()
    Slot(0, str(login()), 0)
