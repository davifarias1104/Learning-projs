# Click.py
import pyautogui as py
import keyboard as key
import time
import os

def Clear():
    # input cls or clear on the terminal
    os.system('cls' if os.name == 'nt' else 'clear')
    print("Running...")

class ClickMap:
    def __init__(self):
        self.learn_mode = False
        self.forget_mode = False
        self.mouse_hold = False
        self.block = {
            "shift": 0,
            "ctrl": 0,
            "tab": 0,
            "left windows": 0,
            "alt": 0,
            "esc": 0,
            "caps lock": 0,
            "menu": 0,
            "alt gr": 0,
            "num lock": 0,
            "backspace": 0,
            "enter": 0,
            "delete": 0,
            "insert": 0
        }
        self.settings = {
            "=": 1,
            "-": 2,
            "[": 3,
            "#": 4
        }
        self.def_positions = {}
        self.clicker()

    def clicker(self):
        def handle_var():
            self.learn_mode = False
            self.forget_mode = False
            self.mouse_hold = False
            return True
        
        def handle_key(event):
            key_name = event.name
            if key_name not in self.block:
                if key_name in self.settings:
                    match self.settings[key_name]:
                        case 1:
                            if not self.learn_mode:
                                print("Learn mode ON")
                                self.learn_mode = handle_var()
                            else:
                                print("Learn mode OFF")
                                self.learn_mode = False
                        case 2:
                            if not self.forget_mode:
                                print("Forget mode ON")
                                self.forget_mode = handle_var()
                            else:
                                print("Forget mode OFF")
                                self.forget_mode = False
                        case 3:
                            if not self.mouse_hold:
                                print("Holding Down")
                                py.mouseDown()
                                self.mouse_hold = handle_var()
                            else:
                                print("Holding Up")
                                py.mouseUp()
                                self.mouse_hold = False
                        case 4:
                            handle_var()
                            self.Debugger()

                elif self.learn_mode:
                    x, y = py.position()
                    self.def_positions[key_name] = (x, y)
                    print(f"Mapped {key_name} -> ({x}, {y})")
                    
                elif self.forget_mode:
                    if key_name in self.def_positions:
                        print(f"Delleted {key_name} {self.def_positions[key_name]}")
                        del self.def_positions[key_name]

                elif key_name in self.def_positions:
                        x, y = self.def_positions[key_name]
                        py.click(x, y)

        key.on_press(handle_key)
        key.wait("esc")
    
    def Debugger(self):
        time.sleep(0.1)
        print(f"\033[2K\r{'\033[92m'}#{'\033[0m'}", end="", flush=True)
        time.sleep(0.1)
        input(print(f"\033[2K\r{'\033[92m'}#{'\033[0m'}", end="", flush=True))

if __name__ == "__main__":
    Clear()
    ClickMap()
