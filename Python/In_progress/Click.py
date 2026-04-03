# Click.py
import pyautogui as py
import keyboard as key
import time
import os

scr_W, scr_H = py.size()

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')
    print("Running...")

class ClickMap:
    def __init__(self):
        self.learn_mode = False
        self.forget_mode = False
        self.block = {
            "shift": 0,
            "ctrl": 0,
            "tab": 0,
            "windows": 0,
            "alt": 0,
            "esc": 0
        }
        self.settings = {
            "=": 1,
            "-": -1,
            "+": 0
        }
        self.def_positions = {}
        self.clicker()

    def clicker(self):
        def handle_key(event):
            key_name = event.name
            if key_name not in self.block:
                if key_name in self.settings:
                    match self.settings[key_name]:
                        case 1:
                            if self.learn_mode == False:
                                print("Click mode ON")
                                self.learn_mode = True
                                self.forget_mode = False
                            else:
                                print("Click mode OFF")
                                self.learn_mode = False
                        case -1:
                            if self.forget_mode == False:
                                print("Forget mode ON")
                                self.learn_mode = False
                                self.forget_mode = True
                            else:
                                print("Forget mode OFF")
                                self.forget_mode = False
                        case 0:
                            self.record()

                elif self.learn_mode:
                    x, y = py.position()
                    self.def_positions[key_name] = (x, y)
                    print(f"Mapped {key_name} -> ({x}, {y})")
                    
                elif self.forget_mode:
                    if key_name in self.def_positions:
                        x, y = py.position()
                        del self.def_positions[key_name]
                        print(f"Delleted {key_name} ({x}, {y})")

                elif key_name in self.def_positions:
                        x, y = self.def_positions[key_name]
                        py.click(x, y)

        key.on_press(handle_key)
        key.wait("esc")
    
    def record(self):
        act = []
        prev_time = time.time()

        def on_move(x, y):
            pass

if __name__ == "__main__":
    Clear()
    ClickMap()
