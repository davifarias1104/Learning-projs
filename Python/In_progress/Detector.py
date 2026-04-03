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
        self.record_mode = False
        self.forget_mode = False
        self.settings = {
            "=": 1,
            "-": -1,
            "space": 0
        }
        self.def_positions = {}
        self.clicker()

    def clicker(self):
        def handle_key(event):
            key_name = event.name
            print(event)
            if key_name in self.settings:
                match self.settings[key_name]:
                    case 1:
                        if self.record_mode == False:
                            print("Record mode ON")
                            self.record_mode = True
                            self.forget_mode = False
                        else:
                            print("Record mode OFF")
                            self.record_mode = False
                    case -1:
                        if self.forget_mode == False:
                            print("Forget mode ON")
                            self.record_mode = False
                            self.forget_mode = True
                        else:
                            print("Forget mode OFF")
                            self.forget_mode = False
                    case 0:
                        pass

            elif self.record_mode:
                    x, y = py.position()
                    self.def_positions.clear()
                    self.def_positions[key_name] = (x, y)
                    print(f"Mapped {key_name} -> ({x}, {y})")
                    
            elif self.forget_mode:
                if key_name in self.def_positions:
                    x, y = py.position()
                    del self.def_positions[key_name]
                    print(f"Delleted {key_name} ({x}, {y})")

        key.on_press(handle_key)
        key.wait("esc")

if __name__ == "__main__":
    Clear()
    ClickMap()
